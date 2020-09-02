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
    public interface ICommonUtilityService
    {     
       
        Task<DataTable> GetCommonUtilityAudienceID(int User_Type,string Token);
        Task<DataTable> GetCommonUtilityISINID(int ISIN_TYPE_ID,string Token);
        
    }

    public class CommonUtilityService : ICommonUtilityService
    {
        //db context here
        protected readonly AppDbContext _context;
        public CommonUtilityService(AppDbContext context)
        {
            _context = context;
        } 
         
         public async Task<DataTable> GetCommonUtilityAudienceID(int User_Type,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
                dictRegis.Add("@USER_TYPE", User_Type);  
                dictRegis.Add("@ISIN_TYPE_ID", 0);   
                dictRegis.Add("@token", Token);            
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_CommonUtility", dictRegis);
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
        public async Task<DataTable> GetCommonUtilityISINID(int ISIN_TYPE_ID,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
                dictRegis.Add("@USER_TYPE", 0);  
                dictRegis.Add("@ISIN_TYPE_ID", ISIN_TYPE_ID);
                 dictRegis.Add("@token", Token);             
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_CommonUtility", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
        
        
    }
}
 