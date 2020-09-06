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
public class FJC_UpdateEVENT
 {
        [Required(ErrorMessage = "Event-ID is required")]
        public int event_id { get; set;}
         
        public string  voting_start_datetime { get; set;}
        
        public string  voting_end_datetime { get; set;}
         
        public string  meeting_datetime { get; set;}
         
        public string last_date_notice { get; set;}
         
        public string voting_result_date { get; set;}
        
        public int upload_logo { get; set;} 
         
        public int upload_resolution_file  { get; set; } 
                 
        public int  upload_notice { get; set; } 
                 
        public int  enter_nof_resolution{ get; set;}

        public FJC_Resolutions_Data[] resolutions_Datas { get; set; }

    }
}