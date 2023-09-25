using Dapper;
using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Service.ImageService;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.UserModel.ImageProfileModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHR_Multilayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadsController : ControllerBase
    {
        private readonly DapperContext _dapperContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageUploadsController(DapperContext dapperContext, IWebHostEnvironment webHostEnvironment)
        {
            _dapperContext = dapperContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("uploadProfile")]
        public async Task<ActionResult> UploadImage([FromForm] UploadImageProfileDto profile)
        {
            try
            {
                var query = "";
                var connection = _dapperContext.CreateConnection();
                Upload obj = new Upload(_webHostEnvironment);

                if (profile.ImageFile != null)
                {
                    query = "Select * from Users where UserId = @UserId";
                    var user = await connection.QueryFirstOrDefaultAsync<Users>(query, new { UserId = profile.UserId });

                    var fileResult = obj.SaveImage(user.UserId, profile.ImageName, profile.ImageFile);



                    query = "Update Users set ProfileImage = @path where UserId = @UserId";

                    await connection.ExecuteAsync(query, new { path = fileResult, UserId = profile.UserId });

                    return Ok(new
                    {
                        message = "Success"
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(new
            {
                message = "Failed"
            });

        }



        [HttpPost("UploadBookprofile")]
        public async Task<ActionResult> UploadProfile([FromForm] BookImageDto image)
        {
            try
            {
                var connection = _dapperContext.CreateConnection();
                var query = "";

                if (image.BookImageFile != null)
                {
                    var contentPath = this._webHostEnvironment.WebRootPath;
                    var path = Path.Combine(contentPath, "BookImage");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var ext = Path.GetExtension(image.BookImageFile.FileName);

                    var newFileName = image.Id + ext;
                    var fileWithPath = Path.Combine(path, newFileName);
                    var stream = new FileStream(fileWithPath, FileMode.Create);
                    image.BookImageFile.CopyTo(stream);
                    stream.Close();

                    var newPath = "https://localhost:7195/" + "/BookImage/" + newFileName;

                    query = "Update Books set CoverImage = @path where Id = @Id";

                    await connection.ExecuteAsync(query, new { path = newPath, id = image.Id });

                    return Ok(new
                    {
                        message = "Success"
                    });

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(new
            {
                message = "Failed"
            });
        }

    }
}
