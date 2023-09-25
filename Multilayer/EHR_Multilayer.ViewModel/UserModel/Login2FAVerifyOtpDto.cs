using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.UserModel
{
    public class Login2FAVerifyOtpDto
    {
        public int UserId { get; set; }
        public int Otp { get; set; }
    }
}
