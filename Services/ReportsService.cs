using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface IReportsService
    {
        Task<DataTable> ReportsData(int event_id, string Token);  
        Task<DataTable> ReportsGetData(int event_id, string Token);  

    }
    public class ReportsService : IReportsService
    {
        protected readonly AppDbContext _context;
        public ReportsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> ReportsData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 0);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Scrutinizer_Report", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        public async Task<DataTable> ReportsGetData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 1);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Scrutinizer_Report", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

    }
}
