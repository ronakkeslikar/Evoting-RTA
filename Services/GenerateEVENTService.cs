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
          Task<DataTable> UpdateGenerateEVENT(FJC_GenerateEVENT fJC_EVSN);
          Task<DataTable> DeleteGenerateEVENT(Int32 EVENT_ID);
          Task<DataTable> GeteGenerateEVENT(Int32 EVENT_ID);

         Task<DataTable> EVENTDetail(FJC_UpdateEVENT fJC_EVENT) ; 
         Task<DataTable> UpdateEVENTDetail(FJC_UpdateEVENT fJC_EVENT) ; 
         Task<DataTable> DeleteEVENTDetail(int EVENT_DETAIL_ID) ; 
         Task<DataTable> GetEVENTDetail(int EVENT_DETAIL_ID) ; 
        
         Task<DataTable> EVENTResolution(FJC_EVENT_Resolution FJC_EVENTRESOLUTION) ; 
         Task<DataTable> UpdateEVENTResolution(FJC_EVENT_Resolution FJC_EVENTRESOLUTION) ; 
         Task<DataTable> DeleteEVENTResolution(int EVENT_RESOLUTION_ID) ; 
         Task<DataTable> GetEVENTResolution(int EVENT_RESOLUTION_ID) ; 
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
         public async Task<DataTable> GenerateEVENT(FJC_GenerateEVENT fJC_EVSN)
        {
            
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", fJC_EVSN.ISIN);
                dictLogin.Add("@ROWID_CLIENT", fJC_EVSN.ROWID_CLIENT);                
                dictLogin.Add("@TYPE_ISIN", fJC_EVSN.TYPE_ISIN);               
                dictLogin.Add("@TYPE_EVOTING", fJC_EVSN.TYPE_EVOTING);
                dictLogin.Add("@TOTAL_NOF_SHARE", fJC_EVSN.TOTAL_NOF_SHARE);               
                dictLogin.Add("@VOTING_RIGHTS", fJC_EVSN.VOTING_RIGHTS);
                dictLogin.Add("@CUT_OF_DATE", new DateTime(2012, 12, 25, 10, 30, 50).ToString("yyyy-MM-dd HH:mm:ss"));               
                dictLogin.Add("@SCRUTINIZER", fJC_EVSN.SCRUTINIZER);
                dictLogin.Add("@CREATED_BY", fJC_EVSN.CREATED_BY);   
                dictLogin.Add("@UPDATED_BY", fJC_EVSN.UPDATED_BY);   
                // dictLogin.Add("@TokenId", TokenId);
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                if(!ds.Tables[0].Columns.Contains("Error"))
                {
                    return ds.Tables[0]; //it will return a Token ID from database 
                }
                else
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "Invalid Event")
                    {
                        throw new CustomException.InvalidUserID();
                    }
                    else
                    {
                        return null;
                    }
                }            
            
            
        }
         
          public async Task<DataTable> UpdateGenerateEVENT(FJC_GenerateEVENT fJC_EVSN)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@ISIN", fJC_EVSN.ISIN); 
                dictLogin.Add("@ROWID_CLIENT", fJC_EVSN.ROWID_CLIENT);             
                dictLogin.Add("@TYPE_ISIN", fJC_EVSN.TYPE_ISIN);               
                dictLogin.Add("@TYPE_EVOTING", fJC_EVSN.TYPE_EVOTING);
                dictLogin.Add("@TOTAL_NOF_SHARE", fJC_EVSN.TOTAL_NOF_SHARE);               
                dictLogin.Add("@VOTING_RIGHTS", fJC_EVSN.VOTING_RIGHTS);
                dictLogin.Add("@CUT_OF_DATE",fJC_EVSN.CUT_OF_DATE );               
                dictLogin.Add("@SCRUTINIZER", fJC_EVSN.SCRUTINIZER);
                dictLogin.Add("@CREATED_BY", fJC_EVSN.CREATED_BY);   
                dictLogin.Add("@UPDATED_BY", fJC_EVSN.UPDATED_BY);            
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

       
            public async Task<DataTable> GeteGenerateEVENT(int EVENT_ID)
        {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_ID",  EVENT_ID);               
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_GenerateGet_Event", dictLogin);
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
                dictLogin.Add("@EVENT_DETAIL_ID", fJC_EVENT.EVENT_DETAIL_ID);               
                //dictLogin.Add("@SELECT_SCRUTINIZER", fJC_EVENT.SELECT_SCRUTINIZER);
                dictLogin.Add("@VOTING_START_DATETIME", fJC_EVENT.VOTING_START_DATETIME);               
                dictLogin.Add("@VOTING_END_DATETIME", fJC_EVENT.VOTING_END_DATETIME);               
                dictLogin.Add("@MEETING_DATETIME", fJC_EVENT.MEETING_DATETIME);
                dictLogin.Add("@LAST_DATE_NOTICE", fJC_EVENT.LAST_DATE_NOTICE);               
                dictLogin.Add("@VOTING_RESULT_DATE", fJC_EVENT.VOTING_RESULT_DATE); 
                dictLogin.Add("@UPLOAD_LOGO", fJC_EVENT.UPLOAD_LOGO); //file             
                dictLogin.Add("@UPLOAD_RESOLUTION_FILE", fJC_EVENT.UPLOAD_RESOLUTION_FILE);//file
                dictLogin.Add("@UPLOAD_NOTICE", fJC_EVENT.UPLOAD_NOTICE); //file
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
         
          public async Task<DataTable> UpdateEVENTDetail(FJC_UpdateEVENT fJC_EVENT)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_DETAIL_ID", fJC_EVENT.EVENT_DETAIL_ID);               
               // dictLogin.Add("@SELECT_SCRUTINIZER", fJC_EVENT.SELECT_SCRUTINIZER);
                dictLogin.Add("@VOTING_START_DATETIME", fJC_EVENT.VOTING_START_DATETIME);               
                dictLogin.Add("@VOTING_END_DATETIME", fJC_EVENT.VOTING_END_DATETIME);               
                dictLogin.Add("@MEETING_DATETIME", fJC_EVENT.MEETING_DATETIME);
                dictLogin.Add("@LAST_DATE_NOTICE", fJC_EVENT.LAST_DATE_NOTICE);               
                dictLogin.Add("@VOTING_RESULT_DATE", fJC_EVENT.VOTING_RESULT_DATE); 
                dictLogin.Add("@UPLOAD_LOGO", fJC_EVENT.UPLOAD_LOGO); //file             
                dictLogin.Add("@UPLOAD_RESOLUTION_FILE", fJC_EVENT.UPLOAD_RESOLUTION_FILE);//file
                dictLogin.Add("@UPLOAD_NOTICE", fJC_EVENT.UPLOAD_NOTICE); //file              
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Generate_Event", dictLogin);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
            public async Task<DataTable> GetEVENTDetail(int EVENT_DETAIL_ID)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_DETAIL_ID", EVENT_DETAIL_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Get_EVENT_DETAIL", dictLogin);
                return ds.Tables[0];
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
                dictLogin.Add("@EVENT_DETAIL_ID", EVENT_DETAIL_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Delete_EVENT_DETAIL", dictLogin);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ////////////////////////////////////////////EVENT RESOLUTION//////////////////////////////////////////////////////
         public async Task<DataTable> EVENTResolution(FJC_EVENT_Resolution FJC_EVENTRESOLUTION)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_RESOLUTION_ID", FJC_EVENTRESOLUTION.EVENT_RESOLUTION_ID);               
                dictLogin.Add("@ROW_NO", FJC_EVENTRESOLUTION.ROW_NO);
                dictLogin.Add("@EVENT_NO", FJC_EVENTRESOLUTION.EVENT_NO);               
                dictLogin.Add("@TITLE", FJC_EVENTRESOLUTION.TITLE);               
                dictLogin.Add("@DESCRIPTION", FJC_EVENTRESOLUTION.DESCRIPTION);
                dictLogin.Add("@FILE_PATH", FJC_EVENTRESOLUTION.FILE_PATH);      
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Event_Resolution", dictLogin);
                return ds.Tables[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
         
          public async Task<DataTable> UpdateEVENTResolution(FJC_EVENT_Resolution FJC_EVENTRESOLUTION)
       {
            try
            {   
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_RESOLUTION_ID", FJC_EVENTRESOLUTION.EVENT_RESOLUTION_ID);               
                dictLogin.Add("@ROW_NO", FJC_EVENTRESOLUTION.ROW_NO);
                dictLogin.Add("@EVENT_NO", FJC_EVENTRESOLUTION.EVENT_NO);               
                dictLogin.Add("@TITLE", FJC_EVENTRESOLUTION.TITLE);               
                dictLogin.Add("@DESCRIPTION", FJC_EVENTRESOLUTION.DESCRIPTION);
                dictLogin.Add("@FILE_PATH", FJC_EVENTRESOLUTION.FILE_PATH);               
                     
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Event_Resolution", dictLogin);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
            public async Task<DataTable> DeleteEVENTResolution(int EVENT_RESOLUTION_ID)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_RESOLUTION_ID", EVENT_RESOLUTION_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Delete_EVENT_Resolution", dictLogin);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         public async Task<DataTable> GetEVENTResolution(int EVENT_RESOLUTION_ID)
       {
            try
            {
                Dictionary<string, object> dictLogin = new Dictionary<string, object>(); 
                dictLogin.Add("@EVENT_RESOLUTION_ID", EVENT_RESOLUTION_ID);               
             
                DataSet ds=new DataSet();
                ds= await AppDBCalls.GetDataSet("Evote_Get_EVENT_Resolution", dictLogin);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         





    }
}