using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Http;

namespace evoting.Domain.Models
{

    public class FJC_SharedHolder_Restrict    
    { 
     public int event_id {get;set;}      
     public string  dpcl {get;set;}
        public string pan { get; set; }
        public string remark{get;set;}   

    } 
    public class FJC_SharedHolder_Derestrict    
    { 
     public int event_id {get;set;}      
     public string  dpcl {get;set;}
    }       
 
}