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
    public interface IAccountSearchService
    {  
       Task<DataTable> GetSearch_Details(FJC_AccountSearch fJC_AccountSearch,string Token); 
       Task<DataTable> GetAudience_Details(int aud_id,string Token); 
       Task<DataTable> Verify_AccountData(int aud_id,string Token);
       Task<DataTable> GetAccountList_Details(int user_type,string Token); 

    }

    public class AccountSearchService : IAccountSearchService
    {
        //db context here
        protected readonly AppDbContext _context;
        public AccountSearchService(AppDbContext context)
        {
            _context = context;
        }  

         /////////////////////////Get Serach using POST method/////////////////////
         public async Task<DataTable> GetSearch_Details(FJC_AccountSearch fJC_AccountSearch,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
               
                dictRegis.Add("@user_type", fJC_AccountSearch.user_type);
                dictRegis.Add("@str", fJC_AccountSearch.str);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetAccountSearch", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }

        //////////////////Verify Account using POST method///////////////////////////
          public async Task<DataTable> Verify_AccountData(int aud_id,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
                
                dictRegis.Add("@aud_id", aud_id);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_VerifyAccount", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }

        /////////////////Get Audience Details using get method //////////////////////////

        public async Task<DataTable> GetAudience_Details(int aud_id,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
                
                dictRegis.Add("@aud_id", aud_id);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetAudienceDetails", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }

         /////////////////Get Account List Details using "List" get method //////////////////////////
        
         public async Task<DataTable> GetAccountList_Details(int user_type,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
                
                dictRegis.Add("@user_type", user_type);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetAccountList", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
    }
}
 