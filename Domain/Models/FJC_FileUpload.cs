using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Http;

namespace evoting.Domain.Models
{

public class FJC_FileUpload 
    { 
         public IFormFile files{get;set;} 
        public string Token_ID { get; set;} 
        public int Event_No {get;set;} 
        public string Process_Type {get;set;} 
       
    }     
 
}