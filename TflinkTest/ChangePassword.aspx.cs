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
    public partial class ChangePassword : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string Memberid = "";
        string familyid = "";
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        Secureconnection connect = new Secureconnection();
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Memberid = Session["Memberid"].ToString();
                familyid = Session["Familyid"].ToString();
            }
            catch
            {

            }
            if (!IsPostBack)
            {

            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            string encryptpaswordold = SomeStaticClass.encrypt(txt_Password.Text.Trim());
            string encryptpasswordnew = SomeStaticClass.encrypt(txt_Newpassword.Text.Trim());
            if (txt_Password.Text == "")
            {
                lbl_oldpaswrd.Text = "Enter Your Password";
                lbl_Newpswrd.Text = "";
                lbl_connewpswrd.Text = "";
            }
            else if (txt_Newpassword.Text == "")
            {
                lbl_oldpaswrd.Text = "";
                lbl_Newpswrd.Text = "Enter New Password";
                lbl_connewpswrd.Text = "";
            }
            else if (txt_connewpswrd.Text == "Select Country")
            {
                lbl_oldpaswrd.Text = "";
                lbl_Newpswrd.Text = "";
                lbl_connewpswrd.Text = "Enter Password Again";
            }
            else if (txt_connewpswrd.Text != txt_Newpassword.Text)
            {
                lbl_oldpaswrd.Text = "";
                lbl_Newpswrd.Text = "";
                lbl_connewpswrd.Text = "Doesn't Match With Password";
            }
            else if (checkpswrd(Memberid, familyid, encryptpaswordold) == 0)
            {
                lbl_oldpaswrd.Text = "Password is incorrect";
                lbl_Newpswrd.Text = "";
                lbl_connewpswrd.Text = "";
            }
            else
            {
                lbl_oldpaswrd.Text = "";
                lbl_Newpswrd.Text = "";
                lbl_connewpswrd.Text = "";
                SqlCommand cmd = new SqlCommand("update MainMembers set Password='" + encryptpasswordnew + "' where FamilyId='" + familyid + "' and MemberId='" + Memberid + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                txt_Password.Text = "";
                txt_Newpassword.Text = "";
                txt_connewpswrd.Text = "";
                btn_Save.Text = "Successfully Changed";
                // Memberid
            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_Password.Text = "";
            txt_Newpassword.Text = "";
            txt_connewpswrd.Text = "";
            lbl_connewpswrd.Text = "";
            lbl_Newpswrd.Text = "";
            lbl_oldpaswrd.Text = "";
            btn_Save.Text = "Change Password";
        }
        public int checkpswrd(string memid, string fmlyid, string password)
        {
            int result = 0;
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da1 = new SqlDataAdapter("select * from MainMembers where MemberId='" + memid + "' and FamilyId='" + fmlyid + "' and password='" + password + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                result = 1;
                // fnamemember = dt1.Rows[0]["FirstName"].ToString(); 
            }
            return result;
        }
    }
}