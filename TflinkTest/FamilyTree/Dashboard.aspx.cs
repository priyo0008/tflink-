using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest.FamilyTree
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string Query = null;
        DataTable dt;
        string Memid = "";
        string Fnamess = "";
        string employeeid = "";
        string userid = "";
        string memberidbysub = "";
        string familyid = "";
        string newid = "";

        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                showall();
            }
        }
        public void showall()
        {
            int countnmb = 0;
            string Query = "select * from MainMembers";
            DataTable dt = RetriveData(Query);
            if (dt.Rows.Count > 0)
            {
             //   lbl_reg.Text = dt.Rows.Count.ToString();
            }
           // lbl_curonline.Text = Application["TotalOnlineUsers"].ToString();
            string Query1 = "select * from Count_Visit";
            DataTable dt1 = RetriveData(Query1);
            if (dt1.Rows.Count > 0)
            {
             //   lbl_visit.Text = dt1.Rows[0]["Count"].ToString();
            }
            string Query2 = "select * from Count_Visit";
            DataTable dt2 = RetriveData(Query1);
            if (dt2.Rows.Count > 0)
            {
                countnmb = Convert.ToInt32(dt2.Rows[0]["Count"].ToString());
            }
          //  lbl_admnstrt.Text = countnmb.ToString();
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