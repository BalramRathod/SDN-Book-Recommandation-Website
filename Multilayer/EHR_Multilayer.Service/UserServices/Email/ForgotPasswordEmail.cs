using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Service.UserServices.Email
{
    public class ForgotPasswordEmail
    {

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ForgotPasswordEmail(string email, string username, string password)
        {



            Email = email;
            Username = username;
            Password = password;
        }
        public void SendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("balramrathod119@gmail.com");
                mail.To.Add(Email)
;
                mail.Subject = "Reset Password Details";
                mail.Body = "<h2>Hey!</h2><h4>" + Username + "</h4><p>Your password is reset successfully !<br>Following is your new password: </p><h4>" + Password + "</h4>";
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("balramrathod119@gmail.com", "jijdnmhvumfgmmgr");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
