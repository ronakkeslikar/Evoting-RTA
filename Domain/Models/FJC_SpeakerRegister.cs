using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class FJC_SpeakerRegister
    {
        [Required(ErrorMessage = "Enter event ID")]
        public int event_id { get; set; }

        [Required(ErrorMessage = "Enter email address")]
        public string email { get; set; }       

    }
}
