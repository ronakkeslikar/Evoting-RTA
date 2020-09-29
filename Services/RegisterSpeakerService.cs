using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface IRegisterSpeakerService
    {
        Task<DataTable> RegisterSpeakerData(string speaker, string Token); 
         Task<DataTable> RegisterGetSpeakerData(string speaker, string Token); 

    }
    public class RegisterSpeakerService : IRegisterSpeakerService
    {
        protected readonly AppDbContext _context;
        public RegisterSpeakerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> RegisterSpeakerData(string speaker, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@speaker", speaker);
            dictLogin.Add("@flag", "post");
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_InvestortSpeaker", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }  
        public async Task<DataTable> RegisterGetSpeakerData(string speaker, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@speaker", speaker);
            dictLogin.Add("@flag", "get");
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_InvestortSpeaker", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }         

    }
}
