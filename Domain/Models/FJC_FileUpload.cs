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
        public int DOC_NO { get; set;}
         public IFormFile files{get;set;} 
        public string File_Name { get; set;}
        public string File_Path { get; set;}
        public int UploadedBy { get; set;}
        public string Token_No { get; set;}  
       
    }     
 
}