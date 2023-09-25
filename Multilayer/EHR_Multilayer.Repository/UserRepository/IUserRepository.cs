using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.ViewModel.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.UserRepository
{
    public interface IUserRepository
    {
         Users UserEmail(Users userEmail);
         Users Save(Users user);
         Users UserLogin(Users userLoginDto);
         Users ForgotPassword(string email);
        void Changeassword(UserChangePWDDto changePWDDto);
        Users GetUserById(int id);

        // during login
        public void TwoFAOtp(LoginOTP loginOtp);
 
        LoginOTP Verify2FA(LoginOTP loginAuth);
  




    }
}
