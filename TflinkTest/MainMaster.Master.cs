using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace TflinkTest
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;

        //Secureconnection connect = new Secureconnection();
        SqlCommand cmd;
        SqlDataReader rd;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        string Query = "";
        string Memberid = "";
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Memberid = Session["Memberid"].ToString();
            }
            catch
            {

            }
            // lbl_online.Text = Application["TotalOnlineUsers"].ToString();
            if (!IsPostBack)
            {
             
                insetrvisiterscount();
                if (Memberid != "")
                {
                    btn_loginv.Text = "Log Out";
                }
                else
                {
                    btn_loginv.Text = "Log in";
                }
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            unlockLogin();
        }
        public void unlockLogin()
        {
            //SqlConnection con = connect.GetEMployeeconnnect();
            SqlConnection con = new SqlConnection(strcon);
            string Uderid = "";
            string password = "";
            string Fname = "";
            string Wrngpswatmpt = "";
            int nmbWrngpswatmpt = 0;
            string wrngpswdate = "";
            string timewrn = "";
            int cn = 0;
            con.Open();
            string encryptedpswrd = SomeStaticClass.encrypt(txt_Password.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter("select * from MainMembers where Userid='" + txt_Userid.Text.Trim() + "'", con);
            string Query = "select * from MainMembers where Userid='" + txt_Userid.Text.Trim() + "' ";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Wrngpswatmpt = dt.Rows[0]["Wrngpswatmpt"].ToString();
                password = dt.Rows[0]["Password"].ToString();
                timewrn = dt.Rows[0]["WrnpswTime"].ToString();
                wrngpswdate = dt.Rows[0]["WrnpswDate"].ToString();
                string time1 = DateTime.Now.ToString("hh:mm:ss");
                DateTime t1 = DateTime.Now;

                DateTime systemDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                if (timewrn == "")
                {
                    timewrn = time1;
                }
                DateTime t2 = Convert.ToDateTime(timewrn);

                int i = DateTime.Compare(t1, t2);

                if (Wrngpswatmpt == "Lock")
                {
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    string unlock = "Unlocked";
                    DateTime dtwrng = Convert.ToDateTime(wrngpswdate.ToString());
                    dtwrng = dtwrng.AddDays(1);
                    if (systemDate == dtwrng)
                    {
                        if (i > 0)
                        {
                            SqlCommand cmd = new SqlCommand("update MainMembers set Wrngpswatmpt='" + unlock + "'  where Userid='" + txt_Userid.Text.Trim() + "'", con);
                            cmd.ExecuteNonQuery();
                            unlockLogin();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('You have entered wrong password more than 5 times,Please contact to admin or wait for 24 hrs to unlock your account!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('You have entered wrong password more than 5 times,Please contact to admin or wait for 24 hrs to unlock your account!');", true);
                    }
                }
                else if (encryptedpswrd == password)
                {
                    Fname = dt.Rows[0]["FirstName"].ToString();
                    Uderid = txt_Userid.Text.Trim();
                    Session["UserId"] = Uderid.ToString();
                    Session["Password"] = password.ToString();
                    Session["FirstName"] = Fname.ToString();
                    Session["Familyid"] = dt.Rows[0]["FamilyId"].ToString();

                    string ddd = dt.Rows[0]["FamilyId"].ToString();
                    Response.Redirect("~/FamilyTree/MyTree1.aspx");
                }
                else
                { 
                    if (Wrngpswatmpt == null || Wrngpswatmpt == "")
                    {
                        Wrngpswatmpt = "0";
                    }
                    nmbWrngpswatmpt = Convert.ToInt32(Wrngpswatmpt.ToString());
                    nmbWrngpswatmpt = nmbWrngpswatmpt + 1;

                    if (nmbWrngpswatmpt < 5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Password is Incorrect! Attempt -" + nmbWrngpswatmpt.ToString() + " ');", true);
                        SqlCommand cmd = new SqlCommand("update MainMembers set Wrngpswatmpt='" + nmbWrngpswatmpt.ToString() + "'  where Userid='" + txt_Userid.Text.Trim() + "'", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string loc = "Lock";
                        string date = DateTime.Now.ToString("yyyy/MM/dd");
                        string time = DateTime.Now.ToString("hh:mm:ss");
                        SqlCommand cmd = new SqlCommand("update MainMembers set Wrngpswatmpt='" + loc + "',WrnpswDate='" + date + "',WrnpswTime='" + time + "'  where Userid='" + txt_Userid.Text.Trim() + "' ", con);
                        cmd.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Your account has been locked for 24 hrs!');", true);
                    }
                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Username is incorrect!');", true);
            }
            con.Close();
        }

        protected void btn_Signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Signup.aspx#home_signup");
        }

        protected void btn_join_Click(object sender, EventArgs e)
        {
            if (btn_loginv.Text == "Log in")
            {
                Response.Redirect("/Home.aspx");
            }
            else
            {
                Response.Redirect("/FamilyTree/Logout.aspx");
            }
        }

        protected void btn_Joinus_Click(object sender, EventArgs e)
        {
            if (Memberid != "")
            {
                Response.Redirect("JoinUs.aspx");
            }
            else
            {
                btn_loginv.Text = "Log in";
            }
        }
        public void insetrvisiterscount()
        {
            SqlConnection con = new SqlConnection(strcon);
            string sescount = "";
            int countnmb = 0;
            try
            {
                sescount = Session["count"].ToString();
            }
            catch
            {

            }
            if (sescount == "")
            {
                string Query1 = "select * from Count_Visit";
                DataTable dt1 = RetriveData(Query1);
                if (dt1.Rows.Count > 0)
                {
                    countnmb = Convert.ToInt32(dt1.Rows[0]["Count"].ToString());
                    Session["count"] = "Active";
                    countnmb = countnmb + 1;
                    SqlCommand cmd = new SqlCommand("update Count_Visit set Count='" + countnmb + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session["count"] = "Active";
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("insert into Count_Visit values('" + countnmb + "')", con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Session["count"] = "Active";
                } 

            }
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
 
    }
}
