using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TflinkTest
{
    public partial class Array : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //arrayexam();
                getnmu();
            }
        }
        public void arrayexam()
        {
            int[] n = new int[10];
            int[] r = { 10, 20, 10, 20, 20, 10, 10, 30, 40 };
            int i, j;

            int count = 0;
            int value = 0;
            int value2 = 0;
            int finalnmb = 0;
            int hh = 0;
            string done = "";
            for (i = 0; i < r.Length; i++)
            {
                done = "";
                value = r[i];

                for (j = 0; j < r.Length; j++)
                {
                    value2 = r[j];
                    for (int h = 0; h < n.Length; h++)
                    {
                        string check = n[h].ToString();

                        if (value == n[h])
                        {
                            done = "Done";
                        }
                        else
                        {
                            done = "";
                        }

                    }
                    if (value == value2 && done != "Done")
                    {
                        count = count + 1;
                        for (int ll = 0; ll < n.Length; ll++)
                        {
                            if (n[ll] != value)
                            {
                                n[i] = value;
                            }
                        }
                        hh = n[i];

                    }
                    if (count % 2 == 0)
                    {

                        finalnmb += count / 2;
                        lbl_array.Text = "The count :" + finalnmb.ToString() + "________";
                    }
                    for (int lh = 0; lh < n.Length; lh++)
                    {
                        lbl_array.Text += n[lh].ToString() + ",";
                    }
                    //    }
                    //}


                }
                //for (j = 0; j < 10; j++)
                //{
                //    Console.WriteLine("Element[{0}] = {1}", j, n[j]);
                //}
            }
        }
        public void getnmu()
        {
            int[] myArray;
            
            int i, j, k, l;
            int count = 0;
            int value = 0;
            int value2 = 0;
            int finalnmb = 0;
            int superfinal = 0;
            int hh = 0;
            string done10 = "";
            string done20 = "";
            string done30 = "";
            string done40 = "";
            int[] r = { 10, 20, 10, 20, 20, 10, 10, 30, 40 };
            myArray = new int[r.Length];
            for (i = 0; i < r.Length; i++)
            {
                value = r[i];
                
                count = 0;


                for (j = 0; j < r.Length; j++)
                {
                    value2 = r[j];
                    if (value == value2)
                    {
                        count = count + 1;
                        if(count>1)
                        {
                            done10 = "Done";
                        }
                       
                    }
                }
                if(myArray.Length>0)
                {
                    for (k = 0; k < myArray.Length; k++)
                    {
                        if (value != myArray[k])
                        {
                            finalnmb += count / 2;
                        }

                    }
                }
                else
                {
                    finalnmb += count / 2;
                }
             
                

                superfinal = finalnmb; 
                if(done10 == "Done")
                {
                    myArray[i] = value;
                } 
                // sNumbers.Split(new[] { ',' }).ToList<int>();
            }
            //Big loop
            for(int gg=0;gg< myArray.Length;gg++)
            {
              string hhk =  myArray[gg].ToString();
                lbl_array.Text += superfinal.ToString()+"__"+ hhk;
            }
            

        }
    }
}