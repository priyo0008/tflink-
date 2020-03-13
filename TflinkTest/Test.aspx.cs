using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                func();
            }
        }
        public void func()
        {
            int[] array1 = new int[] { 1, 3, 5, 7, 9 };
            int sum = 0;
            for(int j=0;j<array1.Length;j++)
            {
                for(int i=0;i<array1.Length;i++)
                {
                    if(array1[j] != array1[i])
                    {
                        sum += Convert.ToInt32(array1[i]);
                    }
                    
                }
                lbl_test.Text += sum.ToString()+"-";
                sum = 0;
            }
        }
    }
}