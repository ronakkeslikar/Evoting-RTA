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
    public interface INoticeService
    {  
       Task<DataTable> GetNotice_Details(FJC_Notice fjc_notice);  
       Task<DataTable> GetIssuer_Details(string str);        

    }

    public class NoticeService : INoticeService
    {
        //db context here
        protected readonly AppDbContext _context;
        public NoticeService(AppDbContext context)
        {
            _context = context;
        }   

        ////////////////Get For Notice Controller/////////////////
         public async Task<DataTable> GetNotice_Details(FJC_Notice fjc_notice)
        { 
            Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
            dictRegis.Add("@issuer_name", fjc_notice.issuer_name);
            dictRegis.Add("@type", fjc_notice.type);
            dictRegis.Add("@start_date", fjc_notice.start_date);
            dictRegis.Add("@end_date", fjc_notice.end_date);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_notice_result_fetch", dictRegis);                            
            return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }
       ////////////////////get For Issuer Controller///////////////////
       public async Task<DataTable> GetIssuer_Details(string str)
        { 
            Dictionary<string, object> dictRegis = new Dictionary<string, object>(); 
            dictRegis.Add("@getCh", str);           

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_GetList", dictRegis);                            
            return Reformatter.Validate_DataTable(ds.Tables[0]); 
        } 
        
    }
}
 