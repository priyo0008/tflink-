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
    public partial class MyTree1 : System.Web.UI.Page
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


        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            memid = Request.QueryString["Memid"];
            try
            {
                userid = Session["UserId"].ToString();
                Fnamess = "";
                familyid = Session["Familyid"].ToString();
            }
            catch
            {
                Response.Redirect("~/Home.aspx");
            }

            if (!IsPostBack)
            {
                ddl_Country.DataSource = CountryList();
                ddl_Country.DataBind();
                if (Fnamess != "")
                {
                    scriptid.InnerHtml = "<script>setTimeout(function () {$('#myModal').modal();}, 1000);</script>";
                }
                else
                {
                    scriptid.InnerHtml = "";
                }
            }
            //try
            //{
            if (checkmemberid() == "Available")
            {
                BindTree(userid);
            }
            else
            {
                scriptid.InnerHtml = "<script>setTimeout(function () {$('#myModal').modal();}, 1000);</script>";
            }
            //}
            //catch
            //{ 

            //}




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
        protected void btn_save_Click(object sender, EventArgs e)
        {
            Submitdata();
            scriptid.InnerHtml = "";
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
        public void Submitdata()
        {
            SqlConnection con = new SqlConnection(strcon);
            string Fid = Getfamilyid();
            Session["Familyid"] = Fid.ToString();
            string Memid = GetMemberid();
            string Userid = "";
            string Password = "";
            string dob = "";
            string dod = "";
            string creradod = "";
            string creradob = "";
            if (txt_Dob.Text != "")
            {
                dob = txt_Dob.Text.Trim();
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("yyyy/MM/dd");
                ////DateTime Fdate = DateTime.ParseExact(txt_Dob.Text.ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture); 
                ////Fdate = Convert.ToDateTime(Fdate.Date.ToString("yyyy/MM/dd"));
                ////dob = Fdate.Date.ToString("yyyy/MM/dd");
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
                creradod = "Available";
            }
            else
            {
                dod = DateTime.Now.ToString("yyyy/MM/dd");
            }

            try
            {
                if (Session["UserId"] != null && Session["Password"] != null)
                {
                    Userid = Session["UserId"].ToString();
                    Password = Session["Password"].ToString();
                    string encryptedString = SomeStaticClass.encrypt(Password.ToString());
                    //Password = encryptedString;
                }
            }
            catch
            {

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
            cmd = new SqlCommand("SPMainMembers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", Userid.ToString());
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
            cmd.Parameters.AddWithValue("Dob", txt_Dob.Text.Trim());
            cmd.Parameters.AddWithValue("Creradob", creradob);
            cmd.Parameters.AddWithValue("Country", ddl_Country.Text.Trim());
            cmd.Parameters.AddWithValue("Birthloc", txt_Birthloc.Text.Trim());
            cmd.Parameters.AddWithValue("Dod", txt_Dod.Text.Trim());
            cmd.Parameters.AddWithValue("Creradod", creradod);
            cmd.Parameters.AddWithValue("Deathloc", txt_deathloc.Text.Trim());
            cmd.Parameters.AddWithValue("Notes", txt_notes.Text.Trim());
            cmd.Parameters.AddWithValue("Userid", Userid.ToString());
            cmd.Parameters.AddWithValue("Password", Password.ToString());
            cmd.Parameters.AddWithValue("Enterby", Memid);
            cmd.Parameters.AddWithValue("Wrngpswatmpt", "0");
            cmd.Parameters.AddWithValue("WrnpswDate", date);
            cmd.Parameters.AddWithValue("WrnpswTime", time);
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Entertime", time);
            if (btn_save.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            else if (btn_save.Text == "Save")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                BindTree(userid);
                // Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                Response.Redirect("../FamilyTree/MyTree1.aspx");
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
                    id = "Memberid" + coun;
                }
                else
                {
                    id = "Memberid" + 1;
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
        public void BindTree(string userid)
        {
            string MemberId = "";
            string familyidd = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where Userid='" + userid + "' order by id desc", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MemberId = dt.Rows[0]["MemberId"].ToString();
                Session["Memberid"] = MemberId.ToString();
                Session["Mainmemid"] = dt.Rows[0]["MemberId"].ToString();

            }
            try
            {
                if (memid.ToString() != "")
                {
                    Session["Memberid"] = memid;
                    MemberId = Session["Memberid"].ToString();
                }
            }
            catch
            {

            }
            string Fname = "";
            string Lname = "";
            string Location = "";
            string Email = "";
            string dob = "";
            string Gender = "";
            string cicra = "";
            string cicradod = "";
            string realage = "";
            string dod = "";
            int cn = 0;
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + MemberId + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Fname = dt1.Rows[0]["FirstName"].ToString();
                Lname = dt1.Rows[0]["LastName"].ToString();
                dob = dt1.Rows[0]["Dob"].ToString();
                dod = dt1.Rows[0]["Dod"].ToString();
                Gender = dt1.Rows[0]["Gender"].ToString();
                Email = dt1.Rows[0]["EmailId"].ToString();
                cicra = dt1.Rows[0]["Creradob"].ToString();
                cicradod = dt1.Rows[0]["Creradod"].ToString();
                Location = dt1.Rows[0]["City"].ToString();
                familyidd = dt1.Rows[0]["FamilyId"].ToString();
                if (familyidd != familyid)
                {
                    btn_Addchld.Visible = false;
                    btn_Addparents.Visible = false;
                    btnShow.Visible = false;
                }
                else
                {
                    btn_Addchld.Visible = true;
                    btn_Addparents.Visible = true;
                    btnShow.Visible = true;
                }
                MailName.InnerHtml = "<p><span style='color:red;'> " + deathmark(MemberId) + "</span> " + " " + Fname + "</p><p> " + Lname+"</p>";
                PLocation.InnerText = Location;
                string yr = DateTime.Now.ToString("yyyy");
                string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                if (cicra == "yes")
                {
                    int valuefst = age - 5;
                    string fstvlu = valuefst.ToString();
                    int valuescnd = age + 5;
                    string scndvlu = valuescnd.ToString();
                    realage = fstvlu + " - " + scndvlu;
                }
                else
                {
                    realage = age.ToString();
                }
                if (cicradod == "Available" || cicradod == "yes")
                {
                    yr = Convert.ToDateTime(dod).ToString("yyyy");
                    yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                    age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                    realage = age.ToString();

                }
                else
                {
                    realage = age.ToString();
                }
                if (cicradod == "yes")
                {
                    int valuefst = age - 5;
                    string fstvlu = valuefst.ToString();
                    int valuescnd = age + 5;
                    string scndvlu = valuescnd.ToString();
                    realage += fstvlu + " - " + scndvlu;
                }
                if (Gender == "Male")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                Years.InnerText = Gender + " (" + getage(MemberId) + " " + " )";
                try
                {
                    Treefor.InnerText = Fname + " " + Lname;
                }
                catch
                {

                }
            }
            Getspouse();
            getchildren();
            Getsparents();
            getSiblings();
        }

        public void Getspouse()
        {
            string bindspouse = "";
            string MemberId = "";
            try
            {
                MemberId = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string MemberFname = "";
            string SpouseFName = "";
            string MemberLname = "";
            string SpouseLName = "";
            string dob = "";
            string spomemberid = "";
            string Gender = "";
            string starttag = "";
            string endtag = "";
            string endtagend = "";
            string cicra = "";
            string realage = "";
            string familyids = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_SpouseOf where Spouseofid='" + MemberId + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows.Count > 1)
                    {
                        starttag = "<ul><li>";
                        endtag = "</li></ul>";
                    }
                    else
                    {
                        starttag = "";
                        endtag = "";
                    }

                    MemberFname = dt1.Rows[i]["MemberFName"].ToString();
                    SpouseFName = dt1.Rows[i]["SpouseoffName"].ToString();
                    MemberLname = dt1.Rows[i]["MemberLName"].ToString();
                    SpouseLName = dt1.Rows[i]["SpouseofLName"].ToString();
                    spomemberid = dt1.Rows[i]["Memberid"].ToString();
                    familyids = dt1.Rows[i]["Familyid"].ToString();
                    Gender = dt1.Rows[i]["Gender"].ToString();
                    dob = dt1.Rows[i]["Dob"].ToString();
                    string yr = DateTime.Now.ToString("yyyy");
                    string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                    int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                    if (familyids != familyid)
                    {
                        // btnShow.Visible = false;
                    }
                    if (Gender == "Male")
                    {
                        Gender = "M";
                    }
                    else
                    {
                        Gender = "F";
                    }
                    bindspouse += "";
                    bindspouse += " <div class='box-main-tree'><span class='box-tree-left'><a href='MyTree1.aspx?Memid=" + spomemberid + "'><p><span class='box-ttree-left' style='color:red;'> " + deathmark(spomemberid) + "</span>  " + MemberFname + " </p><p>" + MemberLname + "</p></a>";
                    bindspouse += "<p>"+getcity(spomemberid) +"</p> <p>" + Gender + " - (" + getage(spomemberid) + ")</p>  </span>";
                    bindspouse += " <span class='box-tree-right'><a href='MyTree1.aspx?Memid=" + spomemberid + "'> <img src='images/tree2.jpg' width='30' height='30' /></a> ";
                    bindspouse += "<a href='View.aspx?target=photo&Memidview=" + spomemberid + "'> <img src='images/greenimg2.jpg' width='30' height='30' /></a>";
                    bindspouse += "<a href='View.aspx?Memidview=" + spomemberid + "' onclick='window.open('Edit_Parents.aspx','popup_window','height=500px,width=500px,scrollbars=1');'><img src='images/image4.png' width='30' height='30' /></a> </span></div>";
                    if (i != dt1.Rows.Count - 1)
                    {
                        bindspouse += starttag;
                        endtagend += endtag;
                    }
                }
                bindspouse += endtagend;
                bindspouse += "</li>";
                if (spomemberid != "")
                {
                    Bind_Spouse.InnerHtml = bindspouse.ToString();
                }

            }
            else
            {
                Bind_Spouse.Visible = false;
            }
        }

        public string checkmemberid()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where Userid='" + userid + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            string Status = "";
            if (dt1.Rows.Count > 0)
            {
                Status = dt1.Rows[0]["MemberId"].ToString();
                txt_Email.Text = dt1.Rows[0]["EmailId"].ToString();
            }
            if (Status != "")
            {
                Status = "Available";
            }
            else
            {
                Status = "Not Available";
            }
            return Status;
        }
        public void getchildren()
        {
            string bindchild = "";
            string MemberId = "";
            try
            {
                MemberId = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string MemberFname = "";
            string SpouseFName = "";
            string MemberLname = "";
            string SpouseLName = "";
            string dob = "";
            string spomemberid = "";
            string Gender = "";
            string starttag = "";
            string endtag = "";
            string endtagend = "";
            string familyidc = "";
            string childtype = "";
            string city = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where Childofid='" + MemberId + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows.Count > 1)
                    {
                        starttag = "<ul><li>";
                        endtag = "</li></ul>";
                    }
                    else
                    {
                        starttag = "";
                        endtag = "";
                    }

                    MemberFname = dt1.Rows[i]["MemberFName"].ToString();
                    SpouseFName = dt1.Rows[i]["ChildoffName"].ToString();
                    MemberLname = dt1.Rows[i]["MemberLName"].ToString();
                    SpouseLName = dt1.Rows[i]["ChildofLName"].ToString();
                    spomemberid = dt1.Rows[i]["Memberid"].ToString();
                    familyidc = dt1.Rows[i]["Familyid"].ToString();
                  
                    Gender = dt1.Rows[i]["Gender"].ToString();

                    dob = dt1.Rows[i]["Dob"].ToString();
                    string yr = DateTime.Now.ToString("yyyy");
                    string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                    int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                    if (familyidc != familyid)
                    {
                        //btn_Addchld.Visible = false;
                    }
                    if (Gender == "Male")
                    {
                        Gender = "M";
                        childtype = "Son";
                    }
                    else
                    {
                        Gender = "F";
                        childtype = "Daughter";
                    }

                    bindchild += "";
                    bindchild += " <div class='box-main-tree'><span class='box-tree-left'><a href='MyTree1.aspx?Memid=" + spomemberid + "'><p><span class='box-ttree-left' style='color:red;'> " + deathmark(spomemberid) + "</span> " + MemberFname + " </p><p>" + MemberLname + "</p></a>";
                    bindchild += "<p>"+ getcity(spomemberid) + "</p><p>"+ childtype + "</p> <p>" + Gender + " - (" + getage(spomemberid) + ") </p>  </span>";
                    bindchild += " <span class='box-tree-right'><a href='MyTree1.aspx?Memid=" + spomemberid + "'> <img src='images/tree2.jpg' width='30' height='30' /></a> ";
                    bindchild += "<a href='View.aspx?target=photo&Memidview=" + spomemberid + "'> <img src='images/greenimg2.jpg' width='30' height='30' /></a>";
                    bindchild += "<a href='View.aspx?Memidview=" + spomemberid + "' onclick='window.open('Edit_Parents.aspx','popup_window','height=500px,width=500px,scrollbars=1');'><img src='images/image4.png' width='30' height='30' /></a>  </span></div>";
                    if (i != dt1.Rows.Count - 1)
                    {
                        bindchild += starttag;
                        endtagend += endtag;
                    }
                }
                bindchild += endtagend;
                bindchild += "</li>";
                Bind_Child.InnerHtml = bindchild.ToString();
            }
            else
            {
                Bind_Child.Visible = false;
            }
        }
        public void Getsparents()
        {
            string bindspouse = "";
            string MemberId = "";
            try
            {
                MemberId = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string MemberFname = "";
            string SpouseFName = "";
            string MemberLname = "";
            string SpouseLName = "";
            string bindparents = "";
            string dob = "";
            string spomemberid = "";
            string Gender = "";
            string starttag = "";
            string endtag = "";
            string maleactive = "";
            string femaleactive = "";
            string guardiantyep = "";
            string factive = "";
            string sfactive = "";
            string smactive = "";
            string mactive = "";
            string familyidp = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ParentsOf where Parentsofid='" + MemberId + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows.Count > 1)
                    {

                    }
                    else
                    {

                    }
                    MemberFname = dt1.Rows[i]["MemberFName"].ToString();
                    SpouseFName = dt1.Rows[i]["ParentsoffName"].ToString();
                    MemberLname = dt1.Rows[i]["MemberLName"].ToString();
                    SpouseLName = dt1.Rows[i]["ParentsofLName"].ToString();
                    spomemberid = dt1.Rows[i]["Memberid"].ToString();
                    familyidp = dt1.Rows[i]["Familyid"].ToString();
                    guardiantyep = dt1.Rows[i]["Parentstype"].ToString();
                    Gender = dt1.Rows[i]["Gender"].ToString();
                    dob = dt1.Rows[i]["Dob"].ToString();
                    string yr = DateTime.Now.ToString("yyyy");
                    string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                    int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                    if (familyidp != familyid)
                    {
                        // btn_Addparents.Visible = false;
                    }
                    if (Gender == "Male")
                    {
                        Gender = "M";
                    }
                    else
                    {
                        Gender = "F";
                    }
                    bindparents = "<h3>" + guardiantyep + "</h3><div class='box-main-tree'><span class='box-tree-left'><a href='MyTree1.aspx?Memid=" + spomemberid + "'><p><span class='box-ttree-left' style='color:red;'> " + deathmark(spomemberid) + "</span> " + MemberFname + " </p><p>" + MemberLname + "</p></a><p>" + getcity(spomemberid) + "</p><p>" + Gender + " - (" + getage(spomemberid) + ") </p></span>";
                    bindparents += " <span class='box-tree-right'><a href='MyTree1.aspx?Memid=" + spomemberid + "'><img src='images/tree2.jpg' width='30' height='30' /></a>";
                    bindparents += "<a href='View.aspx?target=photo&Memidview=" + spomemberid + "'> <img src='images/greenimg2.jpg' width='30' height='30' /></a> ";
                    bindparents += " <a href='View.aspx?Memidview=" + spomemberid + "' onclick='window.open('Edit_Parents.aspx','popup_window','height=500px,width=500px,scrollbars=1');'><img src='images/image4.png' width='30' height='30' /></a> </span></div>";

                    if (Gender == "M" || Gender == "Male")
                    {
                        if (factive != "Active")
                        {
                            if (sfactive != "Active")
                            {
                                if (guardiantyep == "Stepfather")
                                {
                                    bindfather.InnerHtml = bindparents.ToString();
                                    maleactive = "Active";
                                    sfactive = "Active";
                                }
                                else if (guardiantyep == "Guardian")
                                {
                                    bindfather.InnerHtml = bindparents.ToString();
                                    maleactive = "Active";
                                }
                            }
                        }
                        if (guardiantyep == "Father")
                        {
                            bindfather.InnerHtml = bindparents.ToString();
                            maleactive = "Active";
                            factive = "Active";
                        }
                    }
                    else if (Gender == "F" || Gender == "Female")
                    {
                        if (mactive != "Active")
                        {
                            if (smactive != "Active")
                            {
                                if (guardiantyep == "Stepmother")
                                {
                                    bindmother.InnerHtml = bindparents.ToString();
                                    femaleactive = "Active";
                                    smactive = "Active";
                                }
                                else if (guardiantyep == "Guardian")
                                {
                                    bindmother.InnerHtml = bindparents.ToString();
                                    femaleactive = "Active";
                                }
                            }
                        }
                        if (guardiantyep == "Mother")
                        {
                            bindmother.InnerHtml = bindparents.ToString();
                            femaleactive = "Active";
                            mactive = "Active";
                        }
                    }
                }
                if (maleactive == "")
                {
                    bindfather.Visible = false;
                }
                if (femaleactive == "")
                {
                    bindmother.Visible = false;
                }
                //Bind_Spouse.InnerHtml = bindspouse.ToString();
            }
            else
            {
                bindfather.Visible = false;
                bindmother.Visible = false;
            }
        }

        public void insertinsGetspouse()
        {
            string Memberid = "";
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            try
            {
                Memberid = Session["Memberid"].ToString();
            }
            catch
            {

            }
            SqlConnection con = new SqlConnection(strcon);
            string spouseid = "";
            string spousetwoid = "";
            string MemberId = "";
            string Chldmemid = "";
            string Gender = "";
            //SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where SpouseMemid='" + ddl_Mothername.Text.Trim() + "' or Childofid='"+ ddl_Mothername.Text.Trim() + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where Memberid='" + Memberid + "'", con);

            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                spouseid = dt1.Rows[0]["SpouseMemid"].ToString();
                spousetwoid = dt1.Rows[0]["Childofid"].ToString();
            }

            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_ChildrenOf where SpouseMemid='" + spouseid + "' or Childofid='" + spousetwoid + "'", con);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    spouseid = dt2.Rows[i]["SpouseMemid"].ToString();
                    spousetwoid = dt2.Rows[i]["Childofid"].ToString();
                }
            }
        }
        public void getSiblings()
        {
            string bindchild = "";
            string MemberId = "";
            try
            {
                MemberId = Session["Memberid"].ToString();
            }
            catch
            {

            }
            string MemberFname = "";
            string SpouseFName = "";
            string MemberLname = "";
            string SpouseLName = "";
            string dob = "";
            string spomemberid = "";
            string Gender = "";
            string starttag = "";
            string endtag = "";
            SqlConnection con = new SqlConnection(strcon);
            string spouseid = "";
            string spousetwoid = "";
            string Chldmemid = "";
            string endtagend = "";
            //SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where SpouseMemid='" + ddl_Mothername.Text.Trim() + "' or Childofid='"+ ddl_Mothername.Text.Trim() + "'", con);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_ChildrenOf where Memberid='" + MemberId + "'", con);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                spouseid = dt2.Rows[0]["SpouseMemid"].ToString();
                spousetwoid = dt2.Rows[0]["Childofid"].ToString();
            }

            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_ChildrenOf where SpouseMemid='" + spouseid + "' and Childofid='" + spousetwoid + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            string ff = "select * from Tbl_ChildrenOf where SpouseMemid='" + spouseid + "' and Childofid='" + spousetwoid + "'";
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    spomemberid = dt1.Rows[i]["Memberid"].ToString();
                    if (MemberId != spomemberid)
                    {
                        if (dt1.Rows.Count > 1)
                        {
                            starttag = "<ul><li>";
                            endtag = "</li></ul>";
                        }
                        else
                        {
                            starttag = "";
                            endtag = "";
                        }

                        MemberFname = dt1.Rows[i]["MemberFName"].ToString();
                        SpouseFName = dt1.Rows[i]["ChildoffName"].ToString();
                        MemberLname = dt1.Rows[i]["MemberLName"].ToString();
                        SpouseLName = dt1.Rows[i]["ChildofLName"].ToString();
                        Gender = dt1.Rows[i]["Gender"].ToString();
                        dob = dt1.Rows[i]["Dob"].ToString();
                        string yr = DateTime.Now.ToString("yyyy");
                        string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                        int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                        if (Gender == "Male")
                        {
                            Gender = "M";
                        }
                        else
                        {
                            Gender = "F";
                        }
                        bindchild += "";
                        bindchild += " <div class='box-main-tree'><span class='box-tree-left'><a href='MyTree1.aspx?Memid=" + spomemberid + "'><p><span class='box-trete-left' style='color:red;'> " + deathmark(spomemberid) + "</span> " + MemberFname + " </p><p>" + MemberLname + "</p></a>";
                        bindchild += "<p>"+ getcity(spomemberid) + "</p> <p>" + Gender + " - (" + getage(spomemberid) + ") </p>  </span>";
                        bindchild += " <span class='box-tree-right'><a href='MyTree1.aspx?Memid=" + spomemberid + "'> <img src='images/tree2.jpg' width='30' height='30' /></a> ";
                        bindchild += "<a href='View.aspx?target=photo&Memidview=" + spomemberid + "'> <img src='images/greenimg2.jpg' width='30' height='30' /></a>";
                        bindchild += "<a href='View.aspx?Memidview=" + spomemberid + "' onclick='window.open('Edit_Parents.aspx','popup_window','height=500px,width=500px,scrollbars=1');'><img src='images/image4.png' width='30' height='30' /></a> </span></div>";
                        if (i + 1 != dt1.Rows.Count)
                        {
                            bindchild += starttag;
                            endtagend += endtag;
                        }
                        siblinglitag.Visible = true;
                        Bind_Siblings.Visible = true;
                    }

                }
                bindchild += endtagend;
                bindchild += "</li>";
                Bind_Siblings.InnerHtml = bindchild.ToString();
                //siblinglitag.Visible = true;
                //Bind_Siblings.Visible = true;
            }
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
        public string getage(string memid)
        {
            string dob = "";
            string cicradob = "";
            string dod = "";
            string cicradod = "";
            string realage = "";
            string yod = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + memid + "'", con);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    dob = dt2.Rows[i]["Dob"].ToString();
                    cicradob = dt2.Rows[i]["Creradob"].ToString();
                    dod = dt2.Rows[i]["Dod"].ToString();
                    cicradod = dt2.Rows[i]["Creradod"].ToString();
                }
            }
            string yr = DateTime.Now.ToString("yyyy");
            string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
            int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
            if (cicradob == "yes")
            {
                int valuefst = age - 5;
                string fstvlu = valuefst.ToString();
                int valuescnd = age + 5;
                string scndvlu = valuescnd.ToString();
                realage = fstvlu + " - " + scndvlu;
            }
            else
            {
                realage = age.ToString();
            }
            if (cicradod == "Available" || cicradod == "yes")
            { 
                yr = Convert.ToDateTime(dod).ToString("yyyy");
                yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                realage = age.ToString();

            }
            else
            {
                realage = age.ToString()+ "" + " " + "  Years";
            }
            if (cicradod == "yes")
            {
                int valuefst = age - 5;
                string fstvlu = valuefst.ToString();
                int valuescnd = age + 5;
                string scndvlu = valuescnd.ToString();
                realage = fstvlu + " - " + scndvlu + ""+ " "+"  Years";
            }
            //else
            //{
            //    yr = Convert.ToDateTime(dob).ToString("yyyy");
            //    yod = Convert.ToDateTime(dod).ToString("yyyy");
            //    realage = yr + " - " + yod;
            //}
            return realage;
        }
        public string deathmark(string memidd)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + memidd + "'", con);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            string status = "";
            string mark = "";
            if (dt2.Rows.Count > 0)
            {
                status = dt2.Rows[0]["Creradod"].ToString();
                if (status != "Not Available")
                {
                     //mark = "<span class='glyphicon glyphicon-ok'></span>";
                    mark = "Late  ";
                }
                else
                {
                    mark = "";
                }
            }
            return mark;
        }
        public string getcity(string chememcity)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da2 = new SqlDataAdapter("select * from MainMembers where MemberId='" + chememcity + "'", con);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            string city = "";
            string mark = "";
            if (dt2.Rows.Count > 0)
            {
                city = dt2.Rows[0]["City"].ToString(); 
            }
            else
            {
                city = "";
            }
            return city;
        }
    }
}