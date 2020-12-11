using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;
using evoting.Domain.Models.Validate;
using evoting.Utility;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;

namespace evoting.Services
{
    public interface ICustodianROMUploadService
    {  
       
        Task<DataTable> Cutodian_ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token);
           Task<DataTable> GetCustodian_ROMUpload_Details(string Token);
        Task<DataTable> Submit_Vote(FJC_Event_Update _event, string Token);
        Task<DataTable> Modify_Vote(FJC_Event_Update _event, string Token);


    }

    public class CustodianROMUploadService : ICustodianROMUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public CustodianROMUploadService(AppDbContext context)
        {
            _context = context;
        }  

        public async Task<DataTable> Submit_Vote(FJC_Event_Update _event, string Token)
        {
            DataTable dt1 = new DataTable();

            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@event_id", _event.event_id);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@Flag", 0);

            DataSet ds = await AppDBCalls.GetDataSet("Custodian_VoteUpdate", dictUserDetail);
            return Reformatter.Validate_DataTable(ds.Tables[0]);            
        }
        public async Task<DataTable> Modify_Vote(FJC_Event_Update _event, string Token)
        {
            DataTable dt1 = new DataTable();

            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@event_id", _event.event_id);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@Flag", 1);

            DataSet ds = await AppDBCalls.GetDataSet("Custodian_VoteUpdate", dictUserDetail);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        //////////////////////////////////////////ROM File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> Cutodian_ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token)
        {
            //validation job            
            //Here Validate bulk file
            DataTable dt1 =new DataTable();
            DataTable dt2 =new DataTable();
            dt1= await RegisterCustodianROM(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,0)  ;
            ManageErrorUploads dd=new ManageErrorUploads();
            dt2=dd.Manage_ROM_ErrorUploads(dt1, FolderPaths.ProcessType.Custodian_ROMUpload,fjc_ROMUpload.doc_id,fjc_ROMUpload.event_id,Token);

            return dt2;
        }
        /// <summary>
        /// ////////////////////////////Check Custodian Vote file and return Error-Remark //////////////////////
        /// </summary>
        /// <param name="doc_id"></param>
        /// <param name="event_id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        
        

        //////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
        private async Task<DataTable> InsertBulkFileUpload(int Event_No,int DocID, string Token)
        {         
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@DocumentID", DocID);                
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@Flag", 0);
                
            DataSet ds=  await AppDBCalls.GetDataSet("Sp_ValidateAndInsert_CustodianROM", dictUserDetail);
           return Reformatter.Validate_DataTable(ds.Tables[0]);   
                
        }   

        private async Task<DataTable> RegisterCustodianROM(int Event_No,int DocID, string Token,int flag)   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@doc_id", DocID);   
                dictUserDetail.Add("@event_id", Event_No);
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@flag", flag);

            DataSet ds=  await AppDBCalls.GetDataSet("Evote_Sp_Cust_ROM_Register", dictUserDetail);
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        /////////////////////Get//////////////////
        public async Task<DataTable> GetCustodian_ROMUpload_Details(string Token)
        {
           return await RegisterCustodianROM(0,0,Token,1);
        } 

    }
}
 