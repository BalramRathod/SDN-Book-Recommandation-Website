using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.BookModel
{
    public class BookWithAverageRating
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        /* public string CoverImageURL { get; set; }*/
        public double AverageRating { get; set; }
    }
}
