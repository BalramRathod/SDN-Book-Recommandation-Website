using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; } = "";
        public string Genres { get; set; }  
        public DateTime publishYear { get; set; }


    }
}
