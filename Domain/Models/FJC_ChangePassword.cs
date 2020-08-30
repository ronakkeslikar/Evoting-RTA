using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace evoting.Domain.Models
{
    public class FJC_ChangePassword
    {
        [Required(ErrorMessage ="Enter User ID"),RegularExpression(@"^[a-zA-Z0-9]*$")]  
        public string UserID { get; set; }
         [Required(ErrorMessage ="Enter Old Password")]
         [MinLength(6, ErrorMessage = "Password Cannot be less than 6 digit")]  
         [MaxLength(20, ErrorMessage = "Password Cannot be more than 20 digit")]
        public string encrypt_OldPassword { get; set; }
          [Required(ErrorMessage ="Enter New Password")]
          [MinLength(6, ErrorMessage = "Password Cannot be less than 6 digit")]  
         [MaxLength(20, ErrorMessage = "Password Cannot be more than 20 digit")]
        public string encrypt_NewPassword { get; set; }
       
    }
}
