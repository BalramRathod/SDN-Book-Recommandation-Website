using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Service.UserServices.Email
{
    public interface IEmailService
    {
        string SendEmail(string Password, string UserEmail,string username);
      
    }
}
