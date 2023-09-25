using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Service.UserServices;
using EHR_Multilayer.Service.UserServices.Email;
using EHR_Multilayer.ViewModel.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EHR_Multilayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly SampleContext _context;

        public UsersController(IUserService service, SampleContext sampleContext)
        {
            _service = service;
            _context = sampleContext;
        }


        [HttpPost("Register")]
        public IActionResult Insert([FromBody] UserRegisterDto userRegisterDto)
        {
            return Ok(_service.AddUser(userRegisterDto));
        }



        [HttpPost("Login")]
        public IActionResult UserLogin([FromBody] UserLoginDto userLoginDto)
        {
            return Ok(_service.UserLogin(userLoginDto));

        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_service.GetUserById(id));
        }


        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            return Ok(_service.UserForgotPassword(email));
        }

        [HttpPost("VerifyOtp")]
        public IActionResult VerifyOtp(Login2FAVerifyOtpDto loginAuthDataVM)
        {
            return Ok(_service.Verify2FAOtp1(loginAuthDataVM));
        }
     

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserChangePWDDto request)
        {
            /*return Ok(_service.UserChangePassword(request));*/
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);
            if (user == null)
            {
                return BadRequest("Invalid..");
            }

            user.Password = request.Password;


            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Password reset successfuly"
            });
        }

    }
}
