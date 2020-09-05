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
       
        Task<DataTable> FileUpload_Details(FJC_FileUpload _fjc_fileupload,string Token);
    }

    public class FileUploadService : IFileUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public FileUploadService(AppDbContext context)
        {
            _context = context;
        }  

//////////////////////////////////////////File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> FileUpload_Details(FJC_FileUpload _fjc_fileupload,string Token)
        {
            Utility.ManageFileUpload _obj = new ManageFileUpload();
            FolderPaths.UploadType myUpload;
            if(Enum.TryParse(_fjc_fileupload.upload_type, out myUpload))
            {
                 DataTable dt = new DataTable();
                dt = await _obj.SaveFile_FromToken(_fjc_fileupload, Token, myUpload);
               return Reformatter.Validate_DataTable(dt);
            }
            else{
                throw new CustomException.InvalidFileType();
            }
        }  
  
   
        
        
    }
}
 