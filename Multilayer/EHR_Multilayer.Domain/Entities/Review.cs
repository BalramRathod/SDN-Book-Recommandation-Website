using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey("Users")]
        public int User_Id { get; set; }
        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        public string review { get; set; } = "";
        public int Rating { get; set; } = 0;
        public string UserName { get; set; }

    }
}
