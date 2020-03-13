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
    public partial class ProfileList : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
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
                todaysdue();
            }
        }
        public void todaysdue()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataTable dt = new DataTable();
                DateTime dt1 = DateTime.Now;
                SqlDataAdapter adapt = new SqlDataAdapter("select * from MainMembers where Familyid='" + familyid + "'", con);
                adapt.Fill(dt);
                Grd_Pofile.DataSource = dt;
                Grd_Pofile.DataBind();

                string datetoday = dt1.ToString("MM/dd/yyyy");
                string date = "";
                string yr1 = "";
                string yr = "";
                string status = "";

                if (dt.Rows.Count > 0)
                {
                    foreach (GridViewRow row in Grd_Pofile.Rows)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            string memname = row.Cells[1].Text;
                            string dob = row.Cells[4].Text;
                            string dod = row.Cells[6].Text;
                            status = row.Cells[7].Text;
                            if (dod != "" && status != "Not Available")
                            { 
                                yr = Convert.ToDateTime(dod).ToString("yyyy");
                                yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                            }
                            else
                            {
                                yr = DateTime.Now.ToString("yyyy");
                                yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                            }
                           
                            int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                            row.Cells[4].Text = age.ToString();
                            row.Cells[6].Text = "";
                            row.Cells[7].Text = "";
                            //date = row.Cells[5].Text;
                            //string price = row.Cells[4].Text;
                            //float totdues = float.Parse(price);
                            //row.Cells[4].Text = Math.Round(totdues, 2).ToString();

                        }
                        //if (datetoday == date)
                        //{

                        //    row.BackColor = System.Drawing.Color.Green;
                        //    row.ForeColor = System.Drawing.Color.White;

                        //}
                        //else
                        //{
                        //    row.Visible = false;
                        //}
                    }
                }
                else
                {

                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch
            {

            }

        }
    }
}