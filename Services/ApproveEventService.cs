using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface IApproveEventService
    {
        Task<DataTable> ApproveEVENT(int event_id, string Token);

    }
    public class ApproveEventService : IApproveEventService
    {
        protected readonly AppDbContext _context;
        public ApproveEventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> ApproveEVENT(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("sp_ApproveEvent", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

    }
}
