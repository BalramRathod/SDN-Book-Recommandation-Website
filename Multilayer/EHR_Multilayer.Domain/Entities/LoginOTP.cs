using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.Entities
{
    public class LoginOTP
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public int Otp { get; set; }
        public virtual Users Users { get; set; }
    }
}
