using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.BookModel
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string Genres { get; set; }
        public DateTime publishYear { get; set; }










        /*        public string Review { get; set; } = "";
                public int Rating { get; set; } = 0;*/
    }
}
