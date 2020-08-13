using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace evoting.Domain.Models
{
    public class FJC_ChangePassword
    {
        [Required(ErroreMessage ="Enter User ID"),RegularExpression(@"^[a-zA-Z0-9]*$")]  
        public string UserID { get; set; }
         [Required(ErroreMessage ="Enter Old Password")]
        public string encrypt_OldPassword { get; set; }
          [Required(ErroreMessage ="Enter New Password")]
        public string encrypt_NewPassword { get; set; }
       
    }
}
