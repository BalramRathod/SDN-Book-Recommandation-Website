using EHR_Multilayer.Domain.AutoMapper;
using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Repository.BookRepository;
using EHR_Multilayer.Repository.ReviewRepository;
using EHR_Multilayer.Repository.UserRepository;
using EHR_Multilayer.Service;
using EHR_Multilayer.Service.BookServices;
using EHR_Multilayer.Service.ReviewServices;
using EHR_Multilayer.Service.UserServices;
using EHR_Multilayer.Service.UserServices.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace EHR_Multilayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


           

            // here injecting automapper...
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Add services to the container.
            builder.Services.AddDbContext<SampleContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));
            builder.Services.AddSingleton<DapperContext>();

            // Injecting Repositories here..
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
     


            // Injecting Services here..
            builder.Services.AddTransient<IUserService,UserService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IReviewService, EHR_Multilayer.Service.ReviewServices.ReviewService>();



            // jwt configrutatiion open
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });
            // jwt configrutatiion close
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();


            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
       
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}