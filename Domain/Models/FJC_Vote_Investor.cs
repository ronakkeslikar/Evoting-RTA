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
public class FJC_Vote_Investor
 {
        public int event_id { get; set;}
         
        public int  submitted { get; set;}

        public FJC_Resolutions_Vote[] resolutions { get; set; }

    }
}