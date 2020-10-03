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
    public interface IRegisterSpeakerService
    {
        Task<DataTable> RegisterSpeakerData(FJC_SpeakerRegister fJC_Speaker, string Token); 
         Task<DataTable> RegisterGetSpeakerData( string Token); 

    }
    public class RegisterSpeakerService : IRegisterSpeakerService
    {
        protected readonly AppDbContext _context;
        public RegisterSpeakerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> RegisterSpeakerData(FJC_SpeakerRegister fJC_Speaker, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", fJC_Speaker.event_id);
            dictLogin.Add("@email", fJC_Speaker.email);
            dictLogin.Add("@flag", 1);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_InvestortSpeaker", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }  
        public async Task<DataTable> RegisterGetSpeakerData( string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
           
            dictLogin.Add("@flag", 2);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_InvestortSpeaker_get", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }         

    }
}
