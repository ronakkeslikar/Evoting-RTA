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

    public int event_id { get; set;}   

    [Required(ErrorMessage ="Enter ISIN ID"),RegularExpression(@"^[a-zA-Z0-9]*$")]
    public String isin { get; set;}
    
    [Required(ErrorMessage ="Enter ISIN Type")]
    public int type_isin { get; set;}
    
    [Required(ErrorMessage ="Enter Evoing Type")]
    public int type_evoting { get; set;}
    
    [Required(ErrorMessage ="Enter Total No Of Share")] 
    public decimal  total_nof_share{ get; set;}
    
    [Required(ErrorMessage ="Enter Voting Rights")] 
    public int  voting_rights { get; set;}
    
    [Required(ErrorMessage ="Enter Cut Of Date")] 
    public string cut_of_date{ get; set;}
   
    [Required(ErrorMessage ="Select Scrutinizer")] 
    public int scrutinizer { get; set;}

 }
 
}