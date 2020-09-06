using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using evoting.Persistence.Contexts.Sp_SQL_Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using static evoting.Persistence.Contexts.Sp_SQL_Objects.SP_objectParam;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;
using evoting.Utility;

namespace evoting.Services
{

       public interface IGenerateEVENTService
    {
         Task<DataTable> GenerateEVENT(FJC_GenerateEVENT fJC_EVSN, string Token);
         Task<DataSet> EVENTDetail(FJC_UpdateEVENT fJC_EVENT, string Token) ;
         Task<DataSet> EVENTDetail(FJC_CompanyUpdate_Event fJC_EVENT, string Token);
          
         Task<DataSet> GetEVENTDetail(int EVENT_DETAIL_ID, string Token) ;

        Task<DataSet> GetCompanyEVENTDetail(int EVENT_ID, string Token);
    } 

        public class GenerateEVENTService : IGenerateEVENTService
    {
        //db context here
        protected readonly AppDbContext _context;
        public GenerateEVENTService(AppDbContext context)
        {
            _context = context;
        } 
        ///////////////////////////////////////GenerateEVENTDetail///////////////////////////////////////////////////////////
            public async Task<DataTable> GenerateEVENT(FJC_GenerateEVENT fJC_EVSN, string Token)
        {       
           return await CommonEventprocess(fJC_EVSN,Token); 
        }
          
            private async Task<DataTable> CommonEventprocess(FJC_GenerateEVENT fJC_EVSN, string Token)
        {
               Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_ID", fJC_EVSN.event_id);
                dictLogin.Add("@ISIN", fJC_EVSN.isin);                                
                dictLogin.Add("@TYPE_ISIN", fJC_EVSN.type_isin);               
                dictLogin.Add("@TYPE_EVOTING", fJC_EVSN.type_evoting);
                dictLogin.Add("@TOTAL_NOF_SHARE", fJC_EVSN.total_nof_share);               
                dictLogin.Add("@VOTING_RIGHTS", fJC_EVSN.voting_rights);
                dictLogin.Add("@CUT_OF_DATE", fJC_EVSN.cut_of_date);//new DateTime(2012, 12, 25, 10, 30, 50).ToString("yyyy-MM-dd HH:mm:ss"));               
                dictLogin.Add("@SCRUTINIZER", fJC_EVSN.scrutinizer);                   
                dictLogin.Add("@token", Token);
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);               
        }
      
            
///////////////////////////////////////UpdateEVENT////////////////////////////////////////////////////////////////
          public async Task<DataSet> EVENTDetail(FJC_UpdateEVENT fJC_EVENT, string Token)
       {                
            DataSet ds=new DataSet();
            ds= await AppDBCalls.GetDataSet("EVOTE_EVENT_DETAIL", CommonSpParam(fJC_EVENT,Token), PassResolutionArray(fJC_EVENT.resolutions_Datas));
            return Reformatter.Validate_Dataset(ds);
        }

