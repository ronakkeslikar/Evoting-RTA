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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace evoting.Services
{
    public interface IROMUploadService
    {  
       
        Task<DataTable> ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token);
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
                        
            return await InsertBulkFileUpload(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token);
        }  
  
//////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
         private async Task<DataTable> InsertBulkFileUpload(int Event_No,int DocID, string Token)
        {
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@DocumentID", DocID);   
                dictUserDetail.Add("@GENERATEDEVENTNO", Event_No);
            dictUserDetail.Add("@token", Token);
            DataSet ds=  await AppDBCalls.GetDataSet("SP_IMPORTTEXTFILE", dictUserDetail);
           return Reformatter.Validate_DataTable(ds.Tables[0]);   
                
        }      
        
        
    }
}
 