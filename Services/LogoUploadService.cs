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
      public interface ILogoUploadService
    {     
        Task<DataTable> LogoUpload_Details(FJC_FileUpload fjc_FileUpload,string Token);       
        
    }

    public class LogoUploadService : ILogoUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public LogoUploadService(AppDbContext context)
        {
            _context = context;
        }  


          public async Task<DataTable> LogoUpload_Details(FJC_FileUpload fjc_FileUpload,string Token)
        {
           
             if(fjc_FileUpload.files.FileName.Length>0)
            { 
               string User_Type="";
               string TokenRowID="";
               string SaveToFolder="";
               string logonamewithdatetime="";
                DataTable  dt1=new DataTable();
                dt1 =  await GetUserDetailsByTokenID(Token);                  
                    var getpath =(object)null;                   
                    User_Type= dt1.Rows[0]["USER_TYPE"].ToString();
                    TokenRowID= dt1.Rows[0]["ROWID"].ToString();
                   //User type wise Folder path selected
                            switch(Convert.ToUInt32(User_Type))  
                            {
                                case 1:
                                getpath = FolderPaths.Company.LogoUpload(); 
                                break;
                                case 2:
                                getpath = FolderPaths.RTA.LogoUpload(); 
                                break;
                                case 3:
                                 getpath = FolderPaths.Scrutinizer.LogoUpload(); 
                                break;
                                case 4:
                                 getpath = FolderPaths.Custodian.LogoUpload(); 
                                 break;
                                case 5:
                                getpath = FolderPaths.EvotingAgency.LogoUpload(); 
                                break;
                            }
                                       
                   
                    logonamewithdatetime=System.DateTime.Now.ToString("yyyyMMdd-hh-mm-ss-fff-")+ fjc_FileUpload.Event_No + "-" + fjc_FileUpload.files.FileName;
                    SaveToFolder=FolderPaths.CreateSpecificFolder(getpath.ToString(),logonamewithdatetime.ToString(),fjc_FileUpload);

                
                     //////////////////////Start to save database //////////////////////              
                 if(SaveToFolder!=null)
                {

                    Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();
                    dictfileUpld.Add("@DOC_NO", 0);
                    dictfileUpld.Add("@Logo_Name",logonamewithdatetime );
                    dictfileUpld.Add("@Logo_Path", SaveToFolder);  
                    dictfileUpld.Add("@UploadedBy", Convert.ToInt32(TokenRowID));  
                    dictfileUpld.Add("@token", Token); 
                    DataSet ds = new DataSet();       
                    ds = await AppDBCalls.GetDataSet("Evote_spLogoUpload", dictfileUpld);
                    return Reformatter.Validate_DataTable(ds.Tables[0]); 
                }
                else
                {
                     return null;    
                }
                 //////////////////////Complete to save database //////////////////////
            }
            else
            {
                return null;
            }


        }
         public async Task<DataTable> GetUserDetailsByTokenID(string Token)
        {
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@TokenID", Token);               
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetLoginDetails", dictUserDetail);                              
            return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }       

    }     

  
}       