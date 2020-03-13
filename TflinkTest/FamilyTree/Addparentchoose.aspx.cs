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
    public partial class Addparentchoose : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getgrd();
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
            com.CommandText = "select * from MainMembers where " + "FirstName like @Search + '%' ";
            com.Parameters.AddWithValue("@Search", prefixText);
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string faname = "";
                    //string lname = "";
                    //string memid = "";
                    //String From DataBase(dbValues)
                    faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    //memid = row["MemberId"].ToString();
                    txtItems.Add(faname);
                }
            }
            SqlCommand com1 = new SqlCommand();
            DataTable dt1 = new DataTable();
            com1.Connection = con;
            con.Open();
            com1.CommandType = CommandType.Text;
            com1.CommandText = "select * from MainMembers where " + " MemberId like @Search + '%'  ";
            com1.Parameters.AddWithValue("@Search", prefixText);
            com1.ExecuteNonQuery();
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            da1.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    // string faname = "";
                    //string lname = "";
                    string memid = "";
                    //String From DataBase(dbValues)
                    //  faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    memid = row["MemberId"].ToString();
                    txtItems.Add(memid);
                }
            }
            SqlCommand com2 = new SqlCommand();
            DataTable dt2 = new DataTable();
            com2.Connection = con;
            con.Open();
            com2.CommandType = CommandType.Text;
            com2.CommandText = "select * from MainMembers where " + " Contact like @Search + '%'  ";
            com2.Parameters.AddWithValue("@Search", prefixText);
            com2.ExecuteNonQuery();
            SqlDataAdapter da2 = new SqlDataAdapter(com2);
            da2.Fill(dt2);
            con.Close();
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow row in dt2.Rows)
                {
                    // string faname = "";
                    //string lname = "";
                    string memid = "";
                    //String From DataBase(dbValues)
                    //  faname = row["FirstName"].ToString();
                    //lname = row["LastName"].ToString();
                    memid = row["Contact"].ToString();
                    txtItems.Add(memid);
                }
            }

            return txtItems.ToArray();
        }

        protected void txt_search_TextChanged(object sender, EventArgs e)
        {
            //LoadGridview();
            getgrd();
        }
        protected void LoadGridview()
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from MainMembers where Contact='" + txt_search.Text.Trim() + "' or FirstName='" + txt_search.Text.Trim() + "' or MemberId='" + txt_search.Text.Trim() + "'  order by id desc", con);
            con.Open();
            adapt.Fill(dt);
            grd_bindmembers.DataSource = dt;
            grd_bindmembers.DataBind();
            con.Close();
        }
        public void getgrd()
        {
            SqlConnection con = new SqlConnection(strcon);
            da = new SqlDataAdapter("select * from MainMembers where Contact='" + txt_search.Text.Trim() + "' or MemberId='" + txt_search.Text.Trim() + "' or FirstName='" + txt_search.Text.Trim() + "'", con);
            ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < grd_bindmembers.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)grd_bindmembers.Rows[i].Cells[0].FindControl("rdo_check");
               // rb.GroupName = "ff" + i.ToString();
                if (rb != null)
                {
                    if (rb.Checked == true)
                    {
                       
                    }
                }
            }
            //foreach (GridViewRow row in grd_bindmembers.Rows) 
            //{
            //    RadioButton rdoDelDt = (RadioButton)row.FindControl("rdo_check"); 
            //    if (rdoDelDt.Checked == true) 
            //    { 
            //        string date = grd_bindmembers.DataKeys[row.RowIndex].Value.ToString(); 
            //    } 
            //}
            grd_bindmembers.DataSource = ds.Tables[0];
            grd_bindmembers.DataBind();
        }

        protected void btn_chk_Click(object sender, EventArgs e)
        {
            getgrd();
        }

    }
}