using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Service.ImageService
{
    public class Upload
    {
        private readonly IWebHostEnvironment _environment;

        public Upload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string SaveImage(int id, string name, IFormFile imageFile)
        {
            try
            {
                var contentPath = this._environment.WebRootPath;
                var path = Path.Combine(contentPath, "Profile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var ext = Path.GetExtension(imageFile.FileName);
                var newFileName = id + name + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
     
                return GetFilePath(newFileName);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Failed";
            }
        }


        private string GetFilePath(string name)
        {
            return "https://localhost:7195/" + "/Profile/" + name;
        }
    }
}
