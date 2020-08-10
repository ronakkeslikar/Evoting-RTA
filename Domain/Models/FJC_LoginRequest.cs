using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class FJC_LoginRequest
    {
        public string system_ip { get; set; }
        public string UserID { get; set; }
        public string encrypt_Password { get; set; }
    }
}
