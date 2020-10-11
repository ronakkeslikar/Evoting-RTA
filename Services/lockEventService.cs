using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface ILockEventService
    {
        Task<DataTable> LockEventData(int event_id, string Token);
        Task<DataTable> UnlockEventData(int event_id, string token);

    }
    public class LockEventService : ILockEventService
    {
        protected readonly AppDbContext _context;
        public LockEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> LockEventData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);
            dictLogin.Add("@flag", 1);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("sp_LockEvent", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        } 
        public async Task<DataTable> UnlockEventData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);
            dictLogin.Add("@flag", 0);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("sp_LockEvent", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }         
    }
}
