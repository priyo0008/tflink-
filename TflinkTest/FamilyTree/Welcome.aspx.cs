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
    public partial class Welcome : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;

        //Secureconnection connect = new Secureconnection();
        SqlCommand cmd;
        SqlDataReader rd;
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        string Query = "";
        string Memberid = "";
        AppCode.encryption SomeStaticClass = new AppCode.encryption();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Memberid = Session["Mainmemid"].ToString();
            }
            catch
            {

            }
            if (!IsPostBack)
            {
                showall();
                bindgrid();
            }
        }
        public void showall()
        {
            string Query = "select * from MainMembers";
            DataTable dt = RetriveData(Query);
            if (dt.Rows.Count > 0)
            {
                lbl_reg.Text = dt.Rows.Count.ToString();
            }
            lbl_curonline.Text = Application["TotalOnlineUsers"].ToString();
            lbl_visit.Text = "DB not created";
            lbl_admnstrt.Text = "Need to add column";
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
        public void bindgrid()
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            DateTime dt1 = DateTime.Now;
            string password = "";
            SqlDataAdapter adapt = new SqlDataAdapter("select * from MainMembers", con);
            adapt.Fill(dt);
            Grd_Pofile.DataSource = dt;
            Grd_Pofile.DataBind();
            //foreach (GridViewRow row in Grd_Pofile.Rows)
            //{
            //     password = Grd_Pofile.DataKeys[row.RowIndex].Value.ToString();
            //    password =  SomeStaticClass.encrypt(password);
            //   Grd_Pofile.DataKeys[row.RowIndex].Value = "";

            //Grd_Pofile.DataKeys[row.RowIndex].Value.ToString() = password;
            //for (int i = 0; i < Grd_Pofile.Rows.Count; i++)
            //{
            //    password = Grd_Pofile.Rows.Cells[0].Text;
            //    int sub = Convert.ToInt32(Grd_Pofile.Rows.Cells[1].Text); 
            //}
            try
            {
                foreach (GridViewRow row in Grd_Pofile.Rows)
                {
                     
                    //    String header = Grd_Pofile.Columns[i].HeaderText;
                        password = row.Cells[5].Text;
                        password = SomeStaticClass.Decrypt(password);
                        row.Cells[5].Text = password.ToString(); 
                }
            }
            catch
            {

            }
        
        }
    }
}