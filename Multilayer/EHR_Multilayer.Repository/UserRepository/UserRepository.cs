using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.ViewModel.UserModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly SampleContext _context;

        public UserRepository(SampleContext context)
        {
            _context = context;
        }


        // while login
        public void TwoFAOtp(LoginOTP loginOtp)
        {
            _context.LoginOTP.Add(loginOtp);
            _context.SaveChanges();
        }

        public Users ForgotPassword(string email)
        {
            var user =  _context.Users.FirstOrDefault(u => u.Email == email);
             _context.SaveChanges();
            return user;
            

        }

        public Users GetUserById(int id)
        {
            //var data = _context.Users.FirstOrDefault(a=>a.UserId == id);
            //Users data1 = _context.Users.Where(a=> a.UserId == id).FirstOrDefault();
            Users data = _context.Users.Find(id);
            //return _context.Users.Find(id);
            return data;
        }

        public  Users Save(Users user)
        {
             _context.Users.Add(user);
              _context.SaveChanges();

            return user;
          
        }

        public Users UserLogin(Users userLoginDto)
        {
            var UserLoginDetails = _context.Users.FirstOrDefault(x => (x.Username == userLoginDto.Username && x.Password == userLoginDto.Password));
            return UserLoginDetails;

        }


        /*     public bool Verify2FA(Login2FAVerifyOtpDto loginAuth)
             {
                 var flag = _context.LoginOTP.FirstOrDefault(O => (O.UserId == loginAuth.UserId && O.Otp == loginAuth.Otp));
                 return flag != null;
             }
     */
        public LoginOTP Verify2FA(LoginOTP loginAuth)
        {
            var Verified = _context.LoginOTP.FirstOrDefault(O => (O.UserId == loginAuth.UserId && O.Otp == loginAuth.Otp));
            return Verified;
        }



  

        public void Changeassword(UserChangePWDDto user)
        {
            _context.Add(user);
            _context.SaveChanges();

        }


        public Users UserEmail(Users userEmail)
        {
          //  _context.Users.Where(x => x.Email == userEmail.Email).ToArrayAsync();
           var email= _context.Users.Where(x => x.Email == userEmail.Email).FirstOrDefault();
            return userEmail;
        }
    }
}
