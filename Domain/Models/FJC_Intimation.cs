using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace evoting.Domain.Models
{
    public class FJC_Intimation
    {
        public int event_id { get; set;}         
        public string event_name { get; set; }
        public string notice_date { get; set; }
        public string rom_file { get; set; }
        public string email_sent_date { get; set; }
        public string post_sent_date { get; set; }
    }
}