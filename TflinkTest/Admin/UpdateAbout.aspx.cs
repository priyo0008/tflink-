using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TflinkTest.Admin
{
    public partial class UpdateAbout : System.Web.UI.Page
    {
        SqlCommand cm = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string employeeid = "";
        string userid = "";

        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getdata();
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            //submit and update content
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                int S = 0;
                int U = 0;
                string curdate = System.DateTime.Today.ToString("yyyy/MM/dd");

                SqlCommand cmd = null;
                cmd = new SqlCommand("SPUpdateabout", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", hdn_id.Value);
                cmd.Parameters.AddWithValue("Desccription", txt_Aboutcontent.Text);
                cmd.Parameters.AddWithValue("Enterby", "Admin");
                cmd.Parameters.AddWithValue("Enterdt", curdate);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (btn_save.Text == "Save")
                {
                    cmd.Parameters.Add("@StatementType", SqlDbType.Text).Value = "Insert";
                    S = cmd.ExecuteNonQuery();
                    //Response.Write("<script>alert('Added Successfully.')</script>");
                    lbl_Success.Visible = true;
                    lbl_Update.Visible = false;
                    lbl_error.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    btn_save.Text = "Update";
                }
                else
                {
                    cmd.Parameters.Add("@StatementType", SqlDbType.Text).Value = "Update";
                    U = cmd.ExecuteNonQuery();
                    //Response.Write("<script>alert('Updated Successfully.')</script>");
                    lbl_Update.Visible = true;
                    lbl_Success.Visible = false;
                    lbl_error.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel1();", true);
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch
            {
                lbl_error.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel2();", true);
            }
        }
        public void getdata()
        {
            //Get data show in textbox

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = null;
            cmd = new SqlCommand("SPselectAbout", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataReader reader = cmd.ExecuteReader();

            List<Test> TestList = new List<Test>();
            Test test = null;

            if (reader.HasRows)
            {
                reader.Read();
                txt_Aboutcontent.Text = Convert.ToString(reader["Desccription"]);
                hdn_id.Value = Convert.ToString(reader["id"]);
                btn_save.Text = "Update";
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }


        }
    }
}