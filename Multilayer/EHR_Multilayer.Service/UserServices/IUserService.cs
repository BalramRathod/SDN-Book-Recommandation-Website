using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.ViewModel.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Service.UserServices
{
    public interface IUserService
    {
        public JsonModel AddUser(UserRegisterDto userDto);
        public JsonModel UserLogin(UserLoginDto userLoginDto);
        public JsonModel GetUserById(int? id);
        public JsonModel UserForgotPassword(string email);
        JsonModel Verify2FAOtp1(Login2FAVerifyOtpDto loginVerifyDto);
  

















    }
}
