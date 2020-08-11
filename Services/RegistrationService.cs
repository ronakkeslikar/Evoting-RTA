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
        Task<DataTable> Registration_UpdateData(FJC_Registration fJC_Registration);
        
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
            try
            {
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();
                //dictRegis.Add("@Mode", "I");                
                dictRegis.Add("@RTA_ID", "0");
                dictRegis.Add("@REG_TYPE_ID",fJC_Registration.REG_TYPE_ID);
                dictRegis.Add("@NAME", fJC_Registration.NAME); 
                dictRegis.Add("@REG_NO", fJC_Registration.REG_NO);
                dictRegis.Add("@REG_ADD1",fJC_Registration.REG_ADD1);
                dictRegis.Add("@REG_ADD2", fJC_Registration.REG_ADD2);               
                dictRegis.Add("@REG_ADD3", fJC_Registration.REG_ADD3);
                dictRegis.Add("@REG_CITY", fJC_Registration.REG_CITY);
                dictRegis.Add("@REG_PINCODE", fJC_Registration.REG_PINCODE);
                dictRegis.Add("@REG_STATE_ID",fJC_Registration.REG_STATE_ID);  
                dictRegis.Add("@REG_COUNTRY", fJC_Registration.REG_COUNTRY);
                dictRegis.Add("@SCRUTNIZER_ID",fJC_Registration.SCRUTNIZER_ID);
                dictRegis.Add("@CORRES_ADD1", fJC_Registration.CORRES_ADD1);  
                dictRegis.Add("@CORRES_ADD2",fJC_Registration.CORRES_ADD2);
                dictRegis.Add("@CORRES_ADD3",fJC_Registration.CORRES_ADD3);
                dictRegis.Add("@CORRES_CITY", fJC_Registration.CORRES_CITY);  
                dictRegis.Add("@CORRES_PINCODE", fJC_Registration.CORRES_PINCODE);
                dictRegis.Add("@CORRES_STATE_ID", fJC_Registration.CORRES_STATE_ID);
                dictRegis.Add("@CORRES_COUNTRY", fJC_Registration.CORRES_COUNTRY);  
                dictRegis.Add("@PCS_NO", fJC_Registration.PCS_NO);
                dictRegis.Add("@CS_NAME",fJC_Registration.CS_NAME);
                dictRegis.Add("@CS_EMAIL_ID",fJC_Registration.CS_EMAIL_ID);  
                dictRegis.Add("@CS_ALT_EMAIL_ID", fJC_Registration.CS_ALT_EMAIL_ID);
                dictRegis.Add("@CS_TEL_NO", fJC_Registration.CS_TEL_NO);
                dictRegis.Add("@CS_FAX_NO", fJC_Registration.CS_FAX_NO); 
                dictRegis.Add("@CS_MOBILE_NO",fJC_Registration.CS_MOBILE_NO);
                dictRegis.Add("@CREATED_DATE", fJC_Registration.CREATED_DATE);
                dictRegis.Add("@MODIFIED_DATE", fJC_Registration.MODIFIED_DATE);                 

                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Registration_Details", dictRegis);
                return ds.Tables[0];               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         public async Task<DataTable> Registration_UpdateData(FJC_Registration fJC_Registration)
        {
            try
            {
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();   
                // dictRegis.Add("@Mode", "U");               
                dictRegis.Add("@RTA_ID", fJC_Registration.RTA_ID);
                dictRegis.Add("@REG_TYPE_ID",fJC_Registration.REG_TYPE_ID);
                dictRegis.Add("@NAME", fJC_Registration.NAME); 
                dictRegis.Add("@REG_NO", fJC_Registration.REG_NO);
                dictRegis.Add("@REG_ADD1",fJC_Registration.REG_ADD1);
                dictRegis.Add("@REG_ADD2", fJC_Registration.REG_ADD2);               
                dictRegis.Add("@REG_ADD3", fJC_Registration.REG_ADD3);
                dictRegis.Add("@REG_CITY", fJC_Registration.REG_CITY);
                dictRegis.Add("@REG_PINCODE", fJC_Registration.REG_PINCODE);
                dictRegis.Add("@REG_STATE_ID",fJC_Registration.REG_STATE_ID);  
                dictRegis.Add("@REG_COUNTRY", fJC_Registration.REG_COUNTRY);
                dictRegis.Add("@SCRUTNIZER_ID",fJC_Registration.SCRUTNIZER_ID);
                dictRegis.Add("@CORRES_ADD1", fJC_Registration.CORRES_ADD1);  
                dictRegis.Add("@CORRES_ADD2",fJC_Registration.CORRES_ADD2);
                dictRegis.Add("@CORRES_ADD3",fJC_Registration.CORRES_ADD3);
                dictRegis.Add("@CORRES_CITY", fJC_Registration.CORRES_CITY);  
                dictRegis.Add("@CORRES_PINCODE", fJC_Registration.CORRES_PINCODE);
                dictRegis.Add("@CORRES_STATE_ID", fJC_Registration.CORRES_STATE_ID);
                dictRegis.Add("@CORRES_COUNTRY", fJC_Registration.CORRES_COUNTRY);  
                dictRegis.Add("@PCS_NO", fJC_Registration.PCS_NO);
                dictRegis.Add("@CS_NAME",fJC_Registration.CS_NAME);
                dictRegis.Add("@CS_EMAIL_ID",fJC_Registration.CS_EMAIL_ID);  
                dictRegis.Add("@CS_ALT_EMAIL_ID", fJC_Registration.CS_ALT_EMAIL_ID);
                dictRegis.Add("@CS_TEL_NO", fJC_Registration.CS_TEL_NO);
                dictRegis.Add("@CS_FAX_NO", fJC_Registration.CS_FAX_NO); 
                dictRegis.Add("@CS_MOBILE_NO",fJC_Registration.CS_MOBILE_NO);
                dictRegis.Add("@CREATED_DATE", fJC_Registration.CREATED_DATE);
                dictRegis.Add("@MODIFIED_DATE", fJC_Registration.MODIFIED_DATE);                 

                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Registration_Details", dictRegis);
                return ds.Tables[0];               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
    }
}
 