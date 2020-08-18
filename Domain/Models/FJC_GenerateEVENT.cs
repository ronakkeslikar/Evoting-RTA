using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;

namespace evoting.Domain.Models
{
public class FJC_GenerateEVENT
 {   

    public int EVENT_ID { get; set;}

    [Required(ErrorMessage ="Enter Row ID Client")]
    public int ROWID_CLIENT { get; set;}

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
   
    [Required(ErrorMessage ="Select Scrutinizer")] 
    public string SCRUTINIZER { get; set;}

    public string CREATED_BY { get; set;}

    public string UPDATED_BY { get; set;}
 }
}