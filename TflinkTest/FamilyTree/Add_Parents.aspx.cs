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
    public partial class Add_Parents : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string Memid = "";
        string memberidbysub = "";
        string familyid = "";
        string Memidcreatedoarents = "";
        string newid = "";
        string status = "";
        string memidm = "";
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        Secureconnection connect = new Secureconnection();
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                familyid = Session["Familyid"].ToString();
                memidm = Session["Memberid"].ToString();
                // Bindmemnm.InnerText = memidm;
                //  P1.InnerText = memidm;

            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }

            if (!IsPostBack)
            {
                ddl_Country.DataSource = CountryList();
                ddl_Country.DataBind();
            }
            if (!IsPostBack)
            {

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
                    string memid = "";
                    memid = row["Contact"].ToString();
                    txtItems.Add(memid);
                }
            }

            return txtItems.ToArray();
        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            //LoadGridview();
            getgrd();
        }
        protected void LoadGridview()
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from MainMembers where Contact='" + txt_search.Text.Trim() + "' or FirstName='" + txt_search.Text.Trim() + "' or MemberId='" + txt_search.Text.Trim() + "'  order by id desc", con);
            con.Open();
            adapt.Fill(dt);
            grd_bindmembers.DataSource = dt;
            grd_bindmembers.DataBind();
            con.Close();
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

        protected void btn_chk_Click(object sender, EventArgs e)
        {
            getgrd();
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            //try
            //{

            if (ddl_Country.Text == "[Not Specified]")
            {

                // Response.Write("<script>alert('Please select country.')</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Select Country!');", true);

            }
            else
            {
                Submitdata();
                insertinspouseof();
                
                insertparentinspouseof();
                insertparentinspouseofGF();

                insertChild();

                updatespouseinchild();
            }

            //}
            //catch
            //{

            //}
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
                curadm = Session["Mainmemid"].ToString();
            }
            catch
            {

            }
            string creradod = "";
            string creradob = "";
            
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
                creradob = "Not Available";
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
                dod = DateTime.Now.ToString("yyyy/MM/dd");
                creradod = "Not Available";
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
            Session["Chilsnmemid"] = Memid.ToString();
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Updated Successfully</div> ";
                P1.InnerHtml = line.ToString();
            }
            else if (btn_save.Text == "Send")
            {
                cmd.Parameters.AddWithValue("StatementType", "Insert");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Saved Successfully</div> ";
                P1.InnerHtml = line.ToString();
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
                string spousefname = "";
                string spouselname = "";
                string spousememid = "";
                string parentid = "";
                
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
                SqlDataAdapter da6 = new SqlDataAdapter("select * from Tbl_ParentsOf where Parentsofid='" + Memberid + "'", con);
                DataTable dt6 = new DataTable();
                da6.Fill(dt6);
                if (dt6.Rows.Count > 0)
                {
                    parentid = dt6.Rows[0]["Memberid"].ToString();  
                }
                SqlDataAdapter da7 = new SqlDataAdapter("select * from Tbl_SpouseOf where Spouseofid='" + parentid + "'", con);
                DataTable dt7 = new DataTable();
                da7.Fill(dt7);
                if (dt7.Rows.Count > 0)
                {
                    spousememid = dt7.Rows[0]["Memberid"].ToString();
                    spousefname = dt7.Rows[0]["MemberFName"].ToString();
                    spouselname = dt7.Rows[0]["MemberLName"].ToString();
                    SqlCommand cmd = new SqlCommand("update Tbl_ChildrenOf set SpouseName='" + spousefname + " " + spouselname + "',SpouseMemid='" + spousememid + "' where Childofid='" + parentid + "' and Memberid='" + Memberid + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

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
                cmd.Parameters.AddWithValue("SpouseName", spousefname+" "+ spouselname);
                cmd.Parameters.AddWithValue("SpouseMemid", spousememid);
                cmd.Parameters.AddWithValue("MemberFName", fnamemember);
                cmd.Parameters.AddWithValue("MemberLName", Lnamemember);
                cmd.Parameters.AddWithValue("Dob", memberdob);
                cmd.Parameters.AddWithValue("Gender", Gender);
                cmd.Parameters.AddWithValue("Memberid", Memberid);
                cmd.Parameters.AddWithValue("ChildoffName", txt_Fname.Text.Trim());//Got values from Parents
                cmd.Parameters.AddWithValue("ChildofLName", txt_Lname.Text.Trim());//
                cmd.Parameters.AddWithValue("Childofid", Memidcreatedoarents);//
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
            if (ddl_Parentstype.Text.Trim() == "Father" || ddl_Parentstype.Text.Trim() == "Mother")
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
                int k = 0;
                if (ddl_Parentstype.Text.Trim() == "Father")
                {
                    if (Motherparentmemid != "")
                    {
                        k = cmd.ExecuteNonQuery();
                    }
                }
                else if (ddl_Parentstype.Text.Trim() == "Mother")
                {
                    if (Fatherparentmemid != "")
                    {
                        k = cmd.ExecuteNonQuery();
                    }
                }


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
                cmd1.Parameters.AddWithValue("Status", "DirectAdd");
                cmd1.Parameters.AddWithValue("EnterDate", date);
                cmd1.Parameters.AddWithValue("EnterTime", time);
                cmd1.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Send")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Insert");
                }
                //int k1 = cmd1.ExecuteNonQuery();
                int k1 = 0;
                if (ddl_Parentstype.Text.Trim() == "Father")
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
                cmd1.Parameters.AddWithValue("Status", "DirectAdd");
                cmd1.Parameters.AddWithValue("EnterDate", date);
                cmd1.Parameters.AddWithValue("EnterTime", time);
                cmd1.Parameters.AddWithValue("Enterby", "Admin");
                if (btn_save.Text == "Update")
                {
                    cmd1.Parameters.AddWithValue("StatementType", "Update");
                }
                else if (btn_save.Text == "Send")
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
            try
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
            catch
            {

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
            requesttype = "ParentsType";
            ParentsType = ddl_paenttypech.Text.Trim();
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
            if (ddl_paenttypech.Text.Trim() == "Select")
            {
                Response.Write("<script>alert('Select Parent Type.')</script>");
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

        protected void grd_bindmembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void btn_Save_Click2(object sender, EventArgs e)
        {
            string lblvalue = "";
            string reqfor = "";
            string memid = "";
            string reqto = "";
            string fstname = "";
            string lstname = "";
            SqlConnection con = new SqlConnection(strcon);
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //  register the script to close the popup
            //this.Page.ClientScript.RegisterStartupScript(typeof(NewPerson), "closeThickBox", "self.parent.closepopup();", true);
            this.Page.ClientScript.RegisterStartupScript(typeof(MyTree), "closeThickBox", "self.parent.closeScreen();", true);
            //this.Page.ClientScript.RegisterStartupScript(typeof(NewPerson), "closeThickBox", "self.parent.test();", true);
            Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");

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
        public void updatespouseinchild()
        {
            SqlConnection con = new SqlConnection(strcon);
            string memberiid = "";
            try
            {
                memberiid = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string SpouseMemid = "";
            string Spouseofid = "";
            string fname = "";
            string lname = "";

            string spfname = "";
            string splname = "";
            string Childofid = "";
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where Memberid='" + memberiid + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    SpouseMemid = dt1.Rows[i]["SpouseMemid"].ToString();
                    Childofid = dt1.Rows[i]["Childofid"].ToString();


                    if (SpouseMemid == "")
                    {
                        SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_SpouseOf where Memberid='" + Childofid + "'", con);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            { 
                                Spouseofid = dt2.Rows[j]["Spouseofid"].ToString();
                                fname = dt2.Rows[j]["MemberFName"].ToString();
                                lname = dt2.Rows[j]["MemberLName"].ToString(); 
                                spfname = dt2.Rows[j]["SpouseoffName"].ToString();
                                splname = dt2.Rows[j]["SpouseofLName"].ToString();

                                string fullname = "";
                                fullname = fname + " " + lname;
                                if (Spouseofid != "")
                                {
                                    SqlCommand cmd = new SqlCommand("update Tbl_ChildrenOf set SpouseMemid='" + Spouseofid + "',SpouseName='" + fullname + "' where Childofid='" + Childofid + "'", con);

                                    SqlCommand cmd1 = new SqlCommand("update Tbl_ChildrenOf set SpouseMemid='" + Spouseofid + "',SpouseName='" + fullname + "' where Childofid='" + Spouseofid + "'", con);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    //cmd1.ExecuteNonQuery();
                                    con.Close();
                                }

                            }
                        }
                    }
                }

            }
        }

    }
}