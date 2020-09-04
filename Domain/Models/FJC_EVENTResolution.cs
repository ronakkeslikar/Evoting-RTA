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

       public class FJC_EVENT_Resolution
       {
                  public string EVENT_NO { get; set; }                        
              
                  public FJC_Resolutions_Data[] Resolutions_Datas { get; set; }
       }

        public class FJC_Resolutions_Data
        {
            public int EVENT_RESOLUTION_ID { get; set; }
            public string TITLE { get; set; }
            public string DESCRIPTION { get; set; }
            public IFormFile FILE_PATH { get; set; } 
        }

}