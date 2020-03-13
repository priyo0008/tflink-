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

namespace TflinkTest.FamilyTree
{
    public partial class AddParents : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Memid = "";
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string memberidbysub = "";
        string familyid = "";
        string Memidcreatedoarents = ""; 
        string newid = "";
        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                familyid = Session["Familyid"].ToString();

            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }

            if (!IsPostBack)
            {
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Submitdata();
                insertinspouseof();
                insertChild();
                insertparentinspouseof();
                insertparentinspouseofGF();
            }


            catch
            {
            }
            //  insertagaininspouseof();
        }
        public void Submitdata()
        {
            SqlConnection con = new SqlConnection(strcon);

            string Fid = familyid;
            Memid = GetMemberid();
            Memidcreatedoarents = Memid.ToString();
            string Userid = "";
            string Password = "";
            string curadm = "";
            try
            {
                if (Session["UserId"] != null && Session["Password"] != null)
                {
                    Userid = Session["UserId"].ToString();
                    Password = Session["Password"].ToString();
                }
            }
            catch
            {

            }
            try
            {
                curadm = Session["MemberId"].ToString();
            }
            catch
            {

            }
            string creradod = "";
            string creradob = "";
            if (chb_dobCirca.Checked == true)
            {
                creradob = "yes";
            }
            if (chb_dodCirca.Checked == true)
            {
                creradod = "yes";
            }
            string dob = "";
            string dod = "";
            if (txt_Dob.Text != "")
            {
                dob = txt_Dob.Text.Trim();
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("yyyy/MM/dd");
            }
            else
            {
                dob = DateTime.Now.ToString("yyyy/MM/dd");
            }
            if (txt_Dod.Text != "")
            {
                dod = txt_Dod.Text.Trim();
                DateTime date2 = Convert.ToDateTime(dod.ToString());
                dod = date2.ToString("yyyy/MM/dd");
            }
            else
            {
                dod = DateTime.Now.ToString("yyyy/MM/dd");
            }
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            cmd = new SqlCommand("SPMainMembers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", Userid);
            cmd.Parameters.AddWithValue("FamilyId", Fid);
            cmd.Parameters.AddWithValue("MemberId", Memid);
            cmd.Parameters.AddWithValue("FirstName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("LastName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Contact", txt_Phonenmb.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
            cmd.Parameters.AddWithValue("EmailId", txt_Email.Text.Trim());
            cmd.Parameters.AddWithValue("Address", txt_address.Text.Trim());
            cmd.Parameters.AddWithValue("City", txt_City.Text.Trim());
            cmd.Parameters.AddWithValue("State", txt_State.Text.Trim());
            cmd.Parameters.AddWithValue("Dob", dob);
            cmd.Parameters.AddWithValue("Creradob", creradob);
            cmd.Parameters.AddWithValue("Country", ddl_Country.Text.Trim());
            cmd.Parameters.AddWithValue("Birthloc", txt_Birthloc.Text.Trim());
            cmd.Parameters.AddWithValue("Dod", dod);
            cmd.Parameters.AddWithValue("Creradod", creradod);
            cmd.Parameters.AddWithValue("Notes", txt_notes.Text.Trim());
            cmd.Parameters.AddWithValue("Userid", txt_Phonenmb.Text.Trim());
            cmd.Parameters.AddWithValue("Password", Fid + newid);
            cmd.Parameters.AddWithValue("Enterby", curadm);
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Entertime", time);
            Session["Chilsnmemid"] = Memid.ToString();
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Updated Successfully</div> ";
                Bindnotice.InnerHtml = line.ToString();
            }
            else if (btn_save.Text == "Save")
            {
                cmd.Parameters.AddWithValue("StatementType", "Insert");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Saved Successfully</div> ";
                Bindnotice.InnerHtml = line.ToString();
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {

                //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
            }
            else
            {
                string line = "<div class='alert alert-danger'><strong> Success!</strong> Submit Failed</div> ";
                Bindnotice.InnerHtml = line.ToString();
            }

            con.Close();

         
        }
        public string Getfamilyid()
        {
            SqlConnection con = new SqlConnection(strcon);
            string id = "";
            int coun = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                string Query = "select * from MainMembers where FamilyId LIKE 'Family00%' ";
                DataTable dt = RetriveData(Query);
                if (dt.Rows.Count > 0)
                {
                    coun = dt.Rows.Count + 1;
                    id = "Family00" + coun; 
                 
                }
                else
                {
                    id = "Family00" + 1;
                   
                }

            }
            catch
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return id;
        }
        public DataTable RetriveData(string Query)
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            try
            {

                da = new SqlDataAdapter(Query, con);

                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public string GetMemberid()
        {
            SqlConnection con = new SqlConnection(strcon);
            string id = "";
            string getstr = "";

            int coun = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                string Query = "select * from MainMembers where MemberId LIKE 'Memberid%' ";
                DataTable dt = RetriveData(Query);
                if (dt.Rows.Count > 0)
                {
                    coun = dt.Rows.Count + 1;
                    getstr = dt.Rows[0]["MemberId"].ToString();
                    getstr = getstr.Substring(getstr.Length - 1);
                    id = "Memberid" + coun;
                    newid = coun.ToString();

                }
                else
                {
                    id = "Memberid" + 1;
                    newid = "1";
                }
            }
            catch
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return id;
        }
        public void insertinspouseof()
        {
            try
            {
                Memid = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string creradod = "";
            string creradob = "";
            string Chldmemid = "";
            if (chb_dobCirca.Checked == true)
            {
                creradob = "yes";
            }
            string dob = "";
            string fnamemember = "";
            string Lnamemember = "";
            string Memberid = "";
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                Memberid = Session["Memberid"].ToString();
            }
            catch
            {

            }

            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                fnamemember = dt1.Rows[0]["FirstName"].ToString();
                Lnamemember = dt1.Rows[0]["LastName"].ToString();
            }
            if (txt_Dob.Text != "")
            {
                dob = txt_Dob.Text.Trim();
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("yyyy/MM/dd");
            }
            else
            {
                dob = DateTime.Now.ToString("yyyy/MM/dd");
            }
            try
            {
                Chldmemid = Session["Chilsnmemid"].ToString();
            }
            catch
            {

            }
            cmd = new SqlCommand("SPParentsof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Parentstype", ddl_Parentstype.Text);
            cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
            cmd.Parameters.AddWithValue("Memberid", Chldmemid);
            cmd.Parameters.AddWithValue("ParentsoffName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("ParentsofLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Parentsofid", Memid);
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Save")
            {
                cmd.Parameters.AddWithValue("StatementType", "Insert");
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                Session["Slno"] = null;
            }
            con.Close();
        }
        public void insertChild()
        {
            if (Memidcreatedoarents != "")
            {
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                string time = DateTime.Now.ToString("hh:mm:ss");
                string creradod = "";
                string creradob = "";
                if (chb_dobCirca.Checked == true)
                {
                    creradob = "yes";
                }
                string dob = "";
                string fnamemember = "";
                string Lnamemember = "";
                string memberdob = "";
                string Gender = "";
                string spousename = "";
                string spousememid = "";
                SqlConnection con = new SqlConnection(strcon);
                string Memberid = Session["Memberid"].ToString();
                SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    fnamemember = dt1.Rows[0]["FirstName"].ToString();
                    Lnamemember = dt1.Rows[0]["LastName"].ToString();
                    memberdob = dt1.Rows[0]["Dob"].ToString();
                    Gender = dt1.Rows[0]["Gender"].ToString();

                }
                if (txt_Dob.Text != "")
                {
                    dob = txt_Dob.Text.Trim();
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
                cmd.Parameters.AddWithValue("Youare", ddl_Parentstype.Text.Trim());
                cmd.Parameters.AddWithValue("SpouseName", "");
                cmd.Parameters.AddWithValue("SpouseMemid", "");
                cmd.Parameters.AddWithValue("MemberFName", fnamemember);
                cmd.Parameters.AddWithValue("MemberLName", Lnamemember);
                cmd.Parameters.AddWithValue("Dob", memberdob);
                cmd.Parameters.AddWithValue("Gender", Gender);
                cmd.Parameters.AddWithValue("Memberid", Memberid);
                cmd.Parameters.AddWithValue("ChildoffName", txt_Fname.Text.Trim());//Got values from Parents
                cmd.Parameters.AddWithValue("ChildofLName", txt_Lname.Text.Trim());//
                cmd.Parameters.AddWithValue("Childofid", Memidcreatedoarents);//
                cmd.Parameters.AddWithValue("EnterDate", date);
                cmd.Parameters.AddWithValue("EnterTime", time);
                cmd.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Save")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Insert");
                }
                con.Open();
                int k = cmd.ExecuteNonQuery();
                Fillspousedetails(Memidcreatedoarents);
                if (k != 0)
                {
                    //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                    Session["Slno"] = null;
                    //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                    //Response.Redirect("~/FamilyTree/MyTree1.aspx");
                }
                con.Close();
            }
            else
            {

            }
        }
        public void insertparentinspouseof()
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string creradod = "";
            string creradob = "";
            if (chb_dobCirca.Checked == true)
            {
                creradob = "yes";
            }
            string dob = "";
            string Motherfnamemember = "";
            string MotherLnamemember = "";
            string Fatherfnamemember = "";
            string FatherLnamemember = "";
            string Memberid = Session["Memberid"].ToString();
            string Motherparentmemid = "";
            string Fatherparentmemid = "";
            SqlConnection con = new SqlConnection(strcon);
            string parenttype = "";
            if (ddl_Parentstype.Text.Trim() == "Father")
            {
                SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_ParentsOf where Parentsofid='" + Memberid + "'", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        parenttype = dt2.Rows[i]["Parentstype"].ToString();
                        if (parenttype == "Mother")
                        {
                            Motherfnamemember = dt2.Rows[i]["MemberFName"].ToString();
                            MotherLnamemember = dt2.Rows[i]["MemberLName"].ToString();
                            Motherparentmemid = dt2.Rows[i]["Memberid"].ToString();
                        }
                        if (parenttype == "Father")
                        {
                            Fatherfnamemember = dt2.Rows[i]["MemberFName"].ToString();
                            FatherLnamemember = dt2.Rows[i]["MemberLName"].ToString();
                            Fatherparentmemid = dt2.Rows[i]["Memberid"].ToString();

                        }
                    }
                }
            }
            //Test 
            //Memberid = Session["Memberid"].ToString();
            if (ddl_Parentstype.Text.Trim() == "Father" || ddl_Parentstype.Text.Trim() == "Mother")
            {

                if (txt_Dob.Text != "")
                {
                    dob = txt_Dob.Text.Trim();
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
                if (ddl_Parentstype.Text.Trim() == "Father")
                {
                    cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
                    cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd.Parameters.AddWithValue("Memberid", Memidcreatedoarents);
                    cmd.Parameters.AddWithValue("SpouseoffName", Motherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", MotherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Motherparentmemid);
                }
                if (ddl_Parentstype.Text.Trim() == "Mother")
                {
                    cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
                    cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd.Parameters.AddWithValue("Memberid", Memidcreatedoarents);
                    cmd.Parameters.AddWithValue("SpouseoffName", Fatherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", FatherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Fatherparentmemid);
                }

                cmd.Parameters.AddWithValue("EnterDate", date);
                cmd.Parameters.AddWithValue("EnterTime", time);
                cmd.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Save")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Insert");
                }
                con.Open();
                int k = 0;
                //if (ddl_Parentstype.Text.Trim() == "Father")
                //{
                //    if (Motherparentmemid != "")
                //    {
                        k = cmd.ExecuteNonQuery();
                //    }
                //}
                //else if (ddl_Parentstype.Text.Trim() == "Mother")
                //{
                //    if (Fatherparentmemid != "")
                //    {
                //        k = cmd.ExecuteNonQuery();
                //    }
                //}


                if (k != 0)
                {
                    //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                    Session["Slno"] = null;
                    //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                    //Response.Redirect("~/FamilyTree/MyTree1.aspx");
                }
                SqlCommand cmd1 = new SqlCommand("SPSpouseof", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
                cmd1.Parameters.AddWithValue("id", "");
                cmd1.Parameters.AddWithValue("Familyid", familyid);
                if (ddl_Parentstype.Text.Trim() == "Father")
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Motherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", MotherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", dob);
                    cmd1.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd1.Parameters.AddWithValue("Memberid", Motherparentmemid);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Fatherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", FatherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", dob);
                    cmd1.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd1.Parameters.AddWithValue("Memberid", Fatherparentmemid);
                }

                cmd1.Parameters.AddWithValue("SpouseoffName", txt_Fname.Text.Trim());
                cmd1.Parameters.AddWithValue("SpouseofLName", txt_Lname.Text.Trim());
                cmd1.Parameters.AddWithValue("Spouseofid", Memidcreatedoarents);
                cmd1.Parameters.AddWithValue("EnterDate", date);
                cmd1.Parameters.AddWithValue("EnterTime", time);
                cmd1.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Save")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Insert");
                }
                //int k1 = cmd1.ExecuteNonQuery();
                int k1 = 0;
                //if (ddl_Parentstype.Text.Trim() == "Father")
                //{
                //    if (Motherparentmemid != "")
                //    {
                        k1 = cmd1.ExecuteNonQuery();
                //    }
                //}
                //else
                //{
                //    if (Fatherparentmemid != "")
                //    {
                //        k1 = cmd1.ExecuteNonQuery();
                //    }
                //}

                con.Close();
            }
        }
        public void insertparentinspouseofGF()
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string creradod = "";
            string creradob = "";
            if (chb_dobCirca.Checked == true)
            {
                creradob = "yes";
            }
            string dob = "";
            string Motherfnamemember = "";
            string MotherLnamemember = "";
            string Fatherfnamemember = "";
            string FatherLnamemember = "";
            string Memberid = Session["Memberid"].ToString();
            string Motherparentmemid = "";
            string Fatherparentmemid = "";
            SqlConnection con = new SqlConnection(strcon);
            string parenttype = "";
            if (ddl_Parentstype.Text.Trim() == "Father")
            {
                SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_ParentsOf where Parentsofid='" + Memberid + "'", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        parenttype = dt2.Rows[i]["Parentstype"].ToString();
                        if (parenttype == "Stepmother")
                        {
                            Motherfnamemember = dt2.Rows[i]["MemberFName"].ToString();
                            MotherLnamemember = dt2.Rows[i]["MemberLName"].ToString();
                            Motherparentmemid = dt2.Rows[i]["Memberid"].ToString();
                        }
                        if (parenttype == "Stepfather")
                        {
                            Fatherfnamemember = dt2.Rows[i]["MemberFName"].ToString();
                            FatherLnamemember = dt2.Rows[i]["MemberLName"].ToString();
                            Fatherparentmemid = dt2.Rows[i]["Memberid"].ToString();

                        }
                    }
                }
            }
            //Test 
            //Memberid = Session["Memberid"].ToString();
            if (ddl_Parentstype.Text.Trim() == "Stepfather" || ddl_Parentstype.Text.Trim() == "Stepmother")
            {
                if (txt_Dob.Text != "")
                {
                    dob = txt_Dob.Text.Trim();
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
                if (ddl_Parentstype.Text.Trim() == "Father")
                {
                    cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
                    cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd.Parameters.AddWithValue("Memberid", Memidcreatedoarents);
                    cmd.Parameters.AddWithValue("SpouseoffName", Motherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", MotherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Motherparentmemid);
                }
                if (ddl_Parentstype.Text.Trim() == "Mother")
                {
                    cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
                    cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
                    cmd.Parameters.AddWithValue("Dob", dob);
                    cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd.Parameters.AddWithValue("Memberid", Memidcreatedoarents);
                    cmd.Parameters.AddWithValue("SpouseoffName", Fatherfnamemember);
                    cmd.Parameters.AddWithValue("SpouseofLName", FatherLnamemember);
                    cmd.Parameters.AddWithValue("Spouseofid", Fatherparentmemid);
                }

                cmd.Parameters.AddWithValue("EnterDate", date);
                cmd.Parameters.AddWithValue("EnterTime", time);
                cmd.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Save")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Insert");
                }
                con.Open();
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                    Session["Slno"] = null;
                    //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                    //Response.Redirect("~/FamilyTree/MyTree1.aspx");
                }
                SqlCommand cmd1 = new SqlCommand("SPSpouseof", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
                cmd1.Parameters.AddWithValue("id", "");
                cmd1.Parameters.AddWithValue("Familyid", familyid);
                if (ddl_Parentstype.Text.Trim() == "Stepfather")
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Motherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", MotherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", dob);
                    cmd1.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd1.Parameters.AddWithValue("Memberid", Motherparentmemid);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("MemberFName", Fatherfnamemember);
                    cmd1.Parameters.AddWithValue("MemberLName", FatherLnamemember);
                    cmd1.Parameters.AddWithValue("Dob", dob);
                    cmd1.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
                    cmd1.Parameters.AddWithValue("Memberid", Fatherparentmemid);
                }

                cmd1.Parameters.AddWithValue("SpouseoffName", txt_Fname.Text.Trim());
                cmd1.Parameters.AddWithValue("SpouseofLName", txt_Lname.Text.Trim());
                cmd1.Parameters.AddWithValue("Spouseofid", Memidcreatedoarents);
                cmd1.Parameters.AddWithValue("EnterDate", date);
                cmd1.Parameters.AddWithValue("EnterTime", time);
                cmd1.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Save")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Insert");
                }
                int k1 = cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Fillspousedetails(string memid)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_ChildrenOf where Childofid='" + memid + "'", con);
            DataTable dt2 = new DataTable();
            string checkid = "";
            string checkide = "";
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    checkid = dt2.Rows[i]["MemberFName"].ToString();
                    if (checkid == "")
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_SpouseOf where Childofid='" + memid + "'", con);
                        DataTable dt1 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                        {

                        }
                    }
                }
            }
        }
        public string getspouseupdate(string memid)
        {
            string status = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_SpouseOf where Memberid='" + memid + "'", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {

            }
            else
            {
                status = "Yes";
            }
                return status;
        } 
        protected void txt_Phonenmb_TextChanged(object sender, EventArgs e)
        {
            //SqlConnection con = connect.GetEMployeeconnnect();
            SqlConnection con = new SqlConnection(strcon);
            string Uderid = "";
            string password = "";
            string Fname = "";
            int cn = 0;
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where Userid='" + txt_Phonenmb.Text.Trim() + "'", con);
            string Query = "select * from MainMembers where Userid='" + txt_Phonenmb.Text.Trim() + "'";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_Phonenmb.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Contact Number Already Exist!');", true);
            }
            else
            {

            }
        }
    }
}