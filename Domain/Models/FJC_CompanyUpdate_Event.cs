using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;

namespace evoting.Domain.Models
{
public class FJC_CompanyUpdate_Event
 {
        public int event_id { get; set; }

        [Required(ErrorMessage = "Enter ISIN ID"), RegularExpression(@"^[a-zA-Z0-9]*$")]
        public String isin { get; set; }

        [Required(ErrorMessage = "Enter ISIN Type")]
        public int type_isin { get; set; }

        [Required(ErrorMessage = "Enter Evoing Type")]
        public int type_evoting { get; set; }

        [Required(ErrorMessage = "Enter Total No Of Share")]
        public decimal total_nof_share { get; set; }

        [Required(ErrorMessage = "Enter Voting Rights")]
        public int voting_rights { get; set; }

        [Required(ErrorMessage = "Enter Cut Of Date")]
        public string cut_of_date { get; set; }

        [Required(ErrorMessage = "Select Scrutinizer")]
        public int scrutinizer { get; set; }

        public string voting_start_datetime { get; set; }

        public string voting_end_datetime { get; set; }

        public string meeting_datetime { get; set; }

        public string last_date_notice { get; set; }

        public string voting_result_date { get; set; }

        public int upload_logo { get; set; } 

        public int upload_resolution_file { get; set; } 

        public int upload_notice { get; set; } 

        public int enter_nof_resolution { get; set; }

        public FJC_Resolutions_Data[] resolutions_Datas { get; set; }

    }
 
}