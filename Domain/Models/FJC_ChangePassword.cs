using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class FJC_ChangePassword
    {
        public string UserID { get; set; }
        public string encrypt_OldPassword { get; set; }
        public string encrypt_NewPassword { get; set; }
       
    }
}
