
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
    public interface IIntimationService
    {
        Task<DataTable> Intimation_data(FJC_Intimation fjc_intimation, string Token);   
        Task<DataTable> Intimation_Getdata(string Token, string type);      
    }
    public class IntimationService : IIntimationService
    {
        protected readonly AppDbContext _context;
        public IntimationService(AppDbContext context)
        {
            _context = context;
        }  
        //////////////////////////// POST /////////////////////////
         public async Task<DataTable> Intimation_data(FJC_Intimation fjc_intimation, string Token)
        {
          Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
            dictLogin.Add("@token", Token);
            dictLogin.Add("@event_id", fjc_intimation.event_id);
            dictLogin.Add("@event_name", fjc_intimation.event_name);
            dictLogin.Add("@notice_date", (ManageDatetime.DateTimeHandler(fjc_intimation.notice_date)));
            dictLogin.Add("@rom_file", fjc_intimation.rom_file);
            dictLogin.Add("@email_sent_date", (ManageDatetime.DateTimeHandler(fjc_intimation.email_sent_date)));
            dictLogin.Add("@post_sent_date",(ManageDatetime.DateTimeHandler(fjc_intimation.post_sent_date)));
            dictLogin.Add("@flag", 0);
            DataSet ds = new DataSet();

            ds = await AppDBCalls.GetDataSet("Evote_Intimation", dictLogin);           
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        ////////////////////////// Get //////////////////////////////     
         public async Task<DataTable> Intimation_Getdata(string Token, string type )
        {
           Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
            dictLogin.Add("@token", Token);
            dictLogin.Add("@type",type);
            dictLogin.Add("@flag", 1);
            DataSet ds = new DataSet();

            ds = await AppDBCalls.GetDataSet("Evote_Intimation", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);         
        }

        ////////////////////handling Datetime ////////////
        //private object DateTimeHandler(string date_param)
        //{
        //    if (date_param == null)
        //    {
        //        return DBNull.Value;

        //    }
        //    else if(date_param.Trim()==string.Empty)
        //    {
        //        return DBNull.Value;
        //    }
        //    else
        //    {
        //      return DateTime.Parse(date_param).ToString("yyyy-MM-dd HH:mm:ss:fff"); 
        //    }
        //}

    }
}
