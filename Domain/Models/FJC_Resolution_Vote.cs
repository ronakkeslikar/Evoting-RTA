
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
        public class FJC_Resolutions_Vote
        {
            public int  resolution_id { get; set; }
            public int in_favour { get; set; }
            public int not_in_favour { get; set; }
            public int abstain { get; set; }            
        }     

}