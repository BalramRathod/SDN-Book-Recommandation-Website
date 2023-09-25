using AutoMapper;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using EHR_Multilayer.ViewModel.ReviewModel;
using EHR_Multilayer.ViewModel.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Users, UserRegisterDto>();
            CreateMap<Users, UserRegisterDto>().ReverseMap();

            CreateMap<Users, UserLoginDto>();
            CreateMap<Users, UserLoginDto>().ReverseMap();

            CreateMap<Users, UserChangePWDDto>();
            CreateMap<Users, UserChangePWDDto>().ReverseMap();

            CreateMap<LoginOTP, Login2FAVerifyOtpDto>();
            CreateMap<LoginOTP, Login2FAVerifyOtpDto>().ReverseMap();


            CreateMap<Book, BookDto>();
            CreateMap<Book, BookDto>().ReverseMap();

            CreateMap<Book, BookSearchDto>();
            CreateMap<Book, BookSearchDto>().ReverseMap();

            CreateMap<Review, ReviewDto>();
            CreateMap<Review, ReviewDto>().ReverseMap();









        }

    }
}
