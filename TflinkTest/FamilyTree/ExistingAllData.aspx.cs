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

namespace TflinkTest.FamilyTree
{
    public partial class ExistingAllData : System.Web.UI.Page
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
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLinkw"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        public void Getdata()
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            if(ddl_Getdata.Text.Trim() == "Userid-Password")
            {
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT TOP 1000  au.UserName, am.Password FROM[tflink].[dbo].[aspnet_Users] au INNER JOIN aspnet_Membership am ON  au.UserId = am.UserId", con);

                adapt.Fill(dt);
            }
            else
            {
                SqlDataAdapter adapt = new SqlDataAdapter("select * from " + ddl_Getdata.Text.Trim() + "", con);
                adapt.Fill(dt);
            }
            
            
            Grd_Pofile.DataSource = dt;
            Grd_Pofile.DataBind();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        protected void ddl_Getdata_TextChanged(object sender, EventArgs e)
        {
            Getdata();
        }
    }
}