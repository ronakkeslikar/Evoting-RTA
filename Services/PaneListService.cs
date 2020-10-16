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
    public interface IPaneListService
    {
        Task<DataTable> PaneListData(FJC_PaneList fJC_PaneList, string Token);
        Task<DataTable> PaneListUpdateData(FJC_PaneList fJC_PaneList, string Token);
        Task<DataTable> PaneListDeleteData(FJC_PaneList fJC_PaneList, string Token);
        Task<DataTable> GetPaneList( int event_id,string Token); 

    }
    public class PaneListService : IPaneListService
    {
        protected readonly AppDbContext _context;
        public PaneListService(AppDbContext context)
        {
            _context = context;
        }

        ////////////////////////POST Method for Pane List//////////////////////////
        public async Task<DataTable> PaneListData(FJC_PaneList fJC_PaneList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", fJC_PaneList.event_id);
            dictLogin.Add("@email_id", fJC_PaneList.email_id);
            dictLogin.Add("@name", fJC_PaneList.name);
            dictLogin.Add("@flag", 1);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_PaneList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        ////////////////////////PUT Method for Pane List//////////////////////////
        public async Task<DataTable> PaneListDataUpdate(FJC_PaneList fJC_PaneList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", fJC_PaneList.event_id);
            dictLogin.Add("@email_id", fJC_PaneList.email_id);
            dictLogin.Add("@name", fJC_PaneList.name);
            dictLogin.Add("@id", fJC_PaneList.id);
            dictLogin.Add("@flag", 3);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_PaneList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        ////////////////////////Delete Method for Pane List//////////////////////////
        public async Task<DataTable> PaneListDeleteData(FJC_PaneList fJC_PaneList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", fJC_PaneList.event_id);
            dictLogin.Add("@email_id", fJC_PaneList.email_id);
            dictLogin.Add("@name", fJC_PaneList.name);
            dictLogin.Add("@id", fJC_PaneList.id);
            dictLogin.Add("@flag", 4);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_PaneList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        /////////////////////////Get Method for Pane List//////////////////
        public async Task<DataTable> GetPaneList( int event_id,string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 2);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_PaneList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }         

    }
}
