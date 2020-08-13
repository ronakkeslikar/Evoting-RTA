using evoting.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface ITestingService
    {
        string GetClassToJSON(string FJC_ClassName);
    }
    public class TestingServices : ITestingService
    {
        public string GetClassToJSON( string FJC_ClassName)
        {
            string ret = string.Empty;
            try
            {
                switch (FJC_ClassName)
                {
                    case "LoginRequest":
                        ret = JsonConvert.SerializeObject(new FJC_LoginRequest() { UserID = "1", system_ip = "192.168.0.1", encrypt_Password = "xxx" });
                        break;
                    case "ForgotPassword":
                        ret = JsonConvert.SerializeObject(new FJC_ForgotPassword() { UserID = "IN20001232145678", Bank_AccNo = "23456", 
                            DOB="13/08/2020", EmailID="abc@live.com", encrypt_NewPassword = "xxx", encrypt_OldPassword = "XXX", PAN_ID="XXXXXXXXXX",
                            TypeOfUpdate = 's', TypeOfUser = 'S'  });
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
                return ret;            
        }
    }
}