          public async Task<DataSet> EVENTDetail(FJC_CompanyUpdate_Event fJC_CompanyUpdate_Event, string Token)
        {
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("EVOTE_companyEVENT_DETAIL", CommonSpParam(fJC_CompanyUpdate_Event, Token), PassResolutionArray(fJC_CompanyUpdate_Event.resolutions_Datas));
            return Reformatter.Validate_Dataset(ds);
        }
        private Dictionary<string,object> CommonSpParam(FJC_UpdateEVENT fJC_EVENT, string Token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();                
                dictLogin.Add("@EVENT_ID", fJC_EVENT.event_id);           
                dictLogin.Add("@VOTING_START_DATETIME", (DateTime.Parse(fJC_EVENT.voting_start_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));               
                dictLogin.Add("@VOTING_END_DATETIME", (DateTime.Parse(fJC_EVENT.voting_end_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));           
                dictLogin.Add("@MEETING_DATETIME", (DateTime.Parse(fJC_EVENT.meeting_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));    
                dictLogin.Add("@LAST_DATE_NOTICE",(DateTime.Parse(fJC_EVENT.last_date_notice)).ToString("yyyy-MM-dd hh:mm:ss:fff"));         
                dictLogin.Add("@VOTING_RESULT_DATE",(DateTime.Parse(fJC_EVENT.voting_result_date)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
                dictLogin.Add("@UPLOAD_LOGO", fJC_EVENT.upload_logo);             
                dictLogin.Add("@UPLOAD_RESOLUTION_FILE", fJC_EVENT.upload_resolution_file);
                dictLogin.Add("@UPLOAD_NOTICE", fJC_EVENT.upload_notice); 
                dictLogin.Add("@ENTER_NOF_RESOLUTION", fJC_EVENT.enter_nof_resolution);
                dictLogin.Add("@token", Token);
                return dictLogin;
        }

        private Dictionary<string, object> CommonSpParam(FJC_CompanyUpdate_Event fJC_CompanyUpdate_Event, string Token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            
            dictLogin.Add("@EVENT_ID", fJC_CompanyUpdate_Event.event_id);
            dictLogin.Add("@ISIN", fJC_CompanyUpdate_Event.isin);
            dictLogin.Add("@TYPE_ISIN", fJC_CompanyUpdate_Event.type_isin);
            dictLogin.Add("@TYPE_EVOTING", fJC_CompanyUpdate_Event.type_evoting);
            dictLogin.Add("@TOTAL_NOF_SHARE", fJC_CompanyUpdate_Event.total_nof_share);
            dictLogin.Add("@VOTING_RIGHTS", fJC_CompanyUpdate_Event.voting_rights);
            dictLogin.Add("@CUT_OF_DATE", (DateTime.Parse(fJC_CompanyUpdate_Event.cut_of_date)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@SCRUTINIZER", fJC_CompanyUpdate_Event.scrutinizer);
            dictLogin.Add("@VOTING_START_DATETIME", (DateTime.Parse(fJC_CompanyUpdate_Event.voting_start_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@VOTING_END_DATETIME", (DateTime.Parse(fJC_CompanyUpdate_Event.voting_end_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@MEETING_DATETIME", (DateTime.Parse(fJC_CompanyUpdate_Event.meeting_datetime)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@LAST_DATE_NOTICE", (DateTime.Parse(fJC_CompanyUpdate_Event.last_date_notice)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@VOTING_RESULT_DATE", (DateTime.Parse(fJC_CompanyUpdate_Event.voting_result_date)).ToString("yyyy-MM-dd hh:mm:ss:fff"));
            dictLogin.Add("@UPLOAD_LOGO", fJC_CompanyUpdate_Event.upload_logo);
            dictLogin.Add("@UPLOAD_RESOLUTION_FILE", fJC_CompanyUpdate_Event.upload_resolution_file);
            dictLogin.Add("@UPLOAD_NOTICE", fJC_CompanyUpdate_Event.upload_notice);
            dictLogin.Add("@ENTER_NOF_RESOLUTION", fJC_CompanyUpdate_Event.enter_nof_resolution);
            dictLogin.Add("@token", Token);
            return dictLogin;
        }

        private SqlParameter PassResolutionArray(FJC_Resolutions_Data[] fJC_Resolutions_s )
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("resolution_id");
            _dt.Columns.Add("title");
            _dt.Columns.Add("description");
            _dt.Columns.Add("doc_id");

            foreach(var item in fJC_Resolutions_s)
            {
                DataRow row = _dt.NewRow();
                row["resolution_id"] = item.resolution_id;
                row["title"] = item.title;
                row["description"] = item.description;
                row["doc_id"] = item.doc_id;
                _dt.Rows.Add(row);
            }

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ResolutionDataArray";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = _dt;
            return parameter;
        }

        public async Task<DataSet> GetEVENTDetail(int EVENT_ID, string Token)
       {
           Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_ID", EVENT_ID);  
                dictLogin.Add("@token", Token);
                dictLogin.Add("@flag", 1);
            DataSet ds=new DataSet();
            ds= await AppDBCalls.GetDataSet("EVOTE_UpdateEVENT_DETAIL", dictLogin);
            return Reformatter.Validate_Dataset(ds);
        }

        public async Task<DataSet> GetCompanyEVENTDetail(int EVENT_ID, string Token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@EVENT_ID", EVENT_ID);
            dictLogin.Add("@token", Token);
            dictLogin.Add("@flag", 1);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("EVOTE_companyEVENT_DETAIL", dictLogin);
            return Reformatter.Validate_Dataset(ds);
        }

        
    }
}