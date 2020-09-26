using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using evoting.Persistence.Contexts.Sp_SQL_Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using static evoting.Persistence.Contexts.Sp_SQL_Objects.SP_objectParam;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;
using evoting.Utility;

namespace evoting.Services
{
    public interface IRegistrationService
    {     
        Task<DataTable> Registration_InsertData(FJC_Registration fJC_Registration);
        Task<DataTable> Registration_UpdateData(FJC_Registration fJC_Registration,string Token);
        Task<DataTable> GetRegistrationIDData(string Token);
        
    }

    public class RegistrationService : IRegistrationService
    {
        //db context here
        protected readonly AppDbContext _context;
        public RegistrationService(AppDbContext context)
        {
            _context = context;
        } 
         public async Task<DataTable> Registration_InsertData(FJC_Registration fJC_Registration)
        {
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();                            
                dictRegis.Add("@aud_id", 0);
                dictRegis.Add("@REG_TYPE_ID",fJC_Registration.reg_type_id);
                dictRegis.Add("@NAME", fJC_Registration.name); 
                dictRegis.Add("@REG_NO", fJC_Registration.reg_no);
                dictRegis.Add("@REG_ADD1",fJC_Registration.reg_add1);
                dictRegis.Add("@REG_ADD2", fJC_Registration.reg_add2);               
                dictRegis.Add("@REG_ADD3", fJC_Registration.reg_add3);
                dictRegis.Add("@REG_CITY", fJC_Registration.reg_city);
                dictRegis.Add("@REG_PINCODE", fJC_Registration.reg_pincode);
                dictRegis.Add("@REG_STATE_ID",fJC_Registration.reg_state_id);  
                dictRegis.Add("@REG_COUNTRY", fJC_Registration.reg_country_id);                
                dictRegis.Add("@CORRES_ADD1", fJC_Registration.corres_add1);  
                dictRegis.Add("@CORRES_ADD2",fJC_Registration.corres_add2);
                dictRegis.Add("@CORRES_ADD3",fJC_Registration.corres_add3);
                dictRegis.Add("@CORRES_CITY", fJC_Registration.corres_city);  
                dictRegis.Add("@CORRES_PINCODE", fJC_Registration.corres_pincode);
                dictRegis.Add("@CORRES_STATE_ID", fJC_Registration.corres_state_id);
                dictRegis.Add("@CORRES_COUNTRY", fJC_Registration.corres_country_id);  
                dictRegis.Add("@PCS_NO", fJC_Registration.pcs_no);
                dictRegis.Add("@CS_NAME",fJC_Registration.cs_name);
                dictRegis.Add("@CS_EMAIL_ID",fJC_Registration.cs_email_id);  
                dictRegis.Add("@CS_ALT_EMAIL_ID", fJC_Registration.cs_alt_email_id);
                dictRegis.Add("@CS_TEL_NO", fJC_Registration.cs_tel_no);
                dictRegis.Add("@CS_FAX_NO", fJC_Registration.cs_fax_no); 
                dictRegis.Add("@CS_MOBILE_NO",fJC_Registration.cs_mobile_no);   
                dictRegis.Add("@PANID",fJC_Registration.panid);
                dictRegis.Add("@alt_mob_num", fJC_Registration.alt_mob_num);
                dictRegis.Add("@rta_id", fJC_Registration.rta_id);

            DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Registration_Details", dictRegis);                              
              return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
         public async Task<DataTable> Registration_UpdateData(FJC_Registration fJC_Registration,string Token)
        {            
                Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
                dictRegis.Add("@REG_TYPE_ID",fJC_Registration.reg_type_id);
                dictRegis.Add("@NAME", fJC_Registration.name); 
                dictRegis.Add("@REG_NO", fJC_Registration.reg_no);
                dictRegis.Add("@REG_ADD1",fJC_Registration.reg_add1);
                dictRegis.Add("@REG_ADD2", fJC_Registration.reg_add2);               
                dictRegis.Add("@REG_ADD3", fJC_Registration.reg_add3);
                dictRegis.Add("@REG_CITY", fJC_Registration.reg_city);
                dictRegis.Add("@REG_PINCODE", fJC_Registration.reg_pincode);
                dictRegis.Add("@REG_STATE_ID",fJC_Registration.reg_state_id);  
                dictRegis.Add("@REG_COUNTRY", fJC_Registration.reg_country_id);                
                dictRegis.Add("@CORRES_ADD1", fJC_Registration.corres_add1);  
                dictRegis.Add("@CORRES_ADD2",fJC_Registration.corres_add2);
                dictRegis.Add("@CORRES_ADD3",fJC_Registration.corres_add3);
                dictRegis.Add("@CORRES_CITY", fJC_Registration.corres_city);  
                dictRegis.Add("@CORRES_PINCODE", fJC_Registration.corres_pincode);
                dictRegis.Add("@CORRES_STATE_ID", fJC_Registration.corres_state_id);
                dictRegis.Add("@CORRES_COUNTRY", fJC_Registration.corres_country_id);  
                dictRegis.Add("@PCS_NO", fJC_Registration.pcs_no);
                dictRegis.Add("@CS_NAME",fJC_Registration.cs_name);
                dictRegis.Add("@CS_EMAIL_ID",fJC_Registration.cs_email_id);  
                dictRegis.Add("@CS_ALT_EMAIL_ID", fJC_Registration.cs_alt_email_id);
                dictRegis.Add("@CS_TEL_NO", fJC_Registration.cs_tel_no);
                dictRegis.Add("@CS_FAX_NO", fJC_Registration.cs_fax_no); 
                dictRegis.Add("@CS_MOBILE_NO",fJC_Registration.cs_mobile_no);   
                dictRegis.Add("@PANID",fJC_Registration.panid);
                dictRegis.Add("@alt_mob_num", fJC_Registration.alt_mob_num);
                dictRegis.Add("@rta_id", fJC_Registration.rta_id);
                dictRegis.Add("@token", Token);
                dictRegis.Add("@flag", "put");
                
                DataSet ds= await AppDBCalls.GetDataSet("Evote_GetRegistrationIDData", dictRegis);                              
             return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
         public async Task<DataTable> GetRegistrationIDData(string Token)
        {
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
                dictRegis.Add("@token", Token); 
                 dictRegis.Add("@flag", "get");              
                DataSet ds = await AppDBCalls.GetDataSet("Evote_GetRegistrationIDData", dictRegis);
              return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        
        
    }
}
 