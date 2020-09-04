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
        Task<DataTable> FileUpload_Details(FJC_FileUpload fjc_FileUpload,string Token);        
    }

    public class FileUploadService : IFileUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public FileUploadService(AppDbContext context)
        {
            _context = context;
        }  
//////////////////////////////////////////Agreement File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> FileUpload_Details(FJC_FileUpload fjc_FileUpload,string Token)
        {
            //-Start-Agreement HTML logic
                string Agreement_HtmlContent="";
                string DocumentType="";
                DataTable  dt2=new DataTable();
                dt2 =await GetAgreementHtmlContent(fjc_FileUpload.Event_No);
                Agreement_HtmlContent= dt2.Rows[0]["Content"].ToString();
                DocumentType= dt2.Rows[0]["DocumentType"].ToString();

            //-END-
            return await (new Utility.ManageFileUpload()).SaveFile_FromToken(fjc_FileUpload, Token, FolderPaths.UploadType.Agreement);
        } 

//////////////////////////////////////////Get User Login Detail using Token  ////////////////////////////////////////////////////
         public async Task<DataTable> GetUserDetailsByTokenID(string TokenID)
        {
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@TokenID", TokenID); 

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetLoginDetails", dictUserDetail);                              
                return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }   
//////////////////////////////////////////Get User Agreement Pdf from stored procedure to Upload  ////////////////////////////////////////////////////
         public async Task<DataTable> GetAgreementHtmlContent(int Event_No)
        { 
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@ID", 1); 
                dictUserDetail.Add("@CLIENT_NAME", "Lenovo"); 
                dictUserDetail.Add("@CLIENT_ADDRESS", "Mumbai"); 
                dictUserDetail.Add("@EVENT_NO", Event_No);  

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("SP_GETDOCUMENTCONTENT", dictUserDetail);                              
                return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }        
    }
}
 