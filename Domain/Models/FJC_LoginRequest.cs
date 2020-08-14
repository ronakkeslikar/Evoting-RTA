using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace evoting.Domain.Models
{
    public class FJC_LoginRequest
    {
        [Required(ErrorMessage ="Enter User ID"),RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string UserID { get; set; }

        [Required(ErrorMessage ="Enter IP Address")]
        public string system_ip { get; set; }

        [Required(ErrorMessage ="Enter Password")]        
        public string encrypt_Password { get; set; }
    }
}
