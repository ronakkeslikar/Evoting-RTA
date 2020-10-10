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
public class FJC_AccountSearch
 {
        public int user_type  { get; set;}
         
        public string str { get; set;}

         

    }
}