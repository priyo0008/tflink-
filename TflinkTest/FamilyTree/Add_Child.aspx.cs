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
using System.Web.Services;
using System.Globalization;

namespace TflinkTest.FamilyTree
{
    public partial class Add_Child : System.Web.UI.Page
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
        string status = "";
        string Paretnstypecheck = "";

        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userid = Session["UserId"].ToString();
                familyid = Session["Familyid"].ToString();

            }
            catch
            {
           
            }
            if (!IsPostBack)
            {
                bindDropdn();
            }
            if (!IsPostBack)
            {
                ddl_Country.DataSource = CountryList();
                ddl_Country.DataBind();
            }
            try
            {
                status = Request.QueryString["status"];
                if (status == "Sent")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
            catch
            {

            }

        }
        public static List<string> CountryList()
        {
            //Creating list
            List<string> CultureList = new List<string>();

            //getting  the specific  CultureInfo from CultureInfo class
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            CultureList.Add("[Not Specified]");
            foreach (CultureInfo getCulture in getCultureInfo)
            {
                //creating the object of RegionInfo class
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
                //adding each county Name into the arraylist
                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            //sorting array by using sort method to get countries in order
            CultureList.Sort();
            //returning country list
            return CultureList;
        }
        [WebMethod]

        [System.Web.Script.Services.ScriptMethod()]

        public static string[] GetCompletionListsubcatagory(string prefixText, int count)
        {
            return AutoFillProductssubcatagory(prefixText);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        private static string[] AutoFillProductssubcatagory(string prefixText)
        {
            string comid = "";
            List<string> txtItems = new List<string>();
            try
            {
                comid = HttpContext.Current.Session["Familyid"].ToString();
            }
            catch
            {

            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString);

            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            com.Connection = con;
            con.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = "select * from MainMembers where " + "FirstName like @Search + '%' and FamilyId !='" + comid + "' ";
            com.Parameters.AddWithValue("@Search", prefixText);
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string faname = "";
                    //string lname = "";
                    //string memid = "";
                    //String From DataBase(dbValues)
                    faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    //memid = row["MemberId"].ToString();
                    txtItems.Add(faname);
                }
            }
            SqlCommand com1 = new SqlCommand();
            DataTable dt1 = new DataTable();
            com1.Connection = con;
            con.Open();
            com1.CommandType = CommandType.Text;
            com1.CommandText = "select * from MainMembers where " + " MemberId like @Search + '%'  and FamilyId !='" + comid + "' ";
            com1.Parameters.AddWithValue("@Search", prefixText);
            com1.ExecuteNonQuery();
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            da1.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    // string faname = "";
                    //string lname = "";
                    string memid = "";
                    //String From DataBase(dbValues)
                    //  faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    memid = row["MemberId"].ToString();
                    txtItems.Add(memid);
                }
            }
            SqlCommand com2 = new SqlCommand();
            DataTable dt2 = new DataTable();
            com2.Connection = con;
            con.Open();
            com2.CommandType = CommandType.Text;
            com2.CommandText = "select * from MainMembers where " + " Contact like @Search + '%'  and FamilyId !='" + comid + "' ";
            com2.Parameters.AddWithValue("@Search", prefixText);
            com2.ExecuteNonQuery();
            SqlDataAdapter da2 = new SqlDataAdapter(com2);
            da2.Fill(dt2);
            con.Close();
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow row in dt2.Rows)
                {
                    // string faname = "";
                    //string lname = "";
                    string memid = "";
                    //String From DataBase(dbValues)
                    //  faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    memid = row["Contact"].ToString();
                    txtItems.Add(memid);
                }
            }

            return txtItems.ToArray();
        }

        //public void bindDropdn()
        //{
        //    string Memberid = "";
        //    try
        //    {
        //        Memberid = Session["Memberid"].ToString();
        //    }
        //    catch
        //    {

        //    }

        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlCommand cmd = new SqlCommand("select * from Tbl_SpouseOf where Spouseofid='" + Memberid + "'");

        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = con;
        //    con.Open();
        //    ddl_Mothername.DataSource = cmd.ExecuteReader();
        //    ddl_Mothername.DataTextField = "Memberid";
        //    ddl_Mothername.DataValueField = "Memberid";
        //    ddl_Mothername.DataBind();
        //    con.Close();
        //    ddl_Mothername.Items.Insert(0, new ListItem("--Select Spouse Id--", "0"));
        //}

        protected void ddl_Mothername_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            string FName = "";
            string LName = "";
            string MemberId = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_SpouseOf where Memberid='" + ddl_Mothername.Text.Trim() + "'", con);
            string gg = ddl_Mothername.DataValueField.Trim();
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                FName = dt1.Rows[0]["MemberFName"].ToString();
                LName = dt1.Rows[0]["MemberLName"].ToString();
                MemberId = dt1.Rows[0]["Memberid"].ToString();
                txt_Mothername.Text = FName + "  " + LName;
            }
        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            getgrd();
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_youare.Text.Trim() == "Select")
            {
                Response.Write("<script>alert('Select Parent Type.')</script>");
            }
            else if (txt_Mothername.Text.Trim() == "")
            {
                Response.Write("<script>alert('Select Spouse.')</script>");
            }
            else if (txt_Mothername.Text.Trim() != "") 
            {
                Submitdata();
                //adding child
                insertinspouseof();
                //adding child
                insertinspouseof2nd(ddl_Mothername.Text.Trim());
                //adding parent 
                insertinsParentsof();
                //adding parents
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
                curadm = Session["Mainmemid"].ToString();
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
                creradob= "Not Available";
            }
            if (txt_Dod.Text != "")
            {
                dod = txt_Dod.Text.Trim();
                DateTime date2 = Convert.ToDateTime(dod.ToString());
                dod = date2.ToString("yyyy/MM/dd");
                creradod = "Available";
            }
            else
            {
                creradod = "Not Available";
                dod = DateTime.Now.ToString("yyyy/MM/dd");
         
            }
            if (chb_dobCirca.Checked == true)
            {
                creradob = "yes";
            }
            if (chb_dodCirca.Checked == true)
            {
                creradod = "yes";
            }
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string userid = "";
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
            cmd.Parameters.AddWithValue("Deathloc", txt_deathloc.Text.Trim());
            cmd.Parameters.AddWithValue("Notes", txt_notes.Text.Trim());
            userid = txt_Phonenmb.Text.Trim();
            if (txt_Phonenmb.Text.Trim() == "")
            {
                userid = Memid;
            }
            cmd.Parameters.AddWithValue("Userid", userid);
            cmd.Parameters.AddWithValue("Password", SomeStaticClass.encrypt(Fid + newid));
            cmd.Parameters.AddWithValue("Enterby", curadm);
            cmd.Parameters.AddWithValue("Wrngpswatmpt", "0");
            cmd.Parameters.AddWithValue("WrnpswDate", date);
            cmd.Parameters.AddWithValue("WrnpswTime", time); 
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Entertime", time);

            if (Button1.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Updated Successfully</div> ";
                Bindnotice.InnerHtml = line.ToString();
            }
            else if (Button1.Text == "Save")
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
            cmd.Parameters.AddWithValue("Youare", ddl_youare.Text.Trim());
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
            cmd.Parameters.AddWithValue("Status", "DirectAdd");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Send")
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
            cmd.Parameters.AddWithValue("Youare", ddl_youare.Text.Trim());
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
            cmd.Parameters.AddWithValue("Status", "DirectAdd");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Send")
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
        //protected void ddl_Mothername_TextChanged(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(strcon);
        //    string FName = "";
        //    string LName = "";
        //    string MemberId = "";
        //    SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + ddl_Mothername.Text.Trim() + "'", con);
        //    string gg = ddl_Mothername.DataValueField.Trim();
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    if (dt1.Rows.Count > 0)
        //    {
        //        FName = dt1.Rows[0]["FirstName"].ToString();
        //        LName = dt1.Rows[0]["LastName"].ToString();
        //        MemberId = dt1.Rows[0]["MemberId"].ToString();
        //        txt_Mothername.Text = FName + "  " + LName;
        //    }
        //}

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
            cmd.Parameters.AddWithValue("Parentstype", ddl_youare.Text);
            cmd.Parameters.AddWithValue("MemberFName", FName);
            cmd.Parameters.AddWithValue("MemberLName", LName);
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", Gender); 
            cmd.Parameters.AddWithValue("Memberid", MemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("ParentsofLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Parentsofid", Chldmemid);
            cmd.Parameters.AddWithValue("Status", "DirectAdd");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Send")
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
            if(ddl_youare.Text.Trim() == "Father")
            {
                Paretnstypecheck = "Mother";
            }
            else if (ddl_youare.Text.Trim() == "Mother")
            {
                Paretnstypecheck = "Father";
            }
            else if (ddl_youare.Text.Trim() == "Stepmother")
            {
                Paretnstypecheck = "Stepfather";
            }
            else if (ddl_youare.Text.Trim() == "Stepfather")
            {
                Paretnstypecheck = "Stepmother";
            }
             
            cmd.Parameters.AddWithValue("Parentstype", Paretnstypecheck);
            cmd.Parameters.AddWithValue("MemberFName", FName);
            cmd.Parameters.AddWithValue("MemberLName", LName);
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", Gender);
            cmd.Parameters.AddWithValue("Memberid", MemberId);
            cmd.Parameters.AddWithValue("ParentsoffName", txt_Fname.Text.Trim());
            cmd.Parameters.AddWithValue("ParentsofLName", txt_Lname.Text.Trim());
            cmd.Parameters.AddWithValue("Parentsofid", Chldmemid);
            cmd.Parameters.AddWithValue("Status", "DirectAdd");
            cmd.Parameters.AddWithValue("EnterDate", date);
            cmd.Parameters.AddWithValue("EnterTime", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Send")
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

        protected void btn_Savee_Click(object sender, EventArgs e)
        {
            string lblvalue = "";
            string reqfor = "";
            string memid = "";
            string reqto = "";
            string fstname = "";
            string lstname = "";
            SqlConnection con = new SqlConnection(strcon);
            if (ddl_youare.Text.Trim() == "Select")
            {
                Response.Write("<script>alert('Select Parent Type.')</script>");
            }
            else if (txt_Mothername.Text.Trim() == "")
            {
                Response.Write("<script>alert('Select Spouse.')</script>");
            }
            else
            {
                for (int i = 0; i < grd_bindmembers.Rows.Count; i++)
                {
                    RadioButton rb = (RadioButton)grd_bindmembers.Rows[i].Cells[0].FindControl("rdo_check");
                    // rb.GroupName = "ff" + i.ToString();
                    if (rb != null)
                    {
                        if (rb.Checked == true)
                        {
                            lblvalue = ((Label)grd_bindmembers.Rows[i].Cells[1].FindControl("lbl_membid")).Text;
                            reqfor = lblvalue;
                            memid = reqfor;
                        }
                    }
                }
                SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + lblvalue + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    reqto = dt1.Rows[0]["Enterby"].ToString();

                }
                SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + reqto + "'", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    fstname = dt2.Rows[0]["FirstName"].ToString();
                    lstname = dt2.Rows[0]["LastName"].ToString();

                }
                Admstrtname.InnerText = fstname + " " + lstname;
                btnmodal.Visible = true;
                btn_Savee.Visible = false;
            }

        }

        protected void btn_save_Click1(object sender, EventArgs e)
        {
            string id = "";
            string requestno = "";
            string reqfrom = "";
            string reqto = "";
            string reqfor = "";
            string reqmsg = "";
            string memid = "";
            string status = "";
            string enterby = "";
            string enterdt = "";
            string time = "";
            string lblvalue = "";
            string getrespons = "";
            string requesttype = "";
            string ParentsType = "";
            string Email = "";
            string youare = "";
            string spousetype = "";
            string spouseid = "";
            string spousename = "";
            requestno = GetRequestNo();
            try
            {
                reqfrom = Session["Memberid"].ToString();
                enterby = reqfrom;
            }
            catch
            {

            }
            reqto = "";
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            time = DateTime.Now.ToString("hh:mm:ss");
            status = "Accept";
            enterdt = date;
            reqmsg = txt_msg.Text.Trim();
            requesttype = "ChildType";
            if (ddl_youare.Text.Trim() != "")
            {
                youare = ddl_youare.Text.Trim();
            }
            if (ddl_Mothername.Text.Trim() != "")
            {
                spouseid = ddl_Mothername.Text.Trim();
            }
            if (txt_Mothername.Text.Trim() != "")
            {
                spousename = txt_Mothername.Text.Trim();
            }
            ParentsType = ddl_youare.Text.Trim();
            Email = "Na";
            da = new SqlDataAdapter("select * from MainMembers where Contact='" + txt_search.Text.Trim() + "' or MemberId='" + txt_search.Text.Trim() + "' or FirstName='" + txt_search.Text.Trim() + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < grd_bindmembers.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)grd_bindmembers.Rows[i].Cells[0].FindControl("rdo_check");
                // rb.GroupName = "ff" + i.ToString();
                if (rb != null)
                {
                    if (rb.Checked == true)
                    {
                        lblvalue = ((Label)grd_bindmembers.Rows[i].Cells[1].FindControl("lbl_membid")).Text;
                        reqfor = lblvalue;
                        memid = reqfor;
                    }
                }
            }
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + lblvalue + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                reqto = dt1.Rows[0]["Enterby"].ToString();

            }
            Admstrtname.InnerText = reqto;
            getrespons = insertrequest(ParentsType, Email, spousetype, youare, spouseid, spousename, requesttype, id, requestno, reqfrom, reqto, reqfor, reqmsg, memid, status, enterby, enterdt, time).ToString(); ;
            if (getrespons == "1")
            {
                btn_save.Text = "Request Sent";
                Response.Redirect("Add_Parents.aspx?status=Sent");

            }
            else
            {
                Response.Write("<script>alert('There is went something wrong.')</script>");
            }

        }
        public int insertrequest(string ParentsType, string Email, string spousetype, string Youare, string spouseid, string spousename, string requesttype, string id, string requestno, string reqfrom, string reqto, string reqfor, string reqmsg, string memid, string status, string enterby, string enterdt, string time)
        {
            int ret = 0;
            string blank = "No Data";
            if (ddl_youare.Text.Trim() == "Select")
            {
                Response.Write("<script>alert('Select Parent Type.')</script>");
            }
            else if(txt_Mothername.Text.Trim() == "")
            {
                Response.Write("<script>alert('Select Spouse.')</script>");
            }
            else
            {
                SqlConnection con = new SqlConnection(strcon);
                cmd = new SqlCommand("SPAllRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("ParentsType", ParentsType);
                cmd.Parameters.AddWithValue("Email", Email);
                cmd.Parameters.AddWithValue("SpouseType", spousetype);
                cmd.Parameters.AddWithValue("YouAre", Youare);
                cmd.Parameters.AddWithValue("YouSpouseid", spouseid);
                cmd.Parameters.AddWithValue("YouSpousename", spousename);
                cmd.Parameters.AddWithValue("ReqstType", requesttype);
                cmd.Parameters.AddWithValue("RequestNo", requestno);
                cmd.Parameters.AddWithValue("RequestFrom", reqfrom);
                cmd.Parameters.AddWithValue("RequestTo", reqto);
                cmd.Parameters.AddWithValue("RequestFor", reqfor);
                cmd.Parameters.AddWithValue("RequestMsg", reqmsg);
                cmd.Parameters.AddWithValue("Memberid", memid);
                cmd.Parameters.AddWithValue("Status", status);
                cmd.Parameters.AddWithValue("Regstatus", "Reject");
                cmd.Parameters.AddWithValue("Enterby", enterby);
                cmd.Parameters.AddWithValue("Enterdt", enterdt);
                cmd.Parameters.AddWithValue("Entertime", time);
                Session["Chilsnmemid"] = Memid.ToString();
                if (btn_save.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("StatementType", "Update");

                }
                else if (btn_save.Text == "Send")
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
                    ret = ret + 1;
                }
            }
            return ret;
        }
        public string GetRequestNo()
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
                string Query = "select * from Tbl_AllRequests where RequestNo LIKE 'Memberid%' ";
                DataTable dt = RetriveData(Query);
                if (dt.Rows.Count > 0)
                {
                    coun = dt.Rows.Count + 1;
                    id = "RequestNo" + coun;
                }
                else
                {
                    id = "RequestNo" + 1;
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

        protected void btn_serch_Click(object sender, EventArgs e)
        {
            getgrd();
        }
        public void getgrd()
        {
            SqlConnection con = new SqlConnection(strcon);
            da = new SqlDataAdapter("select * from MainMembers where Contact='" + txt_search.Text.Trim() + "' or MemberId='" + txt_search.Text.Trim() + "' or FirstName='" + txt_search.Text.Trim() + "'", con);
            ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < grd_bindmembers.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)grd_bindmembers.Rows[i].Cells[0].FindControl("rdo_check");
                // rb.GroupName = "ff" + i.ToString();
                if (rb != null)
                {
                    if (rb.Checked == true)
                    {

                    }
                }
            }
            //foreach (GridViewRow row in grd_bindmembers.Rows) 
            //{
            //    RadioButton rdoDelDt = (RadioButton)row.FindControl("rdo_check"); 
            //    if (rdoDelDt.Checked == true) 
            //    { 
            //        string date = grd_bindmembers.DataKeys[row.RowIndex].Value.ToString(); 
            //    } 
            //}
            grd_bindmembers.DataSource = ds.Tables[0];
            grd_bindmembers.DataBind();
        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            txt_Dod.Visible = true;
            chb_dodCirca.Visible = true;
            btn_Edit.Visible = false;
            divcreca.Visible = true;
            txt_deathloc.Visible = true;
            Deathlocdiv.Visible = true;
            curalive.Visible = false;
        }
    }
}