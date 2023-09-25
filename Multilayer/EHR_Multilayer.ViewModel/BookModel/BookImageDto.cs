using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.BookModel
{
    public class BookImageDto
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public IFormFile BookImageFile { get; set; }
    }
}
