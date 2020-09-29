
using evoting.Domain.Models;
using evoting.Persistence.Contexts;
using evoting.Utility;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface IVote_InvestorService
    {
        Task<DataSet> Vote_Investor_data(FJC_Vote_Investor fjc_Vote_Investor, string Token);   
        Task<DataTable> Vote_Investor_Getdata(string Token);      
    }
    public class Vote_InvestorService : IVote_InvestorService
    {
        protected readonly AppDbContext _context;
        public Vote_InvestorService(AppDbContext context)
        {
            _context = context;
        }  
        ////////////////////////////Get/////////////////////////
         public async Task<DataTable> Vote_Investor_Getdata(string Token)
        {
          Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
            dictLogin.Add("@token", Token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Vote_Investor", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        //////////////////////////POST//////////////////////////////     
         public async Task<DataSet> Vote_Investor_data(FJC_Vote_Investor fjc_Vote_Investor, string Token)
        {
           DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Vote_Investor", CommonSpParam(fjc_Vote_Investor, Token), PassResolutionArray(fjc_Vote_Investor.resolutions));
            return Reformatter.Validate_Dataset(ds);
        }

         private Dictionary<string,object> CommonSpParam(FJC_Vote_Investor fjc_Vote_Investor, string Token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
                dictLogin.Add("@event_id",fjc_Vote_Investor.event_id);
                dictLogin.Add("@submitted", fjc_Vote_Investor.submitted);         
                dictLogin.Add("@token", Token);
                return dictLogin;
        }

          private SqlParameter PassResolutionArray(FJC_Resolutions_Vote[] fJC_Resolutions_v )
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("resolution_id");
            _dt.Columns.Add("in_favour");
            _dt.Columns.Add("not_in_favour");
            _dt.Columns.Add("abstain");

            foreach(var item in fJC_Resolutions_v)
            {
                DataRow row = _dt.NewRow();
                row["resolution_id"] = item.resolution_id;
                row["in_favour"] = item.in_favour;
                row["not_in_favour"] = item.not_in_favour;
                row["abstain"] = item.abstain;
                _dt.Rows.Add(row);
            }

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ResolutionVoteArray";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = _dt;
            return parameter;
        }

    }
}
