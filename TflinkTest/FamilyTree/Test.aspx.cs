using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest.FamilyTree
{
    public partial class Test : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindtest();
            }
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
            com1.CommandText = "select * from MainMembers where " + " MemberId like @Search + '%'  and FamilyId='" + comid + "' ";
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
            com2.CommandText = "select * from MainMembers where " + " Contact like @Search + '%'  and FamilyId='" + comid + "' ";
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
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            gettext(); 
        }
        public void gettext()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string memid = "";
            try
            {
                memid = Session["Memberid"].ToString();
            }
            catch
            {

            }

            if (memid == "Memberid2")
            {

                //    txt_text.Visible = true;
                //    txt_text.Focus();
                //    int lines = 0;         // count lines
                //    int chars = 0;
                //    Query = "Select * from Test";
                //    DataTable dt3 = RetriveData(Query);
                //    string mydatatext = "";
                //    mydatatext = dt3.Rows[0]["Text"].ToString();

                //    lines = txt_text.Text.Trim().Length;
                //    chars = mydatatext.Length;
                //    if (lines != chars)
                //    {

                //        SqlCommand cmd = new SqlCommand("update  Test set Text='" + txt_text.Text.Trim() + "'", con);
                //        cmd.ExecuteNonQuery();
                //        lbl_typingsts.Visible = true;
                //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                //    }
                //    else
                //    {
                //        lbl_typingsts.Visible = false;
                //    }
                //    Query = "Select * from Test";
                //    DataTable dt5 = RetriveData(Query);
                //    lbl_mytext.Text = dt5.Rows[0]["Text"].ToString();

                //}
                //else
                //{
                //    Query = "Select * from Test";
                //    DataTable dt4 = RetriveData(Query);
                //    lbl_mytext.Text = dt4.Rows[0]["Text"].ToString(); 


                //}
                //con.Close();
            }
        }
        public DataTable RetriveData(string Query)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                da = new SqlDataAdapter(Query, con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        protected void txt_text_TextChanged(object sender, EventArgs e)
        {
           // gettext();
            txt_xhatmsg.Focus();
        }

        protected void btn_group_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Letschat values('"+txt_username.Text.Trim()+"','"+ txt_xhatmsg.Text.Trim()+"')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            txt_xhatmsg.Text = "";
        }

        protected void myset_Tick(object sender, EventArgs e)
        {
            bindmsg();
        }
        public void bindmsg()
        {
            string body = "";
            string msg = "";
            string username = "";
            Query = "Select * from Letschat";
            DataTable dt5 = RetriveData(Query);
            if(dt5.Rows.Count>0)
            {
                for(int i=0;i<dt5.Rows.Count;i++)
                {
                    msg = dt5.Rows[i]["chatmsg"].ToString();
                    username = dt5.Rows[i]["username"].ToString();
                    if(username == "")
                    {
                        username = "Unknown";
                    }
                    body = body + "<p style='color: aliceblue;'>-" + username + "-</p><p style='color: aliceblue;'>" + msg + "</p><br />";
                }
                mychat.InnerHtml = body;
            }
            else
            {
                mychat.InnerHtml = "<p>No chat Available</p>";
            }
           
            
        }
        public void bindtest()
        {
            string a = "a";
            for(int i=0;i<4;i++)
            {
                a = a + a;
                string bind = " <script type='text/javascript'> function ShowModalPopupchld() {$find('mpechld').show();return false;}</script>";
                string panelbind = "<asp:Panel ID='pnlPopUpchld' runat='server' CssClass='modalPopup' Style='display: none'>";
                panelbind = panelbind + "<div class='header'>Add Your Child Profile</div>";
                panelbind = panelbind + "<div class='body'><iframe id='iFramePersonalchld' src='Add_Child.aspx' height='400px' width='100%'></iframe>";
                panelbind = panelbind + "<a href='MyTree1.aspx'><asp:Button ID='btn_closechld' runat='server' class='btn btn-danger' Text='Close' /></a></div></asp:Panel>";
                panelbind = panelbind + "";
                string butbind = "<input value='button' name='btn_Addchld' type='button' class='btn btn-info'  onclick='return ShowModalPopupchld()' />";

                butbind = butbind + "";
                butbind = butbind + "";
                getvalues.InnerHtml = bind + "<br/>" + panelbind + "<br/>";
                btnbind.InnerHtml = butbind.ToString();
            }
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "Test.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}