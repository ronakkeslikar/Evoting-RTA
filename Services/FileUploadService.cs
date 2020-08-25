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
    public interface IFileUploadService
    {     
        Task<DataTable> FileUpload_Details(FJC_FileUpload fjc_FileUpload);
        
    }

    public class FileUploadService : IFileUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public FileUploadService(AppDbContext context)
        {
            _context = context;
        }  

        public async Task<DataTable> FileUpload_Details(FJC_FileUpload fjc_FileUpload)
        {
             
                var getpath = FolderPaths.RTA.ROMUpload(); // date logic - combine
                var name = fjc_FileUpload.files;

                // Saving file on Server
                if (name.Length > 0) 
                {
                    var folderName = Path.Combine("ROM", "RTA");           
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(pathToSave))  
                    {                 
                        Directory.CreateDirectory(pathToSave);
                    } 
                    //Path.Combine(folderName,System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")+ fjc_FileUpload.Token_No + name.FileName);
                    using (var fileStream = new FileStream(name.FileName, FileMode.Create)) 
                    {
                    name.CopyTo(fileStream);
                    }
                }
           
                Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();
                dictfileUpld.Add("@DOC_NO", fjc_FileUpload.DOC_NO);
                dictfileUpld.Add("@File_Name", System.DateTime.Now.ToString("yyyyMMdd-hh:mm:ss:fff-")+ fjc_FileUpload.Token_No.Replace("\"","") + name.FileName);
                dictfileUpld.Add("@File_Path", fjc_FileUpload.File_Path);  
                dictfileUpld.Add("@UploadedBy", fjc_FileUpload.UploadedBy);  
                dictfileUpld.Add("@Token_No", fjc_FileUpload.Token_No); 
          
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_spFileUpload", dictfileUpld);
                 if(!ds.Tables[0].Columns.Contains("Error"))
                {
                    return ds.Tables[0]; 
                }
                else
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "Invalid User ID")
                    {
                        throw new CustomException.InvalidUserID ();
                    }              
                    else
                    {
                        return null;
                    }
                } 
        }       
        
        
    }
}
 