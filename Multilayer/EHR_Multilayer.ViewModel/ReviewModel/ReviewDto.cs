using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.ReviewModel
{
    public class ReviewDto
    {

        public int User_Id { get; set; }
        public int Book_Id { get; set; }
        public string review { get; set; } = "";
        public int Rating { get; set; } = 0;
        public string UserName { get; set; }    
    }
}
