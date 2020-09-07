using evoting.Domain.Models;
using evoting.Persistence.Contexts;
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
        string DBCheck();
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
                            DOB="13/08/2020", EmailID="abc@live.com", PAN_ID="XXXXXXXXXX",
                            TypeOfUpdate = 's', TypeOfUser = 'S'  });
                        break;
                    case "CompanyUpdate_Event":
                        ret = JsonConvert.SerializeObject(new FJC_CompanyUpdate_Event()
                        {
                            cut_of_date = "",
                            enter_nof_resolution = 9,
                            event_id = 1,
                            isin = "",
                            last_date_notice = "",
                            meeting_datetime = "",
                            resolutions = return_resolution(),
                            scrutinizer = 1,
                            total_nof_share = 100000,
                            type_evoting = 1,
                            type_isin = 3,
                            upload_logo = 1,
                            upload_notice = 2,
                            upload_resolution_file = 3,
                            voting_end_datetime = "",
                            voting_result_date = "",
                            voting_rights = 1,
                            voting_start_datetime = ""
                        });
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
                return ret;            
        }
        private FJC_Resolutions_Data[] return_resolution()
        {
            List<FJC_Resolutions_Data> _obj = new List<FJC_Resolutions_Data>();
            _obj.Add(new FJC_Resolutions_Data() { doc_id = 1, description = "desc1", title = "", resolution_id = 1 });
            _obj.Add(new FJC_Resolutions_Data() { doc_id = 2, description = "desc2", title = "", resolution_id = 2 });
            _obj.Add(new FJC_Resolutions_Data() { doc_id = 3, description = "desc3", title = "", resolution_id = 3 });
            return _obj.ToArray();
        }
        public string DBCheck()
        {
            //return JsonConvert.SerializeObject(AppDBCalls.Rel_connection);
            return JsonConvert.SerializeObject(AppDBCalls.GetDataSet("TestingServices", new Dictionary<string, object>() { { "@flag", 1 } }));
        }
    }
}
