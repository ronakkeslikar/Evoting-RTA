using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    
    public class FJC_Feedback
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contact_no { get; set; }
        public string feedback { get; set; }

        [Required(ErrorMessage = "Enter captcha response")]
        public string captcha { get; set; }
    }
}
