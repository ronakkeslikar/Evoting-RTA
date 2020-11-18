
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
    public interface IRequestBillService
    {
        Task<DataTable> RequestBill_data(FJC_RequestBill fjc_RequestBill, string Token);   
        Task<DataTable> GetRequestBillData(string Token, int id,int flag);
    }
    public class RequestBillService : IRequestBillService
    {
        protected readonly AppDbContext _context;
        public RequestBillService(AppDbContext context)
        {
            _context = context;
        }  
        //////////////////////////// POST /////////////////////////
         public async Task<DataTable> RequestBill_data(FJC_RequestBill fjc_RequestBill, string Token)
        {
          Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
            dictLogin.Add("@token", Token);
            dictLogin.Add("@event_id", fjc_RequestBill.event_id);            
            dictLogin.Add("@invoice_date", (DateTimeHandler(fjc_RequestBill.invoice_date)));           
            dictLogin.Add("@mailed_date", (DateTimeHandler(fjc_RequestBill.mailed_date)));
            dictLogin.Add("@paid_date",(DateTimeHandler(fjc_RequestBill.paid_date)));
            dictLogin.Add("@flag", 0);
            DataSet ds = new DataSet();

            ds = await AppDBCalls.GetDataSet("Evote_RequestBill", dictLogin);           
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        ////////////////////////// Get Request Bill //////////////////////////////     
         public async Task<DataTable> GetRequestBillData(string Token, int id ,int flag)
        {
           Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
            dictLogin.Add("@token", Token);
            dictLogin.Add("@id",id);
            dictLogin.Add("@flag", flag);
            DataSet ds = new DataSet();

            ds = await AppDBCalls.GetDataSet("Evote_RequestBill", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);         
        }
        
        ////////////////////handling Datetime ////////////
        private object DateTimeHandler(string date_param)
        {
            if(date_param.Trim()==string.Empty)
            {
                return DBNull.Value;
            }
            else
            {
              return DateTime.Parse(date_param).ToString("yyyy-MM-dd HH:mm:ss:fff"); 
            }
        }

    }
}
