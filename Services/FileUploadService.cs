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
           
                Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();
                dictfileUpld.Add("@DOC_NO", fjc_FileUpload.DOC_NO);
                dictfileUpld.Add("@File_Name", fjc_FileUpload.File_Name);
                dictfileUpld.Add("@File_Path", fjc_FileUpload.File_Path);  
                dictfileUpld.Add("@ROWID", fjc_FileUpload.ROWID);  
                dictfileUpld.Add("@Token_No", fjc_FileUpload.Token_No);  
                dictfileUpld.Add("@Token_No", fjc_FileUpload.Token_No);  
                dictfileUpld.Add("@MODIFIEDBY", fjc_FileUpload.MODIFIEDBY);                
   
          
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
                    else if (ds.Tables[0].Rows[0][0].ToString() == "Invalid Password")
                    {
                        throw new CustomException.InvalidPassword ();
                    }                   
                    else
                    {
                        return null;
                    }
                } 
        }       
        
        
    }
}
 