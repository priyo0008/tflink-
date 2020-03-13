using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        SqlDataAdapter da;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    showall();
                }
                catch
                {

                } 
            }
        }
        public void showall()
        {
            string Query = "select * from MainMembers";
            DataTable dt = RetriveData(Query);
            if (dt.Rows.Count > 0)
            {
                // lbl_reg.Text = dt.Rows.Count.ToString();
            }
            curonline.InnerText = Application["TotalOnlineUsers"].ToString();
            // lbl_visit.Text = "DB not created";
            // lbl_admnstrt.Text = "Need to add column";
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