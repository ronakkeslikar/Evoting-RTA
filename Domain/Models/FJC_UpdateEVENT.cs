using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;

namespace evoting.Domain.Models
{
public class FJC_UpdateEVENT {

 public int EVENT_DETAIL_ID { get; set;}

 public int EVENT_ID { get; set;}

[Required(ErrorMessage ="Enter Voting Start Date")] 
public string VOTING_START_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Voting End Date")] 
public string VOTING_END_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Metting Datetime")] 
public string MEETING_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Ladst Date Notice")] 
public string LAST_DATE_NOTICE { get; set;}
[Required(ErrorMessage ="Enter Voting Result Date")] 
public string VOTING_RESULT_DATE { get; set;}
[Required(ErrorMessage ="Please Upload Logo")] 
public string UPLOAD_LOGO { get; set;}
[Required(ErrorMessage ="Please Upload Resolution File")] 
public string UPLOAD_RESOLUTION_FILE { get; set;}
[Required(ErrorMessage ="Please Upload Notice")] 
public string UPLOAD_NOTICE { get; set;}
[Required(ErrorMessage ="Enter No of Resolution")] 
public string ENTER_NOF_RESOLUTION { get; set;}

}
}