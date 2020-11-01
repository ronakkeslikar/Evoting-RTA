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
         public string upload_type{get;set;}
       
    }  
    public class FJC_ROMUpload 
    { 
         public int doc_id {get;set;} //required
       
        public int event_id {get;set;}  //required

        public string upload_type{get;set;}//For ROM Intimation only
      
       
    }  
    public class FJC_DOC_Upload 
    { 
         public int doc_id {get;set;} //required    
        public string upload_type { get; set; }

    }   
 
}