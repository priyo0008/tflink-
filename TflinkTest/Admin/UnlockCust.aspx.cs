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

namespace TflinkTest.Admin
{
    public partial class UnlockCust : System.Web.UI.Page
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
        string Memid = "";
        string MemberId = "";

        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lockcustbind();
            }
        }
        public void Lockcustbind()
        {
            SqlConnection con = new SqlConnection(strcon);
            string memid = "";
            try
            {
                memid = Session["MemberId"].ToString();
            }
            catch
            {

            }

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            string status = "Lock";
            SqlDataAdapter adapt = new SqlDataAdapter("select * from MainMembers", con);
            //SqlDataAdapter adapt1 = new SqlDataAdapter("select * from MainMembers where Wrngpswatmpt = '" + status + "'", con);
            
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Grd_Custlock.DataSource = dt;
                Grd_Custlock.DataBind();
            }
            else
            {
                myrequest.InnerText = "No data found";
            }

        }

        protected void lnkbtn_live_Click(object sender, EventArgs e)
        {
            string date = "";
            SqlConnection con = new SqlConnection(strcon);
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            string RowIndex = row.Cells[6].Text;
            string btnsts = null;
            string Query = "Select * from MainMembers where id='" + RowIndex + "'";
            DataTable dt = RetriveData(Query);
            //  string ordersts = dt.Rows[0]["Status"].ToString();
            if (dt.Rows.Count > 0)
            {
                btnsts = dt.Rows[0]["Wrngpswatmpt"].ToString();
                date = btnsts;
            }
            if (btnsts == "Lock")
            {
                //date = DateTime.Now.ToString("yyyy/MM/dd");
                btnsts = "0"; 
            }
            else if (btnsts == "0" || btnsts == "1" || btnsts == "2" || btnsts == "3" || btnsts == "4" || btnsts == "5")
            {
                btnsts = "Lock";
                //btnsts = "Accept";
            }
            Query = "update MainMembers set Wrngpswatmpt='" + btnsts + "' where id='" + RowIndex + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Lockcustbind();
            if (btnsts == "Lock")
            {
                //check the request is comming for what
                //Check that member is already added as that or not
                //Add that member to all table as per request
            }
        }
        public DataTable RetriveData(string Query)
        {
            //try
            //{
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            // }
            //catch
            //{
            //    return dt;
            //}
        }

        public void mydate()
        {
            
        }
    }
}