using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models.Validate;

namespace evoting.Domain.Models
{
    public class FJC_ForgotPassword
    {
        public string UserID { get; private set; }         

        public void SetUserID(string value)
        {
            //..throw exception if validation failed
            Validate_Login validateString=new Validate_Login();
            bool isString_UserID;
            try
            {
                isString_UserID = validateString.CheckString(value);
               if (isString_UserID)
                {
                    UserID = value;
                }

            }
            catch(Exception e)
            {
                throw e;
            }
           
        }

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
