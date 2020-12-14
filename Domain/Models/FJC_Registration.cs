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
        public int aud_id { get; set;}
        public int reg_type_id { get; set;}

        [Required (ErrorMessage ="Enter Entity Name") ,RegularExpression(@"^[a-zA-Z0-9 -,_]*$")] 
        public string name { get; set;}  

        //[Required (ErrorMessage ="Enter Reg. No.") ,RegularExpression(@"^[a-zA-Z0-9 -,_]*$")] 
        public string reg_no { get; set;}
                
        [Required (ErrorMessage ="Enter Registered Office Address") ,RegularExpression(@"^[a-zA-Z0-9 -,_]*$")] 
        public string reg_add1 { get; set;}
         
        public string reg_add2 { get; set;}
        public string reg_add3 { get; set;}
        [Required (ErrorMessage ="Enter City")] 
        public string reg_city { get; set;}
         [Required (ErrorMessage ="Enter Pincode"),RegularExpression(@"^[0-9]*$")] 
         [MinLength(6, ErrorMessage = "Pincode Cannot be less than 6 digit")]  
         [MaxLength(6, ErrorMessage = "Pincode Cannot be more than 6 digit")]  

        public string reg_pincode { get; set;}
           [Required (ErrorMessage ="Enter State")] 
        public int reg_state_id { get; set;}
           [Required (ErrorMessage ="Enter Country")] 
        public int reg_country_id { get; set;}       
        [Required (ErrorMessage ="Enter Correspondence Address") ,RegularExpression(@"^[a-zA-Z0-9 -,_]*$")] 
        public string corres_add1 { get; set;}
        public string corres_add2 { get; set;}
        public string corres_add3 { get; set;}
         [Required (ErrorMessage ="Enter City")]
        public string corres_city { get; set;}
         [Required (ErrorMessage ="Enter Pincode"),RegularExpression(@"^[0-9]*$")] 
         [MinLength(6, ErrorMessage = "Pincode Cannot be less than 6 digit")]  
         [MaxLength(6, ErrorMessage = "Pincode Cannot be more than 6 digit")] 
        public string corres_pincode { get; set;}
          [Required (ErrorMessage ="Enter State")] 
        public int corres_state_id { get; set;}
             [Required (ErrorMessage ="Enter Country")] 
        public int corres_country_id { get; set;}       
        public string pcs_no { get; set;}
         [Required (ErrorMessage ="Enter NAME") ,RegularExpression(@"^[a-zA-Z0-9 -,_]*$")] 
        public string cs_name { get; set;}
         [Required (ErrorMessage ="Enter EMAIL_ID") ,RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string cs_email_id { get; set;}
        public string cs_alt_email_id { get; set;}
         [Required (ErrorMessage ="Enter Telephone No.") ,RegularExpression(@"^[0-9]*$")] 
        public string cs_tel_no { get; set;}
        public string cs_fax_no { get; set;}
        [Required (ErrorMessage ="Enter Mobile No.") ,RegularExpression(@"^[0-9]*$")] 
        public string cs_mobile_no { get; set;}
         
     [Required (ErrorMessage ="Enter PAN ID") ,RegularExpression(@"^[a-zA-Z0-9]*$")] 
        public string panid { get; set;}
        public string alt_mob_num{get;set;}//change from decimal to string 
          public int rta_id{get;set;}

        [Required(ErrorMessage = "Enter captcha response")]
        public string captcha { get; set; }

    }  
     
}
