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

            if(fjc_FileUpload.files.FileName.Length>0)
            { 
               string User_Type="";
               string TokenRowID="";
               string SaveToFolder="";
               string filenamewithdatetime="";
                DataTable  dt1=new DataTable();
                dt1 =  await GetUserDetailsByTokenID(Token);                                   
                //-start-create folder directory-here
                        var getpath =(object)null;                   
                       User_Type= dt1.Rows[0]["USER_TYPE"].ToString();
                       TokenRowID= dt1.Rows[0]["ROWID"].ToString();                    
                        switch(Convert.ToUInt32(User_Type))
                            {
                                case 1:
                                getpath = FolderPaths.Company.AgreementUpload(); 
                                break;
                                case 2:
                                getpath = FolderPaths.RTA.AgreementUpload(); 
                                break;                               
                                case 5:
                                getpath = FolderPaths.EvotingAgency.AgreementUpload(); 
                                break;
                            }                  
                   //File name with time stamp and Event no 
                    filenamewithdatetime=System.DateTime.Now.ToString("yyyyMMdd-hh-mm-ss-fff-")+ fjc_FileUpload.Event_No + "-" + fjc_FileUpload.files.FileName;
                   //Return Full file path to save to database
                    SaveToFolder=FolderPaths.CreateSpecificFolder(getpath.ToString(),filenamewithdatetime.ToString(),fjc_FileUpload); 

                //-end-create folder directory- 
                
                //Saving file details to Database               
                if(SaveToFolder!=null)
                {
                    Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();
                    dictfileUpld.Add("@DOC_NO", 0);
                    dictfileUpld.Add("@File_Name",filenamewithdatetime );
                    dictfileUpld.Add("@File_Path", SaveToFolder);  
                    dictfileUpld.Add("@UploadedBy", Convert.ToInt32(TokenRowID));  
                    dictfileUpld.Add("@token", Token); 
            
                    DataSet ds = new DataSet();
                    ds = await AppDBCalls.GetDataSet("Evote_spFileUpload", dictfileUpld);                    
                    return Reformatter.Validate_DataTable(ds.Tables[0]);
                }
                else
                {
                     return null;    
                }                
            }
            else
            {
              return null;  
            }
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
 