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

        [Required(ErrorMessage ="Enter Voting Start Date")] 
        public string  voting_start_datetime { get; set;}
        [Required(ErrorMessage ="Enter Voting End Date")] 
        public string  voting_end_datetime { get; set;}
        [Required(ErrorMessage ="Enter Metting Datetime")] 
        public string  meeting_datetime { get; set;}
        [Required(ErrorMessage ="Enter Ladst Date Notice")] 
        public string last_date_notice { get; set;}
        [Required(ErrorMessage ="Enter Voting Result Date")] 
        public string voting_result_date { get; set;}
        [Required(ErrorMessage ="Please Upload Logo")] 
        public int upload_logo { get; set;} //this will change to file
        [Required(ErrorMessage ="Please Upload Resolution File")] 
        public int upload_resolution_file  { get; set; } //this will change to file
                [Required(ErrorMessage ="Please Upload Notice")] 
        public int  upload_notice { get; set; } //this will change to file
                [Required(ErrorMessage ="Enter No of Resolution")] 
        public int  enter_nof_resolution{ get; set;}

}
}