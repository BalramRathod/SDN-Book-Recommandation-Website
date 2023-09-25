using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.ViewModel.UserModel
{
    public class UserRegisterDto
    {

      /*  public int UserId { get; set; }*/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; } = string.Empty;
 
    }
}
