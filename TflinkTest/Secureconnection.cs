using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TflinkTest
{
    public class Secureconnection
    {
        public SqlConnection GetEMployeeconnnect()
        {
            SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog=TheFlink; User ID=sa; Password='sais123';");
           // SqlConnection conn = new SqlConnection("Data Source=184.168.194.62; Initial Catalog=TheFlink; User ID=Theflink; Password='Theflink123';");
          // SqlConnection conn = new SqlConnection("Data Source=184.168.194.62; Initial Catalog=tflinksaisdemo; User ID=tflinksaisdemo; Password='tflink123';"); 
            //SqlConnection conn = new SqlConnection("Data Source=216.117.130.246,4754; Initial Catalog=DB_NISSAN; User ID=sa; Password='weE2@aNaT%17';");
            return conn;
        }
    }
}