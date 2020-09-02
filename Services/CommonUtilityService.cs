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
       Task<DataTable> GetCommonDetails(string getCh,string Token);        
    }

    public class CommonUtilityService : ICommonUtilityService
    {
        //db context here
        protected readonly AppDbContext _context;
        public CommonUtilityService(AppDbContext context)
        {
            _context = context;
        }   
         public async Task<DataTable> GetCommonDetails(string getCh,string Token)
        { 
                Dictionary<string, object> dictRegis = new Dictionary<string, object>();               
               
                dictRegis.Add("@getCh", getCh);
                dictRegis.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_CommonUtility", dictRegis);                            
              return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
        
        
    }
}
 