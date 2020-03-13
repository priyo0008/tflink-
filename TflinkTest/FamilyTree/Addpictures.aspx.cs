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
    public partial class Addpictures : System.Web.UI.Page
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
        string ID = "";
        string delid = "";
        //Secureconnection connect = new Secureconnection();
        string strcon = ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                familyid = Session["Familyid"].ToString();
                memberidbysub = Session["Memberid"].ToString();
            }
            catch
            {

            }
            try

            {
                ID = Request.QueryString["id"].ToString();
                if (ID != "")
                {
                    showaddnames.Visible = true;
                    getpic(ID);
                }
            }
            catch
            {

            }
            try
            {
                delid = Request.QueryString["delId"].ToString();
                if (delid != "")
                {
                    deletepic(delid);
                }
            }
            catch
            {

            }
            if (!IsPostBack)
            {
                LoadGridview();
                //img_showimg.ImageUrl = "images/defaultimage.jpg";
            }
        }
        [WebMethod]

        [System.Web.Script.Services.ScriptMethod()]

        public static string[] GetCompletionListnames(string prefixText, int count)
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
            string familyid = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FamilyLink"].ConnectionString);
            try
            {
                familyid = HttpContext.Current.Session["Familyid"].ToString();
            }
            catch
            {

            }
            SqlCommand com = new SqlCommand();
            DataTable dt = new DataTable();
            com.Connection = con;
            con.Open();
            com.CommandType = CommandType.Text;
            com.CommandText = "select distinct FirstName from MainMembers where " + "FirstName like @Search + '%' and FamilyId='" + familyid + "'";
            com.Parameters.AddWithValue("@Search", prefixText);
            com.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            { 
                //txtItems.Add("This Username is Already Registered");
                foreach (DataRow row in dt.Rows)
                {
                    string dbValues = "";
                    //String From DataBase(dbValues)
                    dbValues = row["FirstName"].ToString(); 
                    txtItems.Add(dbValues);
                }
            }
            return txtItems.ToArray();

        }
        protected void btn_Addpics_Click(object sender, EventArgs e)
        {
            if (flud_img.HasFile)
            {
                string filename = Path.GetFileName(flud_img.FileName);
                string path = Path.Combine(Server.MapPath("~/FamilyTree/images/"), filename);
                flud_img.SaveAs(path);
                img_showimg.ImageUrl = "~/FamilyTree/images/" + filename;
            }
            else
            {
                img_showimg.ImageUrl = "~/FamilyTree/images/defaultimage.jpg";
            }
            SqlConnection con = new SqlConnection(strcon);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("hh:mm:ss");
            string creradod = "";
            string creradob = "";
            string dob = "";
            string fnamemember = "";
            string Lnamemember = "";
            cmd = new SqlCommand("SPPictures", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = txtRegdId.Text;
            cmd.Parameters.AddWithValue("id", "");
            cmd.Parameters.AddWithValue("Familyid", familyid);
            cmd.Parameters.AddWithValue("Memberid", memberidbysub);
            cmd.Parameters.AddWithValue("Pictureid", "");
            cmd.Parameters.AddWithValue("Photo", img_showimg.ImageUrl);
            cmd.Parameters.AddWithValue("Descr", "");
            cmd.Parameters.AddWithValue("Datetaken", date);
            cmd.Parameters.AddWithValue("Name", "");
            cmd.Parameters.AddWithValue("Enterby", "Admin");
            cmd.Parameters.AddWithValue("Enterdt", date);
            if (btn_Addpics.Text == "Add")
            {
                cmd.Parameters.AddWithValue("StatementType", "Insert");
            }
            con.Open();
            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
               // Response.Write("<script>alert('Data Saved Successfully.')</script>");
                Session["Slno"] = null;
                img_showimg.ImageUrl = "~/FamilyTree/images/defaultimage.jpg";
                //   Response.Redirect("../ERPApp/ViewAdmission.aspx");
                //Response.Redirect("~/FamilyTree/MyTree1.aspx");
            }
            con.Close();
            LoadGridview();
        }
        protected void LoadGridview()
        {
            string fmlyid = familyid;
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_Pictures where Familyid='"+ fmlyid + "' order by id desc", con);
            con.Open();
            adapt.Fill(dt);
            Grd_Bindpic.DataSource = dt;
            Grd_Bindpic.DataBind();
            con.Close();
        }

        protected void Grd_Bindpic_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Grd_Bindpic.EditIndex = e.NewEditIndex;
            LoadGridview();
        }

        protected void Grd_Bindpic_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            TextBox desc = Grd_Bindpic.Rows[e.RowIndex].FindControl("txt_desc") as TextBox;
            TextBox dot = Grd_Bindpic.Rows[e.RowIndex].FindControl("txt_dot") as TextBox;
            Label picid = Grd_Bindpic.Rows[e.RowIndex].FindControl("lbl_ID") as Label;

            //updating the record  
            SqlCommand cmd = new SqlCommand("Update Tbl_Pictures set Descr='" + desc.Text + "',Datetaken='" + dot.Text + "' where id=" + Convert.ToInt32(picid.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            Grd_Bindpic.EditIndex = -1;
            LoadGridview();
        }

        protected void Grd_Bindpic_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Grd_Bindpic.EditIndex = -1;
            LoadGridview();
        }

        protected void Grd_Bindpic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblt = (e.Row.FindControl("lbl_ID") as Label);
                //Label picid = Grd_Bindpic.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
                GridView gvOrders = e.Row.FindControl("grd_names") as GridView;
                DataTable dt = new DataTable();
                SqlDataAdapter adapt = new SqlDataAdapter("select * from Tbl_PicNames where Pictureid='" + lblt.Text + "' order by id desc", con);
                con.Open();
                adapt.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
                con.Close();
            }
        }

        protected void Grd_Bindpic_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            Label picid = Grd_Bindpic.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("delete from Tbl_Pictures where id='" + Convert.ToInt32(picid.Text) + "'", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("delete from Tbl_PicNames where Pictureid='" + Convert.ToInt32(picid.Text) + "'", con);
                cmd1.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                LoadGridview();
            }
            catch
            {

            }
        }

        protected void btn_Addname_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (ID != "")
            {
                showaddnames.Visible = true;
                string pictureid = ID;
                string names = txt_Nmaes.Text.Trim();
                string admin = "Admin";
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                SqlCommand cmd = new SqlCommand("insert into Tbl_PicNames values('" + familyid + "','" + names + "','" + pictureid + "','" + admin + "','" + date + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                txt_Nmaes.Text = "";
                LoadGridview(); 
                txt_Nmaes.Focus();
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }
        public void getpic(string id)
        {
            if (id != "")
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                string str = "";
                str = "select * from Tbl_Pictures where id=" + id;
                SqlCommand sqlCmd = new SqlCommand(str, con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);


                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    img_showimage.ImageUrl = dt.Rows[0]["Photo"].ToString();
                }
            }
        }
        protected void btn_close_Click(object sender, EventArgs e)
        {
            showaddnames.Visible = false;
            Response.Redirect("Addpictures.aspx");

        }

        protected void grd_names_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label picid = Grd_Bindpic.Rows[e.RowIndex].FindControl("lbl_pictureid3") as Label;
            SqlConnection con = new SqlConnection(strcon);


            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("delete from Tbl_PicNames where id='" + Convert.ToInt32(picid.Text) + "'", con);

                cmd.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                LoadGridview();
            }
            catch
            {

            }
        }
        public void deletepic(string delid)
        {
            SqlConnection con = new SqlConnection(strcon);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("delete from Tbl_PicNames where id='" + delid + "'", con);

                cmd.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                LoadGridview();
            }
            catch
            {

            }
        }

    }
}