using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TflinkTest.Admin
{
    public partial class UpdateSearch : System.Web.UI.Page
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

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            try
            {


                SqlConnection con = new SqlConnection(strcon);
                string curdate = System.DateTime.Today.ToString("yyyy/MM/dd");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string admin = "Admin";
                if (flud_image.HasFile)
                {
                    string filename = Path.GetFileName(flud_image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Admin/Image/"), filename);
                    flud_image.SaveAs(path);
                    img_imagesearch.ImageUrl = "~/Admin/Image/" + filename;
                }
                if (btn_Upload.Text == "Save")
                {
                    SqlCommand cmd = new SqlCommand("insert into UpdateSearch values('" + img_imagesearch.ImageUrl + "','" + admin + "','" + curdate + "')", con);
                    cmd.ExecuteNonQuery();
                    lbl_Success.Visible = true;
                    lbl_Update.Visible = false;
                    lbl_error.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    btn_Upload.Text = "Update";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("update UpdateSearch set Image='" + img_imagesearch.ImageUrl + "',Enterby='" + admin + "',Enterdt='" + curdate + "' where id='" + hdn_id.Value + "'", con);
                    cmd.ExecuteNonQuery();
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
            String Query = "select * from UpdateSearch order by id desc";
            DataTable dt = RetriveData(Query);

            if (dt.Rows.Count > 0)
            {
                img_imagesearch.ImageUrl = dt.Rows[0]["Image"].ToString();
                hdn_id.Value = dt.Rows[0]["id"].ToString();
                btn_Upload.Text = "Update";
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