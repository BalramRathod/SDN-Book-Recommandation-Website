using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using MailKit.Net.Smtp;

namespace EHR_Multilayer.Service.UserServices.Email
{
    public class EmailService : IEmailService
    {
        public string SendEmail(string Password, string UserEmail, string username)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("balramrathod119@gmail.com"));
            email.To.Add(MailboxAddress.Parse(UserEmail));
            email.Subject = "One Time Password";
            // string string_id = id.ToString();
            /*  var Encrypt_id = EncryptDecrypt.Encrypt(string_id);*/
            // var Encrypt_id = string_id;


            var url = "http://localhost:4200/activeAccount?id="; //+ Encrypt_id;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "<h1>Registration Completed!</h1><br><h3>Welcome to Book Recommandation App</h3><br><p2>" +
                "Dear user you need to active your account to use</p2> <br>" + "" +
                 /*" <a style='color:blue' href=" + url + "> Please click here to activate your account </a>"+*/
                " <br>" + "<h3>Your Username is : " + username + " </h3> " + "<br>" + "<h3>Your Password is : " + Password + " </h3>" + ""
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("balramrathod119@gmail.com", "jijdnmhvumfgmmgr");
            smtp.Send(email);
            smtp.Disconnect(true);
            return "send";
        }


        // Forgot Password Email


    }
}
