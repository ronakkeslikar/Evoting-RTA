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
          Task<DataTable> GenerateEVENT(FJC_GenerateEVENT fJC_EVSN);
          Task<DataTable> UpdateGenerateEVENT(Int32 EVENT_ID);
          Task<DataTable> DeleteGenerateEVENT(Int32 EVENT_ID);
         

        Task<DataTable> EVENTDetail(FJC_UpdateEVENT fJC_EVENT) ; 
        Task<DataTable> UpdateEVENTDetail(int EVENT_DETAIL_ID) ; 
        Task<DataTable> DeleteEVENTDetail(int EVENT_DETAIL_ID) ; 
         
         

    } 

        public class GenerateEVENTService : IGenerateEVENTService
    {
        //db context here
        protected readonly AppDbContext _context;
        public GenerateEVENTService(AppDbContext context)
        {
            _context = context;
        } 
        ///////////////////////////////////////GenerateEVENT///////////////////////////////////////////////////////////
         public async Task<DataTable> GenerateEVENT(FJC_GenerateEVENT fJC_EVSN)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", fJC_EVSN.ISIN);               
                dictLogin.Add("@TYPE_ISIN", fJC_EVSN.TYPE_ISIN);               
                dictLogin.Add("@TYPE_EVOTING", fJC_EVSN.TYPE_EVOTING);
                dictLogin.Add("@TOTAL_NOF_SHARE", fJC_EVSN.TOTAL_NOF_SHARE);               
                dictLogin.Add("@VOTING_RIGHTS", fJC_EVSN.VOTING_RIGHTS);
                dictLogin.Add("@CUT_OF_DATE", fJC_EVSN.CUT_OF_DATE);               
                dictLogin.Add("@SELECT_SCRUTINIZER", fJC_EVSN.SELECT_SCRUTINIZER);
                // dictLogin.Add("@TokenId", TokenId);
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_LoginSession_Detai=awals", dictLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
          public async Task<DataTable> UpdateGenerateEVENT(int EVENT_ID)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_ID",  EVENT_ID);               
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public async Task<DataTable> DeleteGenerateEVENT(int EVENT_ID)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_ID",  EVENT_ID);               
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_GenerateDelete_Event", dictLogin);
                return ds.Tables[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
///////////////////////////////////////UpdateEVENT////////////////////////////////////////////////////////////////
       public async Task<DataTable> EVENTDetail(FJC_UpdateEVENT fJC_EVENT)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", fJC_EVENT.ISIN);               
                dictLogin.Add("@TYPE_ISIN", fJC_EVENT.TYPE_ISIN);               
                dictLogin.Add("@TYPE_EVOTING", fJC_EVENT.TYPE_EVOTING);
                dictLogin.Add("@TOTAL_NOF_SHARE", fJC_EVENT.TOTAL_NOF_SHARE);               
                dictLogin.Add("@VOTING_RIGHTS", fJC_EVENT.VOTING_RIGHTS); 
                dictLogin.Add("@CUT_OF_DATE", fJC_EVENT.CUT_OF_DATE);              
                dictLogin.Add("@SELECT_SCRUTINIZER", fJC_EVENT.SELECT_SCRUTINIZER);
                dictLogin.Add("@VOTING_START_DATETIME", fJC_EVENT.VOTING_START_DATETIME);               
                dictLogin.Add("@VOTING_END_DATETIME", fJC_EVENT.VOTING_END_DATETIME);               
                dictLogin.Add("@MEETING_DATETIME", fJC_EVENT.MEETING_DATETIME);
                dictLogin.Add("@LAST_DATE_NOTICE", fJC_EVENT.LAST_DATE_NOTICE);               
                dictLogin.Add("@VOTING_RESULT_DATE", fJC_EVENT.VOTING_RESULT_DATE); 
                dictLogin.Add("@UPLOAD_LOGO", fJC_EVENT.UPLOAD_LOGO); //file             
                dictLogin.Add("@UPLOAD_RESOLUTION_FILE", fJC_EVENT.UPLOAD_RESOLUTION_FILE);//file
                dictLogin.Add("@UPLOAD_NOTICE", fJC_EVENT.UPLOAD_NOTICE); //file
                // dictLogin.Add("@ENTER_NOF_RESOLUTION", fJC_EVENT.ENTER_NOF_RESOLUTION);  //decimal           
                // dictLogin.Add("@TITAL", fJC_EVENT.TITAL);//text
                // dictLogin.Add("@DESCRIPTION", fJC_EVENT.DESCRIPTION);     //text          
                // dictLogin.Add("@FILEUPLOAD", fJC_EVENT.FILEUPLOAD); //file
                // dictLogin.Add("@TokenId", TokenId);
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_LoginSession_Detai=awals", dictLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
         
          public async Task<DataTable> UpdateEVENTDetail(int EVENT_DETAIL_ID)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", EVENT_DETAIL_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_LoginSession_Detai=awals", dictLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
         public async Task<DataTable> DeleteEVENTDetail(int EVENT_DETAIL_ID)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", EVENT_DETAIL_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
                //return await AppDBCalls.GetDataSet("Evote_LoginSession_Detai=awals", dictLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}