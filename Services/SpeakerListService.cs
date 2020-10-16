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
    public interface ISpeakerListService
    {
        Task<DataTable> SpeakerListData(FJC_SpeakerList fJC_SpeakerList, string Token);
        Task<DataTable> SpeakerListUpdateData(FJC_SpeakerList fJC_SpeakerList, string Token);

        Task<DataTable> SpeakerListDeleteData(FJC_SpeakerList fJC_SpeakerList, string Token);

        Task<DataTable> GetSpeakerList( int event_id,string Token); 

    }
    public class SpeakerListService : ISpeakerListService
    {
        protected readonly AppDbContext _context;
        public SpeakerListService(AppDbContext context)
        {
            _context = context;
        }

        ////////////////////////POST Method for Speaker List//////////////////////////
        public async Task<DataTable> SpeakerListData(FJC_SpeakerList fJC_SpeakerList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", fJC_SpeakerList.event_id);
            dictLogin.Add("@email_id", fJC_SpeakerList.email_id);
            dictLogin.Add("@name", fJC_SpeakerList.name);
            dictLogin.Add("@flag", 1);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_SpeakerList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        ////////////////////////PUT Method for Speaker List//////////////////////////
        public async Task<DataTable> SpeakerListUpdateData(FJC_SpeakerList fJC_SpeakerList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", fJC_SpeakerList.event_id);
            dictLogin.Add("@email_id", fJC_SpeakerList.email_id);
            dictLogin.Add("@name", fJC_SpeakerList.name);
            dictLogin.Add("@id", fJC_SpeakerList.id);
            dictLogin.Add("@flag", 3);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_SpeakerList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        ////////////////////////Delete  Method for Speaker List//////////////////////////
        public async Task<DataTable> SpeakerListDeleteData(FJC_SpeakerList fJC_SpeakerList, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", fJC_SpeakerList.event_id);
            dictLogin.Add("@email_id", fJC_SpeakerList.email_id);
            dictLogin.Add("@name", fJC_SpeakerList.name);
            dictLogin.Add("@id", fJC_SpeakerList.id);
            dictLogin.Add("@flag", 3);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_SpeakerList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        ///////////////////////Get Method for Speaker List/////////////////////////
        public async Task<DataTable> GetSpeakerList( int event_id,string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 2);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_SpeakerList", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }         

    }
}
