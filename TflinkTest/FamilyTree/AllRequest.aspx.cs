using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Globalization;


namespace TflinkTest.FamilyTree
{
    public partial class AllRequest : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string memid = "";
        string memberidbysub = "";
        string familyid = "";
        string Memid = "";
        string MemberId = "";

        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MemberId = Session["Memberid"].ToString();
            }
            catch
            {

            }
            if (!IsPostBack)
            {
                getrequest();
                getmyrequests();
            }
        }
        public void getrequest()
        {
            SqlConnection con = new SqlConnection(strcon);
            string memid = "";
            try
            {
                memid = Session["MemberId"].ToString();
            }
            catch
            {

            }
            //try
            //{
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_AllRequests where RequestTo='" + memid + "' order by id desc", con);
            adapt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Grd_Pofile.DataSource = dt;
                Grd_Pofile.DataBind();
            }
            else
            {
                myrequest.InnerText = "No data found";
            }
        }
        public void getmyrequests()
        {
            SqlConnection con = new SqlConnection(strcon);
            string memid = "";
            try
            {
                memid = Session["MemberId"].ToString();
            }
            catch
            {

            }
            //try
            //{
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_AllRequests where RequestFrom='" + memid + "' order by id desc", con);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grd_bind.DataSource = dt;
                grd_bind.DataBind();
            }
            else
            {
                reqfromother.InnerText = "No data found";
            }

        }

        protected void lnkbtn_live_Click(object sender, EventArgs e)
        {
            string date = "";
            SqlConnection con = new SqlConnection(strcon);
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            string RowIndex = row.Cells[6].Text;
            string btnsts = null;
            string Query = "Select * from Tbl_AllRequests where id='" + RowIndex + "'";
            DataTable dt = RetriveData(Query);
            //  string ordersts = dt.Rows[0]["Status"].ToString();
            if (dt.Rows.Count > 0)
            {
                btnsts = dt.Rows[0]["Status"].ToString();
                date = btnsts;
            }
            if (btnsts == "Accept")
            {
                date = DateTime.Now.ToString("yyyy/MM/dd");
                btnsts = "Accepted";
                GetparentsUpdate(RowIndex);
            }
            else if (btnsts == "Accepted")
            {
                //btnsts = "Accept";
            }
            Query = "update Tbl_AllRequests set Status='" + date + "',Regstatus='" + btnsts + "' where id='" + RowIndex + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            getrequest();
            if (btnsts == "Accept")
            {
                //check the request is comming for what
                //Check that member is already added as that or not
                //Add that member to all table as per request
            }
        }
        public DataTable RetriveData(string Query)
        {
            //try
            //{
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            // }
            //catch
            //{
            //    return dt;
            //}
        }
        public string GetparentsUpdate(string id)
        {
            string ParentsType = "";
            string Email = "";
            string SpouseType = "";
            string YouAre = "";
            string YouSpouseid = "";
            string YouSpousename = "";
            string ReqstType = "";
            string RequestNo = "";
            string RequestFrom = "";
            string RequestTo = "";
            string RequestFor = "";
            string RequestMsg = "";
            string Memberids = "";

            SqlConnection con = new SqlConnection(strcon);
            string Memberid = "";
            try
            {
                Memberid = Session["Memberid"].ToString();
            }
            catch
            {

            }
           
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_AllRequests where id='" + id + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                ParentsType = dt1.Rows[0]["ParentsType"].ToString();
                Email = dt1.Rows[0]["Email"].ToString();
                SpouseType = dt1.Rows[0]["SpouseType"].ToString();
                YouAre = dt1.Rows[0]["YouAre"].ToString();
                YouSpouseid = dt1.Rows[0]["YouSpouseid"].ToString();
                YouSpousename = dt1.Rows[0]["YouSpousename"].ToString();
                ReqstType = dt1.Rows[0]["ReqstType"].ToString();//check type and add parents
                RequestNo = dt1.Rows[0]["RequestNo"].ToString();
                RequestFrom = dt1.Rows[0]["RequestFrom"].ToString();//Main member id
                RequestTo = dt1.Rows[0]["RequestTo"].ToString();
                RequestFor = dt1.Rows[0]["RequestFor"].ToString();//Add this person as type
                RequestMsg = dt1.Rows[0]["RequestMsg"].ToString();
                Memberids = dt1.Rows[0]["Memberid"].ToString();

                if (ReqstType == "SpouseType")//add many
                {
                    insertspouseof(RequestFrom, RequestFor, ParentsType);
                }
                else if (ReqstType == "ParentsType")
                {
                    insertinspouseof(RequestFrom, RequestFor, ParentsType);
                    insertChild(RequestFrom, RequestFor, ParentsType);
                    insertparentinspouseof(RequestFrom, RequestFor, ParentsType);
                    //add parents
                }
                else if (ReqstType == "ChildType")
                {
                    insertparents(RequestFrom, RequestFor, ParentsType, YouSpouseid, YouSpousename);
          
                }
                // insertparentinspouseof(RequestFrom, RequestFor, ParentsType);
            }

            return "";
        }
        public void insertinspouseof(string Memidfrom, string addingpermemid, string parenttype)
        {
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Ffamilyid = "";
            string FMemberId = "";
            string FFirstName = "";
            string FLastName = "";
            string FContact = "";
            string FGender = "";
            string FEmailId = "";
            string FAddress = "";
            string FCity = "";
            string FState = "";
            string FDob = "";
            string FCreradob = "";
            string FCountry = "";
            string FBirthloc = "";
            string FDod = "";
            string FCreradod = "";
            string FNotes = "";
            string FUserid = "";
            string FPassword = "";

            string Tfamilyid = "";
            string TMemberId = "";
            string TFirstName = "";
            string TLastName = "";
            string TContact = "";
            string TGender = "";
            string TEmailId = "";
            string TAddress = "";
            string TCity = "";
            string TState = "";
            string TDob = "";
            string TCreradob = "";
            string TCountry = "";
            string TBirthloc = "";
            string TDod = "";
            string TCreradod = "";
            string TNotes = "";
            string TUserid = "";
            string TPassword = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memidfrom + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Ffamilyid = dt1.Rows[0]["FamilyId"].ToString();
                FMemberId = dt1.Rows[0]["MemberId"].ToString();
                FFirstName = dt1.Rows[0]["FirstName"].ToString();
                FLastName = dt1.Rows[0]["LastName"].ToString();
                FContact = dt1.Rows[0]["Contact"].ToString();
                FGender = dt1.Rows[0]["Gender"].ToString();
                FEmailId = dt1.Rows[0]["EmailId"].ToString();
                FAddress = dt1.Rows[0]["Address"].ToString();
                FCity = dt1.Rows[0]["City"].ToString();
                FState = dt1.Rows[0]["State"].ToString();
                FDob = dt1.Rows[0]["Dob"].ToString();
                FCreradob = dt1.Rows[0]["Creradob"].ToString();
                FCountry = dt1.Rows[0]["Country"].ToString();
                FBirthloc = dt1.Rows[0]["Birthloc"].ToString();
                FDod = dt1.Rows[0]["Dod"].ToString();
                FCreradod = dt1.Rows[0]["Creradod"].ToString();
                FNotes = dt1.Rows[0]["Notes"].ToString();
                FUserid = dt1.Rows[0]["Userid"].ToString();
                FPassword = dt1.Rows[0]["Password"].ToString();
            }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + addingpermemid + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                Tfamilyid = dt2.Rows[0]["FamilyId"].ToString();
                TMemberId = dt2.Rows[0]["MemberId"].ToString();
                TFirstName = dt2.Rows[0]["FirstName"].ToString();
                TLastName = dt2.Rows[0]["LastName"].ToString();
                TContact = dt2.Rows[0]["Contact"].ToString();
                TGender = dt2.Rows[0]["Gender"].ToString();
                TEmailId = dt2.Rows[0]["EmailId"].ToString();
                TAddress = dt2.Rows[0]["Address"].ToString();
                TCity = dt2.Rows[0]["City"].ToString();
                TState = dt2.Rows[0]["State"].ToString();
                TDob = dt2.Rows[0]["Dob"].ToString();
                TCreradob = dt2.Rows[0]["Creradob"].ToString();
                TCountry = dt2.Rows[0]["Country"].ToString();
                TBirthloc = dt2.Rows[0]["Birthloc"].ToString();
                TDod = dt2.Rows[0]["Dod"].ToString();
                TCreradod = dt2.Rows[0]["Creradod"].ToString();
                TNotes = dt2.Rows[0]["Notes"].ToString();
                TUserid = dt2.Rows[0]["Userid"].ToString();
                TPassword = dt2.Rows[0]["Password"].ToString();
            }
            cmd = new SqlCommand("SPParentsof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", Tfamilyid);
            cmd.Parameters.AddWithValue("Parentstype", parenttype);
            cmd.Parameters.AddWithValue("MemberFName", TFirstName);
            cmd.Parameters.AddWithValue("MemberLName", TLastName);
            cmd.Parameters.AddWithValue("Dob", TDob);
            cmd.Parameters.AddWithValue("Gender", TGender);
            cmd.Parameters.AddWithValue("Memberid", TMemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", FFirstName);
            cmd.Parameters.AddWithValue("ParentsofLName", FLastName);
            cmd.Parameters.AddWithValue("Parentsofid", FMemberId);
            cmd.Parameters.AddWithValue("Status", "Byrequest");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", MemberId);
            //cmd.Parameters.AddWithValue("StatementType", "Update"); 
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                Session["Slno"] = null;
            }
            con.Close();
        }
        public void insertChild(string Memidfrom, string addingpermemid, string parenttype)
        {
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Ffamilyid = "";
            string FMemberId = "";
            string FFirstName = "";
            string FLastName = "";
            string FContact = "";
            string FGender = "";
            string FEmailId = "";
            string FAddress = "";
            string FCity = "";
            string FState = "";
            string FDob = "";
            string FCreradob = "";
            string FCountry = "";
            string FBirthloc = "";
            string FDod = "";
            string FCreradod = "";
            string FNotes = "";
            string FUserid = "";
            string FPassword = "";

            string Tfamilyid = "";
            string TMemberId = "";
            string TFirstName = "";
            string TLastName = "";
            string TContact = "";
            string TGender = "";
            string TEmailId = "";
            string TAddress = "";
            string TCity = "";
            string TState = "";
            string TDob = "";
            string TCreradob = "";
            string TCountry = "";
            string TBirthloc = "";
            string TDod = "";
            string TCreradod = "";
            string TNotes = "";
            string TUserid = "";
            string TPassword = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memidfrom + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Ffamilyid = dt1.Rows[0]["FamilyId"].ToString();
                FMemberId = dt1.Rows[0]["MemberId"].ToString();
                FFirstName = dt1.Rows[0]["FirstName"].ToString();
                FLastName = dt1.Rows[0]["LastName"].ToString();
                FContact = dt1.Rows[0]["Contact"].ToString();
                FGender = dt1.Rows[0]["Gender"].ToString();
                FEmailId = dt1.Rows[0]["EmailId"].ToString();
                FAddress = dt1.Rows[0]["Address"].ToString();
                FCity = dt1.Rows[0]["City"].ToString();
                FState = dt1.Rows[0]["State"].ToString();
                FDob = dt1.Rows[0]["Dob"].ToString();
                FCreradob = dt1.Rows[0]["Creradob"].ToString();
                FCountry = dt1.Rows[0]["Country"].ToString();
                FBirthloc = dt1.Rows[0]["Birthloc"].ToString();
                FDod = dt1.Rows[0]["Dod"].ToString();
                FCreradod = dt1.Rows[0]["Creradod"].ToString();
                FNotes = dt1.Rows[0]["Notes"].ToString();
                FUserid = dt1.Rows[0]["Userid"].ToString();
                FPassword = dt1.Rows[0]["Password"].ToString();
            }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + addingpermemid + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                Tfamilyid = dt2.Rows[0]["FamilyId"].ToString();
                TMemberId = dt2.Rows[0]["MemberId"].ToString();
                TFirstName = dt2.Rows[0]["FirstName"].ToString();
                TLastName = dt2.Rows[0]["LastName"].ToString();
                TContact = dt2.Rows[0]["Contact"].ToString();
                TGender = dt2.Rows[0]["Gender"].ToString();
                TEmailId = dt2.Rows[0]["EmailId"].ToString();
                TAddress = dt2.Rows[0]["Address"].ToString();
                TCity = dt2.Rows[0]["City"].ToString();
                TState = dt2.Rows[0]["State"].ToString();
                TDob = dt2.Rows[0]["Dob"].ToString();
                TCreradob = dt2.Rows[0]["Creradob"].ToString();
                TCountry = dt2.Rows[0]["Country"].ToString();
                TBirthloc = dt2.Rows[0]["Birthloc"].ToString();
                TDod = dt2.Rows[0]["Dod"].ToString();
                TCreradod = dt2.Rows[0]["Creradod"].ToString();
                TNotes = dt2.Rows[0]["Notes"].ToString();
                TUserid = dt2.Rows[0]["Userid"].ToString();
                TPassword = dt2.Rows[0]["Password"].ToString();
            }

            cmd = new SqlCommand("SPChildrenof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", Tfamilyid);
            cmd.Parameters.AddWithValue("Youare", parenttype);
            cmd.Parameters.AddWithValue("SpouseName", "");
            cmd.Parameters.AddWithValue("SpouseMemid", "");
            cmd.Parameters.AddWithValue("MemberFName", FFirstName);
            cmd.Parameters.AddWithValue("MemberLName", FLastName);
            cmd.Parameters.AddWithValue("Dob", FDob);
            cmd.Parameters.AddWithValue("Gender", FGender);
            cmd.Parameters.AddWithValue("Memberid", FMemberId);
            cmd.Parameters.AddWithValue("ChildoffName", TFirstName);//Got values from Parents
            cmd.Parameters.AddWithValue("ChildofLName", TLastName);//
            cmd.Parameters.AddWithValue("Childofid", TMemberId);//
            cmd.Parameters.AddWithValue("Status", "Byrequest");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", MemberId);
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                //Response.Redirect("~/FamilyTree/MyTree1.aspx");
            }
            con.Close();
        }
        public void insertparentinspouseof(string Memidfrom, string addingpermemid, string parenttypefrm)
        {
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Ffamilyid = "";
            string FMemberId = "";
            string FFirstName = "";
            string FLastName = "";
            string FContact = "";
            string FGender = "";
            string FEmailId = "";
            string FAddress = "";
            string FCity = "";
            string FState = "";
            string FDob = "";
            string FCreradob = "";
            string FCountry = "";
            string FBirthloc = "";
            string FDod = "";
            string FCreradod = "";
            string FNotes = "";
            string FUserid = "";
            string FPassword = "";

            string Tfamilyid = "";
            string TMemberId = "";
            string TFirstName = "";
            string TLastName = "";
            string TContact = "";
            string TGender = "";
            string TEmailId = "";
            string TAddress = "";
            string TCity = "";
            string TState = "";
            string TDob = "";
            string TCreradob = "";
            string TCountry = "";
            string TBirthloc = "";
            string TDod = "";
            string TCreradod = "";
            string TNotes = "";
            string TUserid = "";
            string TPassword = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memidfrom + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Ffamilyid = dt1.Rows[0]["FamilyId"].ToString();
                FMemberId = dt1.Rows[0]["MemberId"].ToString();
                FFirstName = dt1.Rows[0]["FirstName"].ToString();
                FLastName = dt1.Rows[0]["LastName"].ToString();
                FContact = dt1.Rows[0]["Contact"].ToString();
                FGender = dt1.Rows[0]["Gender"].ToString();
                FEmailId = dt1.Rows[0]["EmailId"].ToString();
                FAddress = dt1.Rows[0]["Address"].ToString();
                FCity = dt1.Rows[0]["City"].ToString();
                FState = dt1.Rows[0]["State"].ToString();
                FDob = dt1.Rows[0]["Dob"].ToString();
                FCreradob = dt1.Rows[0]["Creradob"].ToString();
                FCountry = dt1.Rows[0]["Country"].ToString();
                FBirthloc = dt1.Rows[0]["Birthloc"].ToString();
                FDod = dt1.Rows[0]["Dod"].ToString();
                FCreradod = dt1.Rows[0]["Creradod"].ToString();
                FNotes = dt1.Rows[0]["Notes"].ToString();
                FUserid = dt1.Rows[0]["Userid"].ToString();
                FPassword = dt1.Rows[0]["Password"].ToString();
            }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + addingpermemid + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                Tfamilyid = dt2.Rows[0]["FamilyId"].ToString();
                TMemberId = dt2.Rows[0]["MemberId"].ToString();
                TFirstName = dt2.Rows[0]["FirstName"].ToString();
                TLastName = dt2.Rows[0]["LastName"].ToString();
                TContact = dt2.Rows[0]["Contact"].ToString();
                TGender = dt2.Rows[0]["Gender"].ToString();
                TEmailId = dt2.Rows[0]["EmailId"].ToString();
                TAddress = dt2.Rows[0]["Address"].ToString();
                TCity = dt2.Rows[0]["City"].ToString();
                TState = dt2.Rows[0]["State"].ToString();
                TDob = dt2.Rows[0]["Dob"].ToString();
                TCreradob = dt2.Rows[0]["Creradob"].ToString();
                TCountry = dt2.Rows[0]["Country"].ToString();
                TBirthloc = dt2.Rows[0]["Birthloc"].ToString();
                TDod = dt2.Rows[0]["Dod"].ToString();
                TCreradod = dt2.Rows[0]["Creradod"].ToString();
                TNotes = dt2.Rows[0]["Notes"].ToString();
                TUserid = dt2.Rows[0]["Userid"].ToString();
                TPassword = dt2.Rows[0]["Password"].ToString();
            }
            string creradod = "";
            string creradob = "";
            //if (chb_dobCirca.Checked == true)
            //{
            //    creradob = "yes";
            //}
            string dob = "";
            string Motherfnamemember = "";
            string MotherLnamemember = "";
            string Fatherfnamemember = "";
            string FatherLnamemember = "";
            string Memberid = Session["Memberid"].ToString();
            string Motherparentmemid = "";
            string Fatherparentmemid = "";
            string parenttype = "";
            if (parenttypefrm == "Father" || parenttypefrm == "Mother")
            {
                SqlDataAdapter da3 = new SqlDataAdapter("select * from Tbl_ParentsOf where Parentsofid='" + Memidfrom + "'", con);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        parenttype = dt3.Rows[i]["Parentstype"].ToString();
                        if (parenttype == "Mother")
                        {
                            Motherfnamemember = dt3.Rows[i]["MemberFName"].ToString();
                            MotherLnamemember = dt3.Rows[i]["MemberLName"].ToString();
                            Motherparentmemid = dt3.Rows[i]["Memberid"].ToString();
                            dob = dt3.Rows[i]["Dob"].ToString();
                        }
                        if (parenttype == "Father")
                        {
                            Fatherfnamemember = dt3.Rows[i]["MemberFName"].ToString();
                            FatherLnamemember = dt3.Rows[i]["MemberLName"].ToString();
                            Fatherparentmemid = dt3.Rows[i]["Memberid"].ToString();
                            dob = dt3.Rows[i]["Dob"].ToString();
                        }
                    }
                }
            }


            if (parenttypefrm == "Father" || parenttypefrm == "Mother")
            {
                cmd = new SqlCommand("SPSpouseof", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
                cmd.Parameters.AddWithValue("id", "");
                cmd.Parameters.AddWithValue("Familyid", Tfamilyid);
                if (parenttypefrm == "Father")
                {
                    cmd.Parameters.AddWithValue("MemberFName", TFirstName);
                    cmd.Parameters.AddWithValue("MemberLName", TLastName);
                    cmd.Parameters.AddWithValue("Dob", TDob);
                    cmd.Parameters.AddWithValue("Gender", TGender);
                    cmd.Parameters.AddWithValue("Memberid", TMemberId);
                    cmd.Parameters.AddWithValue("SpouseoffName", Motherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", MotherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Motherparentmemid);
                }
                if (parenttypefrm == "Mother")
                {
                    cmd.Parameters.AddWithValue("MemberFName", TFirstName);
                    cmd.Parameters.AddWithValue("MemberLName", TLastName);
                    cmd.Parameters.AddWithValue("Dob", TDob);
                    cmd.Parameters.AddWithValue("Gender", TGender);
                    cmd.Parameters.AddWithValue("Memberid", TMemberId);
                    cmd.Parameters.AddWithValue("SpouseoffName", Fatherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", FatherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Fatherparentmemid);
                }

                cmd.Parameters.AddWithValue("Status", "Byrequest");
                cmd.Parameters.AddWithValue("EnterDate", date);
                cmd.Parameters.AddWithValue("EnterTime", time);
                cmd.Parameters.AddWithValue("Enterby", MemberId);
                cmd.Parameters.AddWithValue("StatementType", "Insert");
                con.Open();
                int k = 0;
                if (parenttypefrm == "Father")
                {
                    if (Motherparentmemid != "")
                    {
                        k = cmd.ExecuteNonQuery();
                    }
                }
                else if (parenttypefrm == "Mother")
                {
                    if (Fatherparentmemid != "")
                    {
                        k = cmd.ExecuteNonQuery();
                    }
                }

                if (k != 0)
                {
                    Session["Slno"] = null;
                }
                SqlCommand cmd1 = new SqlCommand("SPSpouseof", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
                cmd1.Parameters.AddWithValue("id", "");
                cmd1.Parameters.AddWithValue("Familyid", Tfamilyid);
                if (parenttypefrm == "Father")
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Motherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", MotherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", TDob);
                    cmd1.Parameters.AddWithValue("Gender", TGender);
                    cmd1.Parameters.AddWithValue("Memberid", Motherparentmemid);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Fatherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", FatherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", TDob);
                    cmd1.Parameters.AddWithValue("Gender", TGender);
                    cmd1.Parameters.AddWithValue("Memberid", Fatherparentmemid);
                }

                cmd1.Parameters.AddWithValue("SpouseoffName", TFirstName);
                cmd1.Parameters.AddWithValue("SpouseofLName", TLastName);
                cmd1.Parameters.AddWithValue("Spouseofid", TMemberId);
                cmd.Parameters.AddWithValue("Status", "Byrequest");
                cmd1.Parameters.AddWithValue("EnterDate", date);
                cmd1.Parameters.AddWithValue("EnterTime", time);
                cmd1.Parameters.AddWithValue("Enterby", MemberId);
                cmd1.Parameters.AddWithValue("StatementType", "Insert");
                //int k1 = cmd1.ExecuteNonQuery();
                int k1 = 0;
                if (parenttypefrm == "Father")
                {
                    if (Motherparentmemid != "")
                    {
                        k1 = cmd1.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (Fatherparentmemid != "")
                    {
                        k1 = cmd1.ExecuteNonQuery();
                    }
                }

                con.Close();
            }
        }

        protected void lnkbtn_rej_Click(object sender, EventArgs e)
        {
            string date = "";
            string time = DateTime.Now.ToString("hh:mm:ss");
            SqlConnection con = new SqlConnection(strcon);
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            string RowIndex = row.Cells[6].Text;
            string btnsts = null;
            string Query = "Select * from Tbl_AllRequests where id='" + RowIndex + "'";
            DataTable dt = RetriveData(Query);
            // string ordersts = dt.Rows[0]["Regstatus"].ToString();
            if (dt.Rows.Count > 0)
            {
                btnsts = dt.Rows[0]["Regstatus"].ToString();
                date = btnsts;
            }
            if (btnsts == "Reject")
            {
                date = DateTime.Now.ToString("yyyy/MM/dd");
                btnsts = "Rejected";
                GetparentsUpdate(RowIndex);
            }
            else if (btnsts == "Rejected")
            {
                //btnsts = "Accept";
            }
            Query = "update Tbl_AllRequests set Regstatus='" + btnsts + "',Status='" + date + "' where id='" + RowIndex + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            getrequest();
            if (btnsts == "Reject")
            {
                //check the request is comming for what
                //Check that member is already added as that or not
                //Add that member to all table as per request
            }
        }
        public void insertspouseof(string Memidfrom, string addingpermemid, string reqtyp)
        {
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Ffamilyid = "";
            string FMemberId = "";
            string FFirstName = "";
            string FLastName = "";
            string FContact = "";
            string FGender = "";
            string FEmailId = "";
            string FAddress = "";
            string FCity = "";
            string FState = "";
            string FDob = "";
            string FCreradob = "";
            string FCountry = "";
            string FBirthloc = "";
            string FDod = "";
            string FCreradod = "";
            string FNotes = "";
            string FUserid = "";
            string FPassword = "";

            string Tfamilyid = "";
            string TMemberId = "";
            string TFirstName = "";
            string TLastName = "";
            string TContact = "";
            string TGender = "";
            string TEmailId = "";
            string TAddress = "";
            string TCity = "";
            string TState = "";
            string TDob = "";
            string TCreradob = "";
            string TCountry = "";
            string TBirthloc = "";
            string TDod = "";
            string TCreradod = "";
            string TNotes = "";
            string TUserid = "";
            string TPassword = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memidfrom + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Ffamilyid = dt1.Rows[0]["FamilyId"].ToString();
                FMemberId = dt1.Rows[0]["MemberId"].ToString();
                FFirstName = dt1.Rows[0]["FirstName"].ToString();
                FLastName = dt1.Rows[0]["LastName"].ToString();
                FContact = dt1.Rows[0]["Contact"].ToString();
                FGender = dt1.Rows[0]["Gender"].ToString();
                FEmailId = dt1.Rows[0]["EmailId"].ToString();
                FAddress = dt1.Rows[0]["Address"].ToString();
                FCity = dt1.Rows[0]["City"].ToString();
                FState = dt1.Rows[0]["State"].ToString();
                FDob = dt1.Rows[0]["Dob"].ToString();
                FCreradob = dt1.Rows[0]["Creradob"].ToString();
                FCountry = dt1.Rows[0]["Country"].ToString();
                FBirthloc = dt1.Rows[0]["Birthloc"].ToString();
                FDod = dt1.Rows[0]["Dod"].ToString();
                FCreradod = dt1.Rows[0]["Creradod"].ToString();
                FNotes = dt1.Rows[0]["Notes"].ToString();
                FUserid = dt1.Rows[0]["Userid"].ToString();
                FPassword = dt1.Rows[0]["Password"].ToString();
            }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + addingpermemid + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                Tfamilyid = dt2.Rows[0]["FamilyId"].ToString();
                TMemberId = dt2.Rows[0]["MemberId"].ToString();
                TFirstName = dt2.Rows[0]["FirstName"].ToString();
                TLastName = dt2.Rows[0]["LastName"].ToString();
                TContact = dt2.Rows[0]["Contact"].ToString();
                TGender = dt2.Rows[0]["Gender"].ToString();
                TEmailId = dt2.Rows[0]["EmailId"].ToString();
                TAddress = dt2.Rows[0]["Address"].ToString();
                TCity = dt2.Rows[0]["City"].ToString();
                TState = dt2.Rows[0]["State"].ToString();
                TDob = dt2.Rows[0]["Dob"].ToString();
                TCreradob = dt2.Rows[0]["Creradob"].ToString();
                TCountry = dt2.Rows[0]["Country"].ToString();
                TBirthloc = dt2.Rows[0]["Birthloc"].ToString();
                TDod = dt2.Rows[0]["Dod"].ToString();
                TCreradod = dt2.Rows[0]["Creradod"].ToString();
                TNotes = dt2.Rows[0]["Notes"].ToString();
                TUserid = dt2.Rows[0]["Userid"].ToString();
                TPassword = dt2.Rows[0]["Password"].ToString();
            }

            string creradod = "";
            string creradob = "";
            if (TCreradob != "")
            {
                creradob = "yes";
            }
            string dob = "";

            string fnamemember = "";
            string Lnamemember = "";
            string Memberid = Session["Memberid"].ToString();
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                fnamemember = dt.Rows[0]["FirstName"].ToString();
                Lnamemember = dt.Rows[0]["LastName"].ToString();
            }
            if (TDob != "")
            {
                dob = TDob;
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("yyyy/MM/dd");
            }
            else
            {
                dob = DateTime.Now.ToString("yyyy/MM/dd");
            }
            cmd = new SqlCommand("SPSpouseof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("MemberFName", FFirstName);
            cmd.Parameters.AddWithValue("MemberLName", FLastName);
            cmd.Parameters.AddWithValue("Dob", dob);
            cmd.Parameters.AddWithValue("Gender", FGender);
            cmd.Parameters.AddWithValue("Memberid", FMemberId);
            cmd.Parameters.AddWithValue("SpouseoffName", TFirstName);
            cmd.Parameters.AddWithValue("SpouseofLName", TLastName);
            cmd.Parameters.AddWithValue("Spouseofid", TMemberId);
            cmd.Parameters.AddWithValue("Status", "Byrequest");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                // Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                //Response.Redirect("~/FamilyTree/MyTree1.aspx");
            }
            cmd = new SqlCommand("SPSpouseof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("MemberFName", TFirstName);
            cmd.Parameters.AddWithValue("MemberLName", TLastName);
            cmd.Parameters.AddWithValue("Dob", dob);
            cmd.Parameters.AddWithValue("Gender", FGender);
            cmd.Parameters.AddWithValue("Memberid", TMemberId);
            cmd.Parameters.AddWithValue("SpouseoffName", FFirstName);
            cmd.Parameters.AddWithValue("SpouseofLName", FLastName);
            cmd.Parameters.AddWithValue("Spouseofid", FMemberId);
            cmd.Parameters.AddWithValue("Status", "Byrequest");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            int k1 = cmd.ExecuteNonQuery();

            if (k1 != 0)
            {
                // Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                //Response.Redirect("~/FamilyTree/MyTree1.aspx");
            }
            con.Close();
        }
        public void insertparents(string from, string fors, string type, string spouseid, string spousename)
        {
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Ffamilyid = "";
            string FMemberId = "";
            string FFirstName = "";
            string FLastName = "";
            string FContact = "";
            string FGender = "";
            string FEmailId = "";
            string FAddress = "";
            string FCity = "";
            string FState = "";
            string FDob = "";
            string FCreradob = "";
            string FCountry = "";
            string FBirthloc = "";
            string FDod = "";
            string FCreradod = "";
            string FNotes = "";
            string FUserid = "";
            string FPassword = "";

            string Tfamilyid = "";
            string TMemberId = "";
            string TFirstName = "";
            string TLastName = "";
            string TContact = "";
            string TGender = "";
            string TEmailId = "";
            string TAddress = "";
            string TCity = "";
            string TState = "";
            string TDob = "";
            string TCreradob = "";
            string TCountry = "";
            string TBirthloc = "";
            string TDod = "";
            string TCreradod = "";
            string TNotes = "";
            string TUserid = "";
            string TPassword = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + from + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Ffamilyid = dt1.Rows[0]["FamilyId"].ToString();
                FMemberId = dt1.Rows[0]["MemberId"].ToString();
                FFirstName = dt1.Rows[0]["FirstName"].ToString();
                FLastName = dt1.Rows[0]["LastName"].ToString();
                FContact = dt1.Rows[0]["Contact"].ToString();
                FGender = dt1.Rows[0]["Gender"].ToString();
                FEmailId = dt1.Rows[0]["EmailId"].ToString();
                FAddress = dt1.Rows[0]["Address"].ToString();
                FCity = dt1.Rows[0]["City"].ToString();
                FState = dt1.Rows[0]["State"].ToString();
                FDob = dt1.Rows[0]["Dob"].ToString();
                FCreradob = dt1.Rows[0]["Creradob"].ToString();
                FCountry = dt1.Rows[0]["Country"].ToString();
                FBirthloc = dt1.Rows[0]["Birthloc"].ToString();
                FDod = dt1.Rows[0]["Dod"].ToString();
                FCreradod = dt1.Rows[0]["Creradod"].ToString();
                FNotes = dt1.Rows[0]["Notes"].ToString();
                FUserid = dt1.Rows[0]["Userid"].ToString();
                FPassword = dt1.Rows[0]["Password"].ToString();
            }
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + fors + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                Tfamilyid = dt2.Rows[0]["FamilyId"].ToString();
                TMemberId = dt2.Rows[0]["MemberId"].ToString();
                TFirstName = dt2.Rows[0]["FirstName"].ToString();
                TLastName = dt2.Rows[0]["LastName"].ToString();
                TContact = dt2.Rows[0]["Contact"].ToString();
                TGender = dt2.Rows[0]["Gender"].ToString();
                TEmailId = dt2.Rows[0]["EmailId"].ToString();
                TAddress = dt2.Rows[0]["Address"].ToString();
                TCity = dt2.Rows[0]["City"].ToString();
                TState = dt2.Rows[0]["State"].ToString();
                TDob = dt2.Rows[0]["Dob"].ToString();
                TCreradob = dt2.Rows[0]["Creradob"].ToString();
                TCountry = dt2.Rows[0]["Country"].ToString();
                TBirthloc = dt2.Rows[0]["Birthloc"].ToString();
                TDod = dt2.Rows[0]["Dod"].ToString();
                TCreradod = dt2.Rows[0]["Creradod"].ToString();
                TNotes = dt2.Rows[0]["Notes"].ToString();
                TUserid = dt2.Rows[0]["Userid"].ToString();
                TPassword = dt2.Rows[0]["Password"].ToString();
            }

            string creradod = "";
            string creradob = "";
            string dob = "";
            string Youare = "";
            string fnamemember = "";
            string Lnamemember = "";
            string Memberid = Session["Memberid"].ToString();
            SqlDataAdapter da3 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            if (dt3.Rows.Count > 0)
            {
                fnamemember = dt3.Rows[0]["FirstName"].ToString();
                Lnamemember = dt3.Rows[0]["LastName"].ToString();
            }
            if (FDob != "")
            {
                dob = FDob;
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("yyyy/MM/dd");
            }
            else
            {
                dob = DateTime.Now.ToString("yyyy/MM/dd");
            }
            cmd = new SqlCommand("SPChildrenof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Youare", Youare);
            if(Youare =="Father")
            {
                Youare = "Mother";
            }
            else if(Youare == "Mother")
            {
                Youare = "Father";
            }
            cmd.Parameters.AddWithValue("SpouseName", spousename);
            cmd.Parameters.AddWithValue("SpouseMemid", spouseid);
            cmd.Parameters.AddWithValue("MemberFName", TFirstName);
            cmd.Parameters.AddWithValue("MemberLName", TLastName);
            cmd.Parameters.AddWithValue("Dob", TDob);
            cmd.Parameters.AddWithValue("Gender", TGender);
            cmd.Parameters.AddWithValue("Memberid", TMemberId);
            cmd.Parameters.AddWithValue("ChildoffName", FFirstName);
            cmd.Parameters.AddWithValue("ChildofLName", FLastName);
            cmd.Parameters.AddWithValue("Childofid", FMemberId);
            cmd.Parameters.AddWithValue("Status",""); 
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                Session["Slno"] = null;
            }
            SqlDataAdapter da5 = new SqlDataAdapter("select * from MainMembers where MemberId='" + spouseid + "'", con);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            string Tfnname = "";
            string Tlname = "";
            string Tdob = "";
            string Tgender = "";
            if (dt5.Rows.Count > 0)
            {
                Tfnname = dt5.Rows[0]["FirstName"].ToString();
                Tlname = dt5.Rows[0]["LastName"].ToString();
                Tdob = dt5.Rows[0]["Dob"].ToString();
                Tgender = dt5.Rows[0]["Gender"].ToString();
            }
             
            cmd = new SqlCommand("SPChildrenof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Youare", Youare);
            cmd.Parameters.AddWithValue("SpouseName", FFirstName +" "+ FLastName);
            cmd.Parameters.AddWithValue("SpouseMemid", FMemberId);
            cmd.Parameters.AddWithValue("MemberFName", TFirstName);
            cmd.Parameters.AddWithValue("MemberLName", TLastName);
            cmd.Parameters.AddWithValue("Dob", TDob);
            cmd.Parameters.AddWithValue("Gender", TGender);
            cmd.Parameters.AddWithValue("Memberid", TMemberId);
            cmd.Parameters.AddWithValue("ChildoffName", Tfnname);
            cmd.Parameters.AddWithValue("ChildofLName", Tlname);
            cmd.Parameters.AddWithValue("Childofid", spouseid);
            cmd.Parameters.AddWithValue("Status", "");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            int k1 = cmd.ExecuteNonQuery();
            if (k1 != 0)
            {
                //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                //Response.Redirect("~/FamilyTree/MyTree1.aspx");
            } 
            //string FName = "";
            //string LName = "";
            //string MemberId = "";
            //string Chldmemid = "";
            //string Gender = "";
            //SqlDataAdapter da6 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
            //string gg = spouseid;
            //DataTable dt6 = new DataTable();
            //da6.Fill(dt6);
            //if (dt6.Rows.Count > 0)
            //{
            //    FName = dt6.Rows[0]["FirstName"].ToString();
            //    LName = dt6.Rows[0]["LastName"].ToString();
            //    MemberId = dt6.Rows[0]["MemberId"].ToString();
            //    Gender = dt6.Rows[0]["Gender"].ToString();
            //}
            //try
            //{
            //    Chldmemid = Session["Chilsnmemid"].ToString();
            //}
            //catch
            //{

            //}
            cmd = new SqlCommand("SPParentsof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Parentstype", type);
            cmd.Parameters.AddWithValue("MemberFName", FFirstName);
            cmd.Parameters.AddWithValue("MemberLName", FLastName);
            cmd.Parameters.AddWithValue("Dob", FDob);
            cmd.Parameters.AddWithValue("Gender", FGender);
            cmd.Parameters.AddWithValue("Memberid", FMemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", TFirstName);
            cmd.Parameters.AddWithValue("ParentsofLName", TLastName);
            cmd.Parameters.AddWithValue("Parentsofid", TMemberId);
            cmd.Parameters.AddWithValue("Status", "");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert"); 
            int k7 = cmd.ExecuteNonQuery();
            
            if (type == "Father")
            {
                type = "Mother";
            }
           else if (type == "Mother")
            {
                type = "Father";
            }
            else if (type == "Stepmother")
            {
                type = "Stepfather";
            }
            else if (type == "Stepfather")
            {
                type = "Stepmother";
            } 
            cmd = new SqlCommand("SPParentsof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Parentstype", type);
            cmd.Parameters.AddWithValue("MemberFName", Tfnname);
            cmd.Parameters.AddWithValue("MemberLName", Tlname);
            cmd.Parameters.AddWithValue("Dob", Tdob);
            cmd.Parameters.AddWithValue("Gender", Tgender);
            cmd.Parameters.AddWithValue("Memberid", spouseid);
            cmd.Parameters.AddWithValue("ParentsoffName", TFirstName);
            cmd.Parameters.AddWithValue("ParentsofLName", TLastName);
            cmd.Parameters.AddWithValue("Parentsofid", TMemberId);
            cmd.Parameters.AddWithValue("Status", "");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("StatementType", "Insert"); 
            int k8 = cmd.ExecuteNonQuery();
            if (k8 != 0)
            {
                Session["Slno"] = null;
            }
            con.Close();
        }

    }
}