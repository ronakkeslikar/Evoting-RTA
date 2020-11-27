using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotations;

namespace evoting.Domain.Models
{
    public class FJC_ForgotPassword
    {
        //private string _panid;        
        [Required(ErrorMessage ="Enter User ID")]                
        public string UserID { get; set; } 
        public string EmailID { get; set; }

       //[Required(ErrorMessage ="Enter PAN ID"),RegularExpression(@"^[a-zA-Z0-9]*$")]  
       public string PAN_ID { get; set; }
       //public string PAN_ID { get { return _panid; } set { _panid = (Validate_Login.CheckString(_panid) ? _panid : "Enter PAN ID"); } }         

        public string Bank_AccNo { get; set; }

        public string DOB { get; set; }

        public char TypeOfUpdate { get; set; }
        public char TypeOfUser { get; set; }


    }
}
