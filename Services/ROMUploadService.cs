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
       
        Task<DataTable> ROMUpload_Details(FJC_FileUpload fjc_FileUpload,string Token);
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
        public async Task<DataTable> ROMUpload_Details(FJC_FileUpload fjc_FileUpload,string Token)
        {
            Utility.ManageFileUpload _obj = new ManageFileUpload();
            return await _obj.SaveFile_FromToken(fjc_FileUpload, Token, FolderPaths.UploadType.ROM);
        }  
  
//////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
         public async void InsertBulkFileUpload(int Event_No,string FullPath)
        {
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@FILEPATH", FullPath);     
                dictUserDetail.Add("@GENERATEDEVENTNO", Event_No);           
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("SP_IMPORTTEXTFILE", dictUserDetail);                              
             
        }      
        
        
    }
}
 