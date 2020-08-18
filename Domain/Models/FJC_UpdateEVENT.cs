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
[Required(ErrorMessage ="Enter ISIN ID"),RegularExpression(@"^[a-zA-Z0-9]*$")] 
public String ISIN { get; set;}
[Required(ErrorMessage ="Enter ISIN Type")] 
public string TYPE_ISIN { get; set;}
[Required(ErrorMessage ="Enter Evoing Type")] 
public string TYPE_EVOTING { get; set;}
[Required(ErrorMessage ="Enter Total No Of Share")] 
public string TOTAL_NOF_SHARE { get; set;}
[Required(ErrorMessage ="Enter Voting Rights")] 
public string VOTING_RIGHTS { get; set;}
[Required(ErrorMessage ="Enter Cut Of Date")] 
public DateTime CUT_OF_DATE { get; set;}
// [Required(ErrorMessage ="Enter Scrutinizer")] 
// public string SELECT_SCRUTINIZER { get; set;}
[Required(ErrorMessage ="Enter Voting Start Date")] 
public DateTime VOTING_START_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Voting End Date")] 
public DateTime VOTING_END_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Metting Datetime")] 
public DateTime MEETING_DATETIME { get; set;}
[Required(ErrorMessage ="Enter Ladst Date Notice")] 
public DateTime LAST_DATE_NOTICE { get; set;}
[Required(ErrorMessage ="Enter Voting Result Date")] 
public DateTime VOTING_RESULT_DATE { get; set;}
[Required(ErrorMessage ="Please Upload Logo")] 
public string UPLOAD_LOGO { get; set;}
[Required(ErrorMessage ="Please Upload Resolution File")] 
public string UPLOAD_RESOLUTION_FILE { get; set;}
[Required(ErrorMessage ="Please Upload Notice")] 
public string UPLOAD_NOTICE { get; set;}
[Required(ErrorMessage ="Enter No of Resolution")] 
public string ENTER_NOF_RESOLUTION { get; set;}
[Required(ErrorMessage ="Please Enter Title")] 
public string TITAL { get; set;}
[Required(ErrorMessage ="Please Enter Description")] 
public string DESCRIPTION { get; set;}
[Required(ErrorMessage ="Please Upload File")] 
public string FILEUPLOAD { get; set;}
}
}