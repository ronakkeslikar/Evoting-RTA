
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
        public class FJC_Resolutions_Data
        {
            public int  resolution_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int doc_id { get; set; } 
        }     

}