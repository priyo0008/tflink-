using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TflinkTest
{
    public partial class Default : System.Web.UI.Page
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
        //public void getdata()
        //{
        //    //Get data show in textbox

        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlCommand cmd = null;
        //    cmd = new SqlCommand("SPUpdateHome", con);
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }

        //    SqlDataReader reader = cmd.ExecuteReader();

        //    List<Test> TestList = new List<Test>();
        //    Test test = null;

        //    if (reader.HasRows)
        //    {
        //        reader.Read();
        //        content.InnerHtml = Convert.ToString(reader["Desccription"]); 
        //    }
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }


        //}
        public void getdata()
        {
            //Get data show in textbox
            try
            {

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = null;
            cmd = new SqlCommand("SPselectHome", con);
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
                content.InnerHtml = Convert.ToString(reader["Desccription"]);
                //hdn_id.Value = Convert.ToString(reader["id"]);
                //btn_save.Text = "Update";
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