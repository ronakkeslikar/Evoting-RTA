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
    public interface IListService
    {  
       Task<DataTable> GetList_Details(string getCh);        
    }

    public class ListService : IListService
    {
        //db context here
        protected readonly AppDbContext _context;
        public ListService(AppDbContext context)
        {
            _context = context;
        }   
         public async Task<DataTable> GetList_Details(string getCh)
        { 
            Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
            dictRegis.Add("@getCh", getCh);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_GetList", dictRegis);                            
            return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
        
        
    }
}
 