using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;
using System.ComponentModel.DataAnnotation;

namespace evoting.Domain.Models
{
    public class FJC_ForgotPassword
    {
        //private string _userid;
        //public string UserID { get { return _userid; } set { _userid = (Validate_Login.CheckString(_userid) ? _userid : ""); } }         
          [Required(ErroreMessage ="Enter User ID")]                
          public string UserID { get; set; }
        public string encrypt_OldPassword { get; set; }
        public string encrypt_NewPassword { get; set; }
        public string EmailID { get; set; }
        public string PAN_ID { get; set; }

        public string Bank_AccNo { get; set; }

        public string DOB { get; set; }

        public char TypeOfUpdate { get; set; }
        public char TypeOfUser { get; set; }


    }
}
