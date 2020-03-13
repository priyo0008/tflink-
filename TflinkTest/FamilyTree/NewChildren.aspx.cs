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
    public partial class NewChildren : System.Web.UI.Page
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
        string newid = "";

        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userid = Session["UserId"].ToString();
                familyid = Session["Familyid"].ToString();
            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }
            if (!IsPostBack)
            {
                bindDropdn();
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if(txt_Mothername.Text.Trim() !="")
            {
                Submitdata();
                insertinspouseof();
                insertinspouseof2nd(ddl_Mothername.Text.Trim());
                insertinsParentsof();
                insertinsParentsofF();
                // this.Page.ClientScript.RegisterStartupScript(typeof(AddParents), "closeThickBox", "self.parent.updateScreen();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
            }
            else
            {
                Response.Write("<script>alert('Add spouse and try again.')</script>");
            }
          
        }
        public void Submitdata()
        {
            SqlConnection con = new SqlConnection(strcon);

            string Fid = familyid;
            Memid = GetMemberid();
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
            cmd.Parameters.AddWithValue("Password", Fid+newid);
            cmd.Parameters.AddWithValue("Enterby", curadm);
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Entertime", time);

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
                Session["Chilsnmemid"] = id.ToString();

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
            SqlConnection con = new SqlConnection(strcon);
            string Memberid = Session["Memberid"].ToString();
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
            cmd = new SqlCommand("SPChildrenof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Youare", ddl_Youare.Text.Trim());
            cmd.Parameters.AddWithValue("SpouseName", txt_Mothername.Text.Trim());
            cmd.Parameters.AddWithValue("SpouseMemid", ddl_Mothername.Text.Trim());
            cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Dob", dob);
            cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
            cmd.Parameters.AddWithValue("Memberid", Memid);
            cmd.Parameters.AddWithValue("ChildoffName", fnamemember);
            cmd.Parameters.AddWithValue("ChildofLName", Lnamemember);
            cmd.Parameters.AddWithValue("Childofid", Memberid);
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
            con.Close();
        }
        public void insertinspouseof2nd(string Memberid)
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
            SqlConnection con = new SqlConnection(strcon);
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
            cmd = new SqlCommand("SPChildrenof", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Youare", ddl_Youare.Text.Trim());
            cmd.Parameters.AddWithValue("SpouseName", txt_Mothername.Text.Trim());
            cmd.Parameters.AddWithValue("SpouseMemid", ddl_Mothername.Text.Trim());
            cmd.Parameters.AddWithValue("MemberFName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("MemberLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Dob", dob);
            cmd.Parameters.AddWithValue("Gender", ddl_gender.Text.Trim());
            cmd.Parameters.AddWithValue("Memberid", Memid);
            cmd.Parameters.AddWithValue("ChildoffName", fnamemember);
            cmd.Parameters.AddWithValue("ChildofLName", Lnamemember);
            cmd.Parameters.AddWithValue("Childofid", Memberid);
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
            con.Close();
        }

        public void bindDropdn()
        {
            string Memberid = "";
            try
            {
                Memberid = Session["Memberid"].ToString();
            }
            catch
            {

            }

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("select * from Tbl_SpouseOf where Spouseofid='" + Memberid + "'");

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            ddl_Mothername.DataSource = cmd.ExecuteReader();
            ddl_Mothername.DataTextField = "Memberid";
            ddl_Mothername.DataValueField = "Memberid";
            ddl_Mothername.DataBind();
            con.Close();
            ddl_Mothername.Items.Insert(0, new ListItem("--Select Spouse Id--", "0"));
        }
        protected void ddl_Mothername_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            string FName = "";
            string LName = "";
            string MemberId = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + ddl_Mothername.Text.Trim() + "'", con);
            string gg = ddl_Mothername.DataValueField.Trim();
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                FName = dt1.Rows[0]["FirstName"].ToString();
                LName = dt1.Rows[0]["LastName"].ToString();
                MemberId = dt1.Rows[0]["MemberId"].ToString();
                txt_Mothername.Text = FName + "  " + LName;
            }
        }

        //string MemberiidMoth = ddl_Mothername.Text.Trim();
        public void insertinsParentsof()
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Memberid = Session["Memberid"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            string FName = "";
            string LName = "";
            string MemberId = "";
            string Chldmemid = "";
            string Gender = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + Memberid + "'", con);
            string gg = ddl_Mothername.DataValueField.Trim();
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                FName = dt1.Rows[0]["FirstName"].ToString();
                LName = dt1.Rows[0]["LastName"].ToString();
                MemberId = dt1.Rows[0]["MemberId"].ToString();
                Gender = dt1.Rows[0]["Gender"].ToString();
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
            cmd.Parameters.AddWithValue("Parentstype", ddl_Youare.Text);
            cmd.Parameters.AddWithValue("MemberFName", FName);
            cmd.Parameters.AddWithValue("MemberLName", LName);
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", Gender);
            cmd.Parameters.AddWithValue("Memberid", MemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("ParentsofLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Parentsofid", Chldmemid);
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
        public void insertinsParentsofF()
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string Memberid = Session["Memberid"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            string FName = "";
            string LName = "";
            string MemberId = "";
            string Chldmemid = "";
            string Gender = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + ddl_Mothername.Text.Trim() + "'", con);
            string gg = ddl_Mothername.DataValueField.Trim();
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                FName = dt1.Rows[0]["FirstName"].ToString();
                LName = dt1.Rows[0]["LastName"].ToString();
                MemberId = dt1.Rows[0]["MemberId"].ToString();
                Gender = dt1.Rows[0]["Gender"].ToString();
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
            cmd.Parameters.AddWithValue("Parentstype", ddl_Youare.Text);
            cmd.Parameters.AddWithValue("MemberFName", FName);
            cmd.Parameters.AddWithValue("MemberLName", LName);
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", Gender);
            cmd.Parameters.AddWithValue("Memberid", MemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("ParentsofLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Parentsofid", Chldmemid);
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