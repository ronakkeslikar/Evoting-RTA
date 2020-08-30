using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations; 

namespace evoting.Domain.Models
{
    public class FJC_Registration
    {
        public int SR_NO { get; set;}
        public int REG_TYPE_ID { get; set;}

        [Required (ErrorMessage ="Enter Entity Name") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string NAME { get; set;}  

        [Required (ErrorMessage ="Enter Reg. No.") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string REG_NO { get; set;}
                
        [Required (ErrorMessage ="Enter Registered Office Address") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string REG_ADD1 { get; set;}
         
        public string REG_ADD2 { get; set;}
        public string REG_ADD3 { get; set;}
        [Required (ErrorMessage ="Enter City")] 
        public string REG_CITY { get; set;}
         [Required (ErrorMessage ="Enter Pincode"),RegularExpression(@"^[0-9]*$")] 
         [MinLength(6, ErrorMessage = "Pincode Cannot be less than 6 digit")]  
         [MaxLength(6, ErrorMessage = "Pincode Cannot be more than 6 digit")]  

        public string REG_PINCODE { get; set;}
           [Required (ErrorMessage ="Enter State")] 
        public string REG_STATE_ID { get; set;}
           [Required (ErrorMessage ="Enter Country")] 
        public string REG_COUNTRY { get; set;}       
        [Required (ErrorMessage ="Enter Correspondence Address") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string CORRES_ADD1 { get; set;}
        public string CORRES_ADD2 { get; set;}
        public string CORRES_ADD3 { get; set;}
         [Required (ErrorMessage ="Enter City")]
        public string CORRES_CITY { get; set;}
         [Required (ErrorMessage ="Enter Pincode"),RegularExpression(@"^[0-9]*$")] 
         [MinLength(6, ErrorMessage = "Pincode Cannot be less than 6 digit")]  
         [MaxLength(6, ErrorMessage = "Pincode Cannot be more than 6 digit")] 
        public string CORRES_PINCODE { get; set;}
          [Required (ErrorMessage ="Enter State")] 
        public string CORRES_STATE_ID { get; set;}
             [Required (ErrorMessage ="Enter Country")] 
        public string CORRES_COUNTRY { get; set;}
        public string PCS_NO { get; set;}
         [Required (ErrorMessage ="Enter NAME") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string CS_NAME { get; set;}
         [Required (ErrorMessage ="Enter EMAIL_ID") ,RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string CS_EMAIL_ID { get; set;}
        public string CS_ALT_EMAIL_ID { get; set;}
         [Required (ErrorMessage ="Enter Telephone No.") ,RegularExpression(@"^[0-9]*$")] 
        public string CS_TEL_NO { get; set;}
        public string CS_FAX_NO { get; set;}
        [Required (ErrorMessage ="Enter Mobile No.") ,RegularExpression(@"^[0-9]*$")] 
        public string CS_MOBILE_NO { get; set;}
         
     [Required (ErrorMessage ="Enter PAN ID") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string PANID { get; set;}

    }  
     
}
