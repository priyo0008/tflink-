using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if(txt_Userid.Text=="Admin" && txt_password.Text=="11111")
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
        }
    }
}