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
[Required(ErrorMessage ="Enter Event ID")]
    public int event_id { get; set;}   

    [Required(ErrorMessage ="Enter ISIN ID"),RegularExpression(@"^[a-zA-Z0-9]*$")]
    public String ISIN { get; set;}
    
    [Required(ErrorMessage ="Enter ISIN Type")]
    public int TYPE_ISIN { get; set;}
    
    [Required(ErrorMessage ="Enter Evoing Type")]
    public int TYPE_EVOTING { get; set;}
    
    [Required(ErrorMessage ="Enter Total No Of Share")] 
    public decimal TOTAL_NOF_SHARE { get; set;}
    
    [Required(ErrorMessage ="Enter Voting Rights")] 
    public decimal VOTING_RIGHTS { get; set;}
    
    [Required(ErrorMessage ="Enter Cut Of Date")] 
    public DateTime CUT_OF_DATE { get; set;}
   
    [Required(ErrorMessage ="Select Scrutinizer")] 
    public int SCRUTINIZER { get; set;}

 }
 
}