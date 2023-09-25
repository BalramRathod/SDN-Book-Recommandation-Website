using AutoMapper;
using Dapper;
using EHR_Multilayer.Domain;
using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.HelperModel;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.Domain.StatuMessageModel;
using EHR_Multilayer.Repository.UserRepository;
using EHR_Multilayer.Service.UserServices.Email;
using EHR_Multilayer.Service.UserServices.Twilio;
using EHR_Multilayer.ViewModel.UserModel;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio.Jwt.AccessToken;
using static EHR_Multilayer.Domain.StatuMessageModel.StatusMessage;
using static System.Net.WebRequestMethods;

namespace EHR_Multilayer.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly SampleContext _context;
        private readonly DapperContext _dapperContext;

        public UserService(IUserRepository repository, IMapper mapper, IEmailService emailService, SampleContext sampleContext, DapperContext dapperContext)
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
            _context = sampleContext;
            _dapperContext = dapperContext;
        }


        public JsonModel AddUser(UserRegisterDto userDto)
        {
            JsonModel response = new JsonModel();
       

            if (userDto.UserType == 1)
            {
                // Admin  Information here

                // Generate username
                var d = userDto.DateOfBirth.Value.ToString("MMddyy");
                var Username = $"AN_{userDto.FirstName.ToUpper()}{userDto.FirstName.Substring(0, 1).ToUpper()}{d}";

                // Generate Password
                var random = new Random();
                string str = "abcdefghijklmnopqrstuvwxyz";
                var rand_string = "";
                for (int i = 0; i < 8; i++)
                {
                    random.Next(0, 26);


                    rand_string = rand_string + str[random.Next(0, 26)];
                }

                Users user = new Users();
               // user.FirstName = EncryptionHelper.Encrypt( userDto.FirstName);
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.Username = Username;
                user.Password = rand_string;
                user.UserType = userDto.UserType;
                user.DateOfBirth = userDto.DateOfBirth;
                user.Address = userDto.Address;
          

                //Mapping UserDto<Destination Data> Data into User <Resource Data>
                Users userPatient = _mapper.Map<Users>(user);

                var entity = _repository.Save(userPatient);
                if (entity != null)
                {
                    response = new JsonModel(entity, null, StatusMessage.Success, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "This is app error");
                }

                _emailService.SendEmail(rand_string, userPatient.Email, userPatient.Username);

            }
            if (userDto.UserType == 2)
            {
                // Customer  Information here

                // Generate username
                var d = userDto.DateOfBirth.Value.ToString("MMddyy");
                var Username = $"CR_{userDto.FirstName.ToUpper()}{userDto.FirstName.Substring(0, 1).ToUpper()}{d}";

                // Generate Password
                var random = new Random();
                string str = "abcdefghijklmnopqrstuvwxyz";
                var rand_string = "";
                for (int i = 0; i < 8; i++)
                {

                    //random.Next(0, 26) -> return type is int random.Next(3) it takes   four 4 character like this abcd
                    rand_string = rand_string + str[random.Next(0, 26)];
                }

                Users user = new Users();
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.DateOfBirth = userDto.DateOfBirth;
                user.Username = Username;
                user.Password = rand_string;
                user.UserType = userDto.UserType;
                user.Address = userDto.Address;
       
                Users userProvider = _mapper.Map<Users>(user);
                var entity = _repository.Save(userProvider);
                if (entity != null)
                {
                    response = new JsonModel(entity, null, StatusMessage.Success, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "This is app error");
                }
                _emailService.SendEmail(rand_string, userProvider.Email, userProvider.Username);

            }
            //

            return response;
        }

        public JsonModel GetUserById(int? id)
        {
            JsonModel response = new JsonModel();

            if (id != 0)
            {
                var entity = _repository.GetUserById((int)id);


                if (entity != null)
                {

                    //                    EncryptionHelper.Decrypt(entity);
                    response = new JsonModel(entity, null, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
                }
            }

            return response;
        }

        public JsonModel UserForgotPassword(string email)
        {
            JsonModel response = new JsonModel();

            var UserEntity = _repository.ForgotPassword(email);

            if (UserEntity != null)
            {
                ForgotPasswordEmail mail = new ForgotPasswordEmail(email, UserEntity.Username, UserEntity.Password);
                mail.SendMail();
                response = new JsonModel(UserEntity, null, StatusMessage.ChangeMessage, (int)HttpStatusCodes.OK);
            }
            else
            {
                response = new JsonModel(new object(), null, StatusMessage.EmailNotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
            }


            return response;
        }

        public JsonModel UserLogin(UserLoginDto userLoginDto)
        {
            JsonModel response = new JsonModel();

            Users UserLogin = _mapper.Map<Users>(userLoginDto);

            var UserLoginDetails = _repository.UserLogin(UserLogin);
            if (UserLoginDetails == null)
            {
                response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "This is app error");
                return response;
            }



            // if already otp is already then delete old otp and create new otp
            var UserID = _repository.GetUserById(UserLoginDetails.UserId);
            var conn = _dapperContext.CreateConnection();
            var Query = "delete from LoginOTP where UserId = @UserId";
            conn.Open();
            conn.Execute(Query, new { UserID.UserId });
            conn.Close();

            LoginOTP loginotp = new LoginOTP();
            loginotp.UserId = UserLoginDetails.UserId;
            loginotp.Otp = CreateRandomOtp();
            LoginOTP dbotp = loginotp.Adapt<LoginOTP>();
            _repository.TwoFAOtp(dbotp);

            // here calling Twilio functionality
            TwilioService twilio = new TwilioService();
             twilio.SendOtpOnRegisterdMoileNo(loginotp.Otp);
            var token = CreateJwtToken(UserLoginDetails);

            if (UserLoginDetails != null)
            {

                response = new JsonModel(UserLoginDetails, token, StatusMessage.LoginSuccess, (int)HttpStatusCodes.OK);
            }
            else
            {
                response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "This is app error");
            }



            return response;
        }


        // 2FA Verify Method 
        public JsonModel Verify2FAOtp1(Login2FAVerifyOtpDto loginVerifyDto)
        {
            JsonModel response = new JsonModel();

            LoginOTP UserLogin = _mapper.Map<LoginOTP>(loginVerifyDto);
            LoginOTP result = _repository.Verify2FA(UserLogin);
            if (result != null)
            {
                response = new JsonModel(result, null, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
            }
            else
            {
                response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
            }


            return response;
        }

       

       

        //here generating random otp for 2FA
        private int CreateRandomOtp()
        {
            int _min = 100000;
            int _max = 999990;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }


        private string CreateJwtToken(Users userObj)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {


                new Claim(ClaimTypes.Name,$"{userObj.FirstName}{userObj.LastName}")


            });

            var credencial = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencial
            };

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            return JwtTokenHandler.WriteToken(token);
        }

      
    }
}
