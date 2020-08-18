using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;

namespace evoting.Domain.Models
{

public class FJC_EVENT_Resolution
   {
              public String EVENT_RESOLUTION_ID { get; set;}
              public string ROW_NO { get; set;}
              public string EVENT_NO { get; set;}
              public string TITLE { get; set;}
              public string DESCRIPTION { get; set;}
              public string FILE_PATH { get; set;}
     }

}