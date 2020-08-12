using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Domain.Models
{
    public class FJC_Registration
    {
        public String RTA_ID { get; set;}
        public int REG_TYPE_ID { get; set;}
        public string NAME { get; set;}              

        public string SetNAME(string value)
        {
            if(value != null)
            {
                //..throw exception if validation failed
                Validate_Login validateString=new Validate_Login();
                bool isString_NAME;
                try
                {
                    isString_NAME = validateString.CheckString(value);
                if (isString_NAME)
                    {
                        NAME = value;
                        return "Ok";
                    }
                    else
                    {
                        return "Please Enter Valid Name";

                    }

                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return "Please Enter Name";
            }
           
        }

        public string REG_NO { get; set;}
        public string SetREG_NO(string value)
            {
                if(value != null)
                {
                    //..throw exception if validation failed
                    Validate_Login validateString=new Validate_Login();
                    bool isString_REG_NO;
                    try
                    {
                        isString_REG_NO = validateString.CheckString(value);
                    if (isString_UserID)
                        {
                            REG_NO = value;
                            return "Ok";
                        }
                        else
                        {
                            return "Please Enter Valid Reg. No / CIN";

                        }

                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    return "Please Enter Reg. No / CIN";
                }
            
            }        
        public string REG_ADD1 { get; set;}
        public string SetREG_ADD1(string value)
            {
                if(value != null)
                {
                    //..throw exception if validation failed
                    Validate_Login validateString=new Validate_Login();
                    bool isString_REG_ADD1;
                    try
                    {
                        isString_REG_ADD1 = validateString.CheckString(value);
                    if (isString_REG_ADD1)
                        {
                            REG_ADD1 = value;
                            return "Ok";
                        }
                        else
                        {
                            return "Please Enter Valid Address";
                        }

                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    return "Please Enter Address";
                }
            
            }
        public string REG_ADD2 { get; set;}
        public string REG_ADD3 { get; set;}
        public string REG_CITY { get; set;}
        public String REG_PINCODE { get; set;}
        public String REG_STATE_ID { get; set;}
        public string REG_COUNTRY { get; set;}
        public string SCRUTNIZER_ID { get; set;}
        public string CORRES_ADD1 { get; set;}
        public string CORRES_ADD2 { get; set;}
        public string CORRES_ADD3 { get; set;}
        public string CORRES_CITY { get; set;}
        public String CORRES_PINCODE { get; set;}
        public String CORRES_STATE_ID { get; set;}
        public string CORRES_COUNTRY { get; set;}
        public string PCS_NO { get; set;}
        public string CS_NAME { get; set;}
        public string CS_EMAIL_ID { get; set;}
        public string CS_ALT_EMAIL_ID { get; set;}
        public string CS_TEL_NO { get; set;}
        public string CS_FAX_NO { get; set;}
        public string CS_MOBILE_NO { get; set;}
        public DateTime CREATED_DATE { get; set;}
        public DateTime MODIFIED_DATE { get; set;}
    }  
     
}
