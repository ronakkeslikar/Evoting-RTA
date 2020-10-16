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
    public class FJC_VideoConf
    {
        public int event_id { get; set;}         
        public string vc_url { get; set; }
        public string investor_url { get; set; }
        public string vc_title { get; set; }
        public string vc_datetime { get; set; }
        public string vc_file { get; set; }
        public string vc_handler { get; set; }


    }
}