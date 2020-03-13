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
using System.Globalization;

namespace TflinkTest
{
    public partial class Search : System.Web.UI.Page
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
                ddl_Country.DataSource = CountryList();
                ddl_Country.DataBind();

            }
        }
        public static List<string> CountryList()
        {
            //Creating list
            List<string> CultureList = new List<string>();

            //getting  the specific  CultureInfo from CultureInfo class
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            CultureList.Add("[Not Specified]");
            foreach (CultureInfo getCulture in getCultureInfo)
            {
                //creating the object of RegionInfo class
                RegionInfo GetRegionInfo = new RegionInfo(getCulture.LCID);
                //adding each county Name into the arraylist
                if (!(CultureList.Contains(GetRegionInfo.EnglishName)))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            //sorting array by using sort method to get countries in order
            CultureList.Sort();
            //returning country list
            return CultureList;
        }
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_Fname.Text == "")
            {
                lbl_lname.Text = "";
                lbl_country.Text = "";
                lbl_fname.Text = "Enter First Name";
            }
            else if (txt_Lastname.Text == "")
            {
                lbl_fname.Text = "";
                lbl_country.Text = "";
                lbl_lname.Text = "Enter Last Name";
            }
            else if (ddl_Country.Text == "Select Country")
            {
                lbl_fname.Text = "";
                lbl_lname.Text = "";
                lbl_country.Text = "[Not Specified]";
            }
            else
            {
                perdonfound.Visible = true;
                Administrator.Visible = true;
                lbl_lname.Text = "";
                lbl_fname.Text = "";
                lbl_country.Text = "";
                SqlConnection con = new SqlConnection(strcon);
                DataTable dt = new DataTable();
                string dob = "";
                con.Open();
                string str = "select * from MainMembers where FirstName='" + txt_Fname.Text.Trim() + "' and LastName='" + txt_Lastname.Text.Trim() + "' and Country='" + ddl_Country.Text.Trim() + "'";
                SqlCommand sqlCmd = new SqlCommand(str, con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dob = dt.Rows[0]["Dob"].ToString();
                    string yr = DateTime.Now.ToString("yyyy");
                    string yr1 = Convert.ToDateTime(dob).ToString("yyyy");
                    int age = Convert.ToInt32(yr) - Convert.ToInt32(yr1);
                    if (age >= 18)
                    {
                        perdonfound.InnerText = "Person Found";
                        Administrator.InnerText = "Member Id : " + dt.Rows[0]["MemberId"].ToString();
                        txt_Fname.Text = "";
                        txt_Lastname.Text = "";
                        showpart.Visible = true;
                        searchpart.Visible = false;
                    }
                    else
                    {
                        Administrator.InnerText = "";
                        perdonfound.InnerText = "Person Not Found";
                        Administrator.InnerText = "";
                        showpart.Visible = true;
                        searchpart.Visible = false;
                    }
                }
                else
                {
                    Administrator.InnerText = "";
                    perdonfound.InnerText = "Person Not Found";
                    Administrator.InnerText = "";
                    showpart.Visible = true;
                    searchpart.Visible = false;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            showpart.Visible = false;
            searchpart.Visible = true;
            perdonfound.Visible = false;
            Administrator.Visible = false;
        }
    }
}