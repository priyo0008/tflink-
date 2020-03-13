using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TflinkTest
{
    public class Emailsend
    {
        public void sendemail()
        {

            string to = "toaddress@gmail.com"; //To address    
            string from = "fromaddress@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "This is message to send your body";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            //message.BodyEncoding =/* Encoding.UTF8*/"";
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("yourmail id", "Password");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}