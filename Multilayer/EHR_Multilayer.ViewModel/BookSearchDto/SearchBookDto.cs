using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.BookSearchDto
{
    public class SearchBookDto
    {

        // creating Dto for getting all procedure data
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genres { get; set; }
        public string CoverImage { get; set; }

    }
}
