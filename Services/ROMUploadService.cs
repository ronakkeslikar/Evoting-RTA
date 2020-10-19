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

namespace evoting.Services
{
    public interface IROMUploadService
    {  
       
        Task<DataTable> ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token);
           Task<DataTable> GetROMUpload_Details(string Token);
    }

    public class ROMUploadService : IROMUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public ROMUploadService(AppDbContext context)
        {
            _context = context;
        }  

//////////////////////////////////////////ROM File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token)
        {
            //validation job
           
               
                //Here Validate bulk file
                DataTable dt1=new DataTable();
                dt1= await RegisterROM(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,0)  ; // GetROMUpload_Details(Token);
                if(dt1.Rows.Count>0)
                { Validate_ROM val_ROM=new Validate_ROM();
                    bool isValid= val_ROM.Validate_File(dt1.Rows[0]["url"].ToString(),Token); //("C:\\evoting\\RTA\\ROM\\2020-09-18\\20200918-112617505-CDSL_FileUpload_128_ROM_ANUH_PHARMA_LTD_13220201.txt");//(dt1.Rows[0]["url"].ToString()); 
                    if(isValid)
                    {
                        return await InsertBulkFileUpload(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token);
                    } 
                    else
                    {
                        throw new CustomException.InvalidFileRejected(); 
                    }
                }
                else
                {
                     throw new CustomException.InvalidFileNotUploaded(); 
                      
                }
             
         
           
        }  
  
//////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
         private async Task<DataTable> InsertBulkFileUpload(int Event_No,int DocID, string Token)
        {         
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@DocumentID", DocID);   
                dictUserDetail.Add("@event_no", Event_No);
                dictUserDetail.Add("@token", Token);
            DataSet ds=  await AppDBCalls.GetDataSet("SP_IMPORTROMFILE", dictUserDetail);
           return Reformatter.Validate_DataTable(ds.Tables[0]);   
                
        }   

        private async Task<DataTable> RegisterROM(int Event_No,int DocID, string Token,int flag)   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@doc_id", DocID);   
                dictUserDetail.Add("@event_id", Event_No);
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@flag", flag);

            DataSet ds=  await AppDBCalls.GetDataSet("Evote_Sp_ROM_Register", dictUserDetail);
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        ///////////////////////Get//////////////////
        public async Task<DataTable> GetROMUpload_Details(string Token)
        {
           return await RegisterROM(0,0,Token,1);            
        }
        
        
    }
}
 