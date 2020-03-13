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

namespace TflinkTest
{
    public partial class Signup : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        Secureconnection connect = new Secureconnection();
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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
            List<string> txtItems = new List<string>();
            //try
            //{ 
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString);

            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            com.Connection = con;
            con.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = "select distinct Userid from MainMembers where " + "Userid=@Search";
            com.Parameters.AddWithValue("@Search", prefixText);
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {

                txtItems.Add("This Username is Already Registered");
                //foreach (DataRow row in dt.Rows)
                //{
                //    //String From DataBase(dbValues)
                //    dbValues = row["Userid"].ToString();

                //    txtItems.Add(dbValues);
                //}
            }
            return txtItems.ToArray();

        }
        [WebMethod]

        [System.Web.Script.Services.ScriptMethod()]

        public static string[] GetCompletionListEmail(string prefixText, int count)
        {
            return AutoFillProductEmail(prefixText);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        private static string[] AutoFillProductEmail(string prefixText)
        {
            List<string> txtItems = new List<string>();
            //try
            //{ 
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString);

            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            com.Connection = con;
            con.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = "select distinct EmailId from MainMembers where " + "EmailId=@Search";
            com.Parameters.AddWithValue("@Search", prefixText);
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {

                txtItems.Add("This Email id is Already Registered");

            }
            return txtItems.ToArray();

        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Submitdata();
        }
        public void Submitdata()
        {
            //{
            //    SqlConnection con = connect.GetEMployeeconnnect();
            SqlConnection con = new SqlConnection(strcon);
            string Userid = "";
            string Password = "";
            string Emailid = "";
            Userid = txt_Username.Text.Trim();
            Password = txt_Password.Text.Trim();
            Emailid = txt_Emailid.Text.Trim();

            string creradod = "";
            string creradob = "";
            string encryptedString = SomeStaticClass.encrypt(Password.ToString());
            // string decryptedString = SomeStaticClass.Decrypt(encryptedString);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            cmd = new SqlCommand("SPMainMembersignup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EmailId", Emailid.ToString());
            cmd.Parameters.AddWithValue("Userid", Userid.ToString());
            cmd.Parameters.AddWithValue("Password", encryptedString);
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("Enterdate", date);
            cmd.Parameters.AddWithValue("Wrngpswatmpt", "0");
            cmd.Parameters.AddWithValue("Entertime", time);
            cmd.Parameters.AddWithValue("StatementType", "Insert");
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                Response.Write("<script>alert('Sign Up Successfully. Username=" + Userid.ToString() + " Password=" + Password.ToString() + "')</script>");
                btn_Save.Text = "User Created Successfully";
                // msg.InnerText = "User Created Successfully";
                //lbl_msg.Text = "User Created Successfully";
                Session["Slno"] = null;
                clear();
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
            }

            con.Close();
        }
        public void clear()
        {
            txt_Emailid.Text = "";
            txt_Password.Text = "";
            txt_Username.Text = "";
        }

        protected void txt_Username_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();

            con.Open();
            string str = "select * from MainMembers where Userid='" + txt_Username.Text.Trim() + "'";
            SqlCommand sqlCmd = new SqlCommand(str, con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_Username.Text = "";
            }
            else if (txt_Username.Text.Trim() == "This Username is Already Registered")
            {
                txt_Username.Text = "";
            }
            else
            {
                // txt_Emailid.Focus();
            }
        }

        protected void txt_Emailid_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();

            con.Open();
            string str = "select * from MainMembers where EmailId='" + txt_Emailid.Text.Trim() + "'";
            SqlCommand sqlCmd = new SqlCommand(str, con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_Emailid.Text = "";
            }
            else if (txt_Emailid.Text.Trim() == "This Email id is Already Registered")
            {
                txt_Emailid.Text = "";
            }
            else
            {
                // txt_Password.Focus();
            }
        }
    }
}