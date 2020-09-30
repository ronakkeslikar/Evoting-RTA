using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;

namespace evoting.Services
{
    public interface IShareHolderService
    {
        Task<DataTable> ShareHolder_RestrictData(FJC_SharedHolder_Restrict fjc_SharedHolder_Restrict, string Token);      
        Task<DataTable> GetShareHolder_RestrictData(int event_id,string Token);
        Task<DataTable> ShareHolder_DerestrictData(FJC_SharedHolder_Derestrict fjc_SharedHolder_Derestrict, string Token);      


    }
    public class ShareHolderService : IShareHolderService
    {
        protected readonly AppDbContext _context;
        public ShareHolderService(AppDbContext context)
        {
            _context = context;
        } 
        //////////////////////////////ShareHolder Restrict/////////////////////////////////
         public async Task<DataTable> ShareHolder_RestrictData(FJC_SharedHolder_Restrict fjc_SharedHolder_Restrict,string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", fjc_SharedHolder_Restrict.event_id);
            dictLogin.Add("@dpcl",fjc_SharedHolder_Restrict.dpcl);
            dictLogin.Add("@remark", fjc_SharedHolder_Restrict.remark); 
            dictLogin.Add("@flag", 2);             
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_ShareHolder_Restrict", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        public async Task<DataTable> GetShareHolder_RestrictData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);    
            dictLogin.Add("@flag", 1);             
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_ShareHolder_Restrict", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        //////////////////////////////ShareHolder Derestrict/////////////////////////////////

            public async Task<DataTable> ShareHolder_DerestrictData(FJC_SharedHolder_Derestrict fjc_SharedHolder_Derestrict,string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", fjc_SharedHolder_Derestrict.event_id);
            dictLogin.Add("@dpcl",fjc_SharedHolder_Derestrict.dpcl);           
            dictLogin.Add("@flag", 3);             
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_ShareHolder_Restrict", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
    }
}
