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
using System.Drawing;

namespace TflinkTest.FamilyTree
{
    public partial class Edit_Parents : System.Web.UI.Page
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
        string ID = "";
        string target = "";


        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        string MemberId = "";
        string Memidmain = "";
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            target = Request.QueryString["target"];
            try
            {
                familyid = Session["Familyid"].ToString();
            }
            catch
            {

            }
            if (target == "photo")
            {
                profile.Attributes.Add("class", "active");
                Profilea.Attributes.Add("class", "tab-pane active");
                photo.Attributes.Add("class", "");
                photoa.Attributes.Add("class", "tab-pane");

            }
            else
            {
                photo.Attributes.Add("class", "active");
                photoa.Attributes.Add("class", "tab-pane active");
                profile.Attributes.Add("class", "");
                Profilea.Attributes.Add("class", "tab-pane");
            }
            try
            {
                ID = Request.QueryString["id"].ToString();
               
                if (ID != "")
                {
                    // showaddnames.Visible = true;
                    getpic(ID);
                }
            }
            catch
            {

            }
            finally
            {
                try
                {
                    Memidmain = Session["Mainmemid"].ToString();
                }
                catch
                {

                }
                
            }
            try
            {
                userid = Session["UserId"].ToString();
               // lbl_Username.Text = userid.ToString();
                MemberId = Session["Memberid"].ToString();


                if (!IsPostBack)
                {
                    //DataBinding photos 
                    LoadGridview();
                     //Names Binding
                    bindgrid();
                    if (checkcurssion(MemberId, userid) == "Available")
                    {
                        editbtn.Visible = true;
                        Div4.Visible = true;
                        Div3.Visible = true; 

                    }
                    getmemdetails(MemberId);
                    getdatainupdateform(MemberId);
                    ddl_Country.DataSource = CountryList();
                    ddl_Country.DataBind();
                }

        }
            catch
            {

            }
            finally
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
        public void getmemdetails(string memid)
        {
            string gender = "";
            string fname = "";
            string lastname = "";
            string dob = "";
            string birthloc = "";
            string dod = "";
            string deathloc = "";
            string city = "";
            string state = "";
            string country = "";
            string cicradeath = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where Memberid='" + memid + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MemberId = dt.Rows[0]["MemberId"].ToString();
                gender = dt.Rows[0]["Gender"].ToString();
                fname = dt.Rows[0]["FirstName"].ToString();
                lastname = dt.Rows[0]["LastName"].ToString();
                dob = dt.Rows[0]["Dob"].ToString();
                birthloc = dt.Rows[0]["Birthloc"].ToString();
                dod = dt.Rows[0]["Dod"].ToString();
                deathloc = dt.Rows[0]["Deathloc"].ToString();
                city = dt.Rows[0]["City"].ToString();
                state = dt.Rows[0]["State"].ToString();
                country = dt.Rows[0]["Country"].ToString();
                cicradeath = dt.Rows[0]["Creradod"].ToString();
                //string s1 = Backgroundcolor.SelectedItem.Text.ToString();

                //LabelText.BackColor = System.Drawing.Color.FromName(s1);
                DateTime date1 = Convert.ToDateTime(dob.ToString());
                dob = date1.ToString("dd/MM/yyyy");
                PGender.InnerHtml = "Gender:" + gender;
                Pfname.InnerHtml = "First Name:" + fname;
                Plastname.InnerHtml = "Last Name:" + lastname;
                Pdob.InnerHtml = "Date of birth:" + dob;
                Pbirthloc.InnerHtml = "Birth Location:" + birthloc;
                DateTime date2 = Convert.ToDateTime(dod.ToString());
                dod = date2.ToString("dd/MM/yyyy");
                if (cicradeath == "yes" || cicradeath == "Available")
                {
                    Pdod.InnerHtml = "Date of Death :" + dod;
                    Pdeathloc.InnerHtml = "Death Location:" + deathloc;
                    txt_Dod.Visible = true;
                    chb_dodCirca.Visible = true;
                    btn_Edit.Visible = false;
                    divcreca.Visible = true;
                    txt_deathloc.Visible = true;
                    Deathlocdiv.Visible = true;
                    curalive.Visible = false;
                }
                else
                {
                    Pdod.InnerHtml = "currently alive";
                }
                
                PCity.InnerHtml = "City:" + city;
                PRegion.InnerHtml = "State:" + state;
                Pcountry.InnerHtml = "Country:" + country;

            }
        }
        public void getdatainupdateform(string memidget)
        {

            string gender = "";
            string fname = "";
            string lastname = "";
            string dob = "";
            string birthloc = "";
            string city = "";
            string state = "";
            string country = "";
            string Email = "";
            string Address = "";
            string dod = "";
            string chbdodcr = "";
            string notes = "";
            string contact = "";
            string deathloc = "";
            string chbdobcr = "";
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where Memberid='" + memidget + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MemberId = dt.Rows[0]["MemberId"].ToString();
                gender = dt.Rows[0]["Gender"].ToString();
                fname = dt.Rows[0]["FirstName"].ToString();
                lastname = dt.Rows[0]["LastName"].ToString();
                dob = dt.Rows[0]["Dob"].ToString();
                birthloc = dt.Rows[0]["Birthloc"].ToString();
                city = dt.Rows[0]["City"].ToString();
                state = dt.Rows[0]["State"].ToString();
                country = dt.Rows[0]["Country"].ToString();
                Address = dt.Rows[0]["Address"].ToString();
                Email = dt.Rows[0]["EmailId"].ToString();
                dod = dt.Rows[0]["Dod"].ToString();
                chbdodcr = dt.Rows[0]["Creradod"].ToString();
                chbdobcr = dt.Rows[0]["Creradob"].ToString();
                deathloc = dt.Rows[0]["Deathloc"].ToString();
                notes = dt.Rows[0]["Notes"].ToString();
                contact = dt.Rows[0]["Contact"].ToString();
                hdn_pawrd.Value = dt.Rows[0]["Password"].ToString();
                hdn_fmlyid.Value = dt.Rows[0]["FamilyId"].ToString();
                hdn_id.Value = dt.Rows[0]["id"].ToString();
                hdn_Userid.Value = dt.Rows[0]["Userid"].ToString();

                PProfileof.InnerHtml = fname + " " + lastname;
                hdn_FullName.Value = fname + " " + lastname;
                fullname2.InnerHtml = fname + " " + lastname;

                if(chbdobcr == "Not Available")
                {
                    dob = "";
                }
                else
                {
                    DateTime date1 = Convert.ToDateTime(dob.ToString());
                    dob = date1.ToString("yyyy/MM/dd");
                }
                if (chbdodcr == "Not Available")
                {
                    dod = "";
                }
                else
                {
                    DateTime date2 = Convert.ToDateTime(dod.ToString());
                    dod = date2.ToString("yyyy/MM/dd");
                }
                ddl_gender.Text = gender;
                txt_Phonenmb.Text = contact;
                txt_Fname.Text = fname;
                txt_Lname.Text = lastname;
                txt_Email.Text = Email;
                txt_address.Text = Address;
                txt_City.Text = city;
                txt_State.Text = state;
                txt_Dob.Text = dob;
           
                ddl_Country.Text = country;
                txt_Birthloc.Text = birthloc;
                txt_Dod.Text = dod;
                txt_deathloc.Text = deathloc;
                chb_dodCirca.Checked = true;
                txt_notes.Text = notes;

                if (chbdobcr == "Not Available")
                {
                    chb_dobCirca.Checked = false;
                }
                else
                {
                    chb_dobCirca.Checked = true;
                }
                if (chbdodcr == "Not Available")
                {
                    chb_dodCirca.Checked = false;
                }
                else
                {
                    chb_dodCirca.Checked = true;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
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
                    bindgrid();
                    //insertinspouseof();
                    //insertChild();
                    //insertparentinspouseof();
                    //insertparentinspouseofGF();
                }

            //}
            //catch
            //{

            //}
        }
        protected void LoadGridview()
        {
            string fmlyid = familyid;
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_Pictures where MemberId='" + MemberId + "' order by id desc", con);
            con.Open();
            adapt.Fill(dt);
            grd_Picture.DataSource = dt;
            grd_Picture.DataBind();
            con.Close();
        }
        public void Submitdata()
        {
            SqlConnection con = new SqlConnection(strcon);
            string Fid = familyid;
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
                creradob = "Available";
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
            cmd = new SqlCommand("SPMainMembers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", hdn_Userid.Value);
            cmd.Parameters.AddWithValue("FamilyId", hdn_fmlyid.Value);
            cmd.Parameters.AddWithValue("MemberId", MemberId);
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
            cmd.Parameters.AddWithValue("Userid", hdn_Userid.Value);
            cmd.Parameters.AddWithValue("Password", hdn_pawrd.Value);
            cmd.Parameters.AddWithValue("Enterby", Memidmain);
            cmd.Parameters.AddWithValue("Wrngpswatmpt", "0");
            cmd.Parameters.AddWithValue("WrnpswDate", date); 
            cmd.Parameters.AddWithValue("WrnpswTime", time);
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Entertime", time);
            Session["Chilsnmemid"] = MemberId.ToString();
            if (btn_Update.Text == "Update")
            {
                cmd.Parameters.AddWithValue("StatementType", "Update");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Updated Successfully</div> ";
                P1.InnerHtml = line.ToString();
                //Request.QueryString["target"] = "";
              
            }
            else if (btn_Update.Text == "Send")
            {
                //cmd.Parameters.AddWithValue("StatementType", "Insert");
                //string line = "<div class='alert alert-success'><strong> Success!</strong> Data Saved Successfully</div> ";
                //P1.InnerHtml = line.ToString();
            }
            
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {

                //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                Response.Redirect("../FamilyTree/Edit_Parents.aspx");
            }
            else
            {
                string line = "<div class='alert alert-danger'><strong> Success!</strong> Submit Failed</div> ";
                //Bindnotice.InnerHtml = line.ToString();
            }

            con.Close();
        }
        public string checkcurssion(string curssionmemid, string user_id)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers  where Enterby='" + curssionmemid + "'  or MemberId='" + curssionmemid + "'", con);//curssionmemid
            string ggg = "select * from MainMembers where Enterby='" + curssionmemid + "'  or MemberId='" + curssionmemid + "'";
            DataTable dt1 = new DataTable();
            string curmemid = "";
            string result = "";
            string enterby = "";
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                curmemid = dt1.Rows[0]["MemberId"].ToString();
                enterby = dt1.Rows[0]["Enterby"].ToString();
                lbl_Username.Text = dt1.Rows[0]["Userid"].ToString();
                if (curmemid == Memidmain || enterby == Memidmain)
                {
                    passwordchng.Visible = true;
                    result = "Available";
                }
                else
                {
                    passwordchng.Visible = false;
                    result = "Notavailable";
                }
            }
            else
            {
                result = "Notavailable";
            }
            //if (curmemid == curssionmemid)
            //{
            //    result = "Available";
            //}
            //else
            //{
            //    result = "Notavailable";
            //}

            return result;
        }

        protected void Button2_Click(object sender, EventArgs e)
        { 
            SubmitNames();
            bindgrid();

        }
        public void SubmitNames()
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            SqlConnection con = new SqlConnection(strcon);
            cmd = new SqlCommand("SPTbl_Names", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;

            cmd.Parameters.AddWithValue("id", hdn_Userid.Value);
            cmd.Parameters.AddWithValue("Familyid", hdn_fmlyid.Value);
            cmd.Parameters.AddWithValue("Memberid", MemberId);
            cmd.Parameters.AddWithValue("FnameKwas", txt_Fnameknas.Text.Trim());
            cmd.Parameters.AddWithValue("FnameLegal", txt_FnameLegal.Text.Trim());
            cmd.Parameters.AddWithValue("MiddleName", txt_Emiddlename.Text.Trim());
            cmd.Parameters.AddWithValue("LastName", txt_LastName.Text.Trim());
            cmd.Parameters.AddWithValue("Type", ddl_Type.Text.Trim());
            cmd.Parameters.AddWithValue("ChangeDesc", ddl_Changedec.Text.Trim());
            cmd.Parameters.AddWithValue("ChangeDate", txt_date.Text.Trim());
            cmd.Parameters.AddWithValue("Date", date);
            cmd.Parameters.AddWithValue("Time", time);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            
            Session["Chilsnmemid"] = MemberId.ToString();
            if (Button2.Text == "Save")
            {
                cmd.Parameters.AddWithValue("StatementType", "insert");
                string line = "<div class='alert alert-success'><strong> Success!</strong> Data Updated Successfully</div> ";
                P1.InnerHtml = line.ToString();
            }
            else if (btn_Update.Text == "Update")
            {
                //cmd.Parameters.AddWithValue("StatementType", "Insert");
                //string line = "<div class='alert alert-success'><strong> Success!</strong> Data Saved Successfully</div> ";
                //P1.InnerHtml = line.ToString();
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                //Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                Response.Redirect("../FamilyTree/Edit_Parents.aspx");
            }
            else
            {
                string line = "<div class='alert alert-danger'><strong> Success!</strong> Submit Failed</div> ";
                //Bindnotice.InnerHtml = line.ToString();
            }

            con.Close();
        }
        public void bindgrid()
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_Names  where Memberid='" + MemberId + "'", con);
            adapt.Fill(dt);
            Grd_Names.DataSource = dt;
            Grd_Names.DataBind();
        }

        protected void lnkbtn_NameEdit_Click(object sender, EventArgs e)
        {
            string date = "";
            //       ,[Familyid]
            //,[Memberid]
            //,[FnameKwas]
            //,[FnameLegal]
            //,[MiddleName]
            //,[LastName]
            //,[Type]
            //,[ChangeDesc]
            //,[ChangeDate]
            string Fnameknas = "";
            string Fnamelegal = "";
            string Middlename = "";
            string LastName = "";
            string Type = "";
            string ChangeDesc = "";
            string ChangeDate = "";
            SqlConnection con = new SqlConnection(strcon);
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            string RowIndex = row.Cells[6].Text;
            string btnsts = null;
            string Query = "Select * from Tbl_Names where id='" + RowIndex + "'";
            DataTable dt = RetriveData(Query);
            //  string ordersts = dt.Rows[0]["Status"].ToString();
            if (dt.Rows.Count > 0)
            {
                Fnameknas = dt.Rows[0]["Fnameknas"].ToString();
                Fnamelegal = dt.Rows[0]["Fnamelegal"].ToString();
                Middlename = dt.Rows[0]["Middlename"].ToString();
                LastName = dt.Rows[0]["LastName"].ToString();
                Type = dt.Rows[0]["Type"].ToString();
                ChangeDesc = dt.Rows[0]["ChangeDesc"].ToString();
                ChangeDate = dt.Rows[0]["ChangeDate"].ToString();
                txt_Fnameknas.Text = Fnameknas;
                txt_FnameLegal.Text = Fnamelegal;

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

        protected void Grd_Names_RowEditing(object sender, GridViewEditEventArgs e)
        {
             
                Grd_Names.EditIndex = e.NewEditIndex;
                bindgrid();
             
           
        }

        protected void Grd_Names_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
                SqlConnection con = new SqlConnection(strcon);
                Label picid = Grd_Names.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("delete from Tbl_Names where id='" + Convert.ToInt32(picid.Text) + "'", con);
                    cmd.ExecuteNonQuery();
                    //SqlCommand cmd1 = new SqlCommand("delete from Tbl_Names where Pictureid='" + Convert.ToInt32(picid.Text) + "'", con);
                    //cmd1.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    bindgrid();
                }
                catch
                {

                } 
           
        }

        protected void Grd_Names_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grd_Names.EditIndex = -1;
            bindgrid();
        }

        protected void Grd_Names_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            TextBox Fnameknas = Grd_Names.Rows[e.RowIndex].FindControl("txt_Fnameknas") as TextBox;
            TextBox FnameLegal = Grd_Names.Rows[e.RowIndex].FindControl("txt_FnameLegal") as TextBox;
            TextBox Middlename = Grd_Names.Rows[e.RowIndex].FindControl("txt_MiddleName") as TextBox;
            TextBox Lastname = Grd_Names.Rows[e.RowIndex].FindControl("txt_Lastname") as TextBox;
            Label picid = Grd_Names.Rows[e.RowIndex].FindControl("lbl_ID") as Label;

            //updating the record  
            SqlCommand cmd = new SqlCommand("Update Tbl_Names set FnameKwas='" + Fnameknas.Text + "',FnameLegal='" + FnameLegal.Text + "',MiddleName='" + Middlename.Text + "',LastName='" + Lastname.Text + "' where id=" + Convert.ToInt32(picid.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            Grd_Names.EditIndex = -1;
            bindgrid();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (flud_img.HasFile)
            {
                string filename = Path.GetFileName(flud_img.FileName);
                string path = Path.Combine(Server.MapPath("~/FamilyTree/images/"), filename);
                flud_img.SaveAs(path);
                img_showimg.ImageUrl = "~/FamilyTree/images/" + filename;
            }
            else
            {
                img_showimg.ImageUrl = "~/FamilyTree/images/defaultimage.jpg";
            }
            if (ID == "" || ID != "")
            {
                //showaddnames.Visible = true;
                string pictureid = ID;
                string names = hdn_FullName.Value;
                string admin = "Admin";
                string date = DateTime.Now.ToString("yyyy/MM/dd");

                SqlCommand cmd = new SqlCommand("insert into Tbl_PicNames values('" + familyid + "','" + names + "','" + pictureid + "','" + admin + "','" + date + "')", con);
                SqlCommand cmd1 = new SqlCommand("insert into Tbl_Pictures values('"+ familyid + "','"+ Memidmain + "','"+ pictureid + "','"+ img_showimg.ImageUrl + "','"+txt_Desc.Text.Trim()+"','"+ date+"','"+""+"','"+ admin + "','" + date + "')", con);
          
                con.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                con.Close();
                //txt_Nmaes.Text = "";
                bindgrid();
                LoadGridview();
                //txt_Nmaes.Focus();
                lblMessage.Text = "Photo saved successfully";
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }
        public void getpic(string id)
        {
            if (id != "")
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                string str = "";
                str = "select * from Tbl_Pictures where id=" + id;
                SqlCommand sqlCmd = new SqlCommand(str, con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd); 
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    img_showimg.ImageUrl = dt.Rows[0]["Photo"].ToString();
                }
            }
        }

        protected void btn_Paswordchng_Click(object sender, EventArgs e)
        {
            if (txt_Confrmpswrd.Text.Trim() != txt_Newpswrd.Text.Trim())
            {
                lbl_pswrdchngmsg.Text = "Password doesn't match";
                lbl_pswrdchngmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string encryptedpswrd = SomeStaticClass.encrypt(txt_pswrd.Text.Trim());
                SqlConnection con = new SqlConnection(strcon);
                string str = "";
                //str = "select * from MainMembers where MemberId=" + MemberId + " and Password=" + encryptedpswrd;
                //SqlCommand sqlCmd = new SqlCommand(str, con);
                //SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                //sqlDa.Fill(dt);
                con.Open();
                string Query = "select * from MainMembers where MemberId='" + Memidmain + "' and Password='" + encryptedpswrd + "'";
                DataTable dt = RetriveData(Query);
                //  string ordersts = dt.Rows[0]["Status"].ToString();
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("Update MainMembers set Password='" + SomeStaticClass.encrypt(txt_Newpswrd.Text.Trim()) + "' where MemberId='" + Memidmain + "'", con);
                    cmd.ExecuteNonQuery();
                    lbl_pswrdchngmsg.Text = "Password changed successfully";
                    lbl_pswrdchngmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbl_pswrdchngmsg.Text = "Password is incorrect";
                    lbl_pswrdchngmsg.ForeColor = System.Drawing.Color.Red;
                }
                con.Close();
            }


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