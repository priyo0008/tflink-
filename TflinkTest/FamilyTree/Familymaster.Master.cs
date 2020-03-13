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

namespace TflinkTest.FamilyTree
{
    public partial class Familymaster : System.Web.UI.MasterPage
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
            try
            {
                string Memberid = Session["Memberid"].ToString();
                bindreqcount(Memberid);
            }
            catch
            {

            }
            if (!IsPostBack)
            {

            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            try
            {
                Session["UserId"] = null;
                Session["Password"] = null;
                Session["FirstName"] = null;
            }
            catch
            {

            }
            Response.Redirect("~/Home.aspx");
        }
        public void bindreqcount(string memid)
        {
            string acpt = "Accept";
            string Regstatus = "Reject";
            string Query = "select * from Tbl_AllRequests where RequestTo='" + memid + "' and Status='" + acpt + "' and Regstatus='" + Regstatus + "'";
            DataTable dt = RetriveData(Query);
            if (dt.Rows.Count > 0)
            {
                bindcountreq.InnerText = "(" + dt.Rows.Count.ToString() + ")";
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