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
            if(fjc_FileUpload.files.FileName.Length>0)
            { 
               int User_Type=0;
               int TokenRowID=0;
               string SaveToFolder="";
               string filenamewithdatetime="";
                DataTable  dt1=new DataTable();
                dt1 =  await GetUserDetailsByTokenID(fjc_FileUpload.Token_ID);
                 if(!dt1.Columns.Contains("Error"))
                {                    
                   //-start-create folder directory-here
                        var getpath =(object)null;                   
                        
                    if(fjc_FileUpload.Process_Type=="ROM")  
                    {
                            switch(User_Type)
                            {
                                case 1:
                                getpath = FolderPaths.Company.ROMUpload(); 
                                break;
                                case 2:
                                getpath = FolderPaths.RTA.ROMUpload(); 
                                break;
                                case 3:
                                getpath = FolderPaths.Scrutinizer.ROMUpload(); 
                                break;
                                case 4:
                                getpath = FolderPaths.Custodian.ROMUpload(); 
                                break;
                                case 5:
                                getpath = FolderPaths.EvotingAgency.ROMUpload(); 
                                break;
                            }
                    }
                    else
                    {
                        switch(User_Type)
                            {
                                case 1:
                                getpath = FolderPaths.Company.AgreementUpload(); 
                                break;
                                case 2:
                                getpath = FolderPaths.RTA.AgreementUpload(); 
                                break;
                                case 3:
                                getpath = FolderPaths.Scrutinizer.AgreementUpload(); 
                                break;
                                case 4:
                                getpath = FolderPaths.Custodian.AgreementUpload(); 
                                break;
                                case 5:
                                getpath = FolderPaths.EvotingAgency.AgreementUpload(); 
                                break;
                            }
                    }
                   
                    filenamewithdatetime=System.DateTime.Now.ToString("yyyyMMdd-hh-mm-ss-fff-")+ fjc_FileUpload.Event_No + "-" + fjc_FileUpload.files.FileName;
                    SaveToFolder=FolderPaths.CreateSpecificFolder(getpath.ToString(),filenamewithdatetime.ToString(),fjc_FileUpload); // date logic - combine

                        //-end-create folder directory-
                }
                else{
                    if (dt1.Rows[0][0].ToString() == "Invalid Token ID")
                        {
                            throw new CustomException.InvalidTokenID ();
                        } 
                    else
                        {
                        return null;
                        }                        
                    }                

                                  
                //var getpath = FolderPaths.RTA.ROMUpload(); // date logic - combine
                
                // // Saving file on Server               
                if(SaveToFolder=="Done")
                {

                    Dictionary<string, object> dictfileUpld = new Dictionary<string, object>();
                    dictfileUpld.Add("@DOC_NO", 0);
                    dictfileUpld.Add("@File_Name",filenamewithdatetime );
                    dictfileUpld.Add("@File_Path", fjc_FileUpload.File_Path);  
                    dictfileUpld.Add("@UploadedBy", TokenRowID);  
                    dictfileUpld.Add("@Token_No", fjc_FileUpload.Token_ID); 
            
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
                      else if (ds.Tables[0].Rows[0][0].ToString() == "Invalid Token ID")
                        {
                            throw new CustomException.InvalidTokenID ();
                        }            
                        else
                        {
                            return null;
                        }
                    } 
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

         public async Task<DataTable> GetUserDetailsByTokenID(string TokenID)
        {
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@TokenID", TokenID);               
                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_GetLoginDetails", dictUserDetail);                              
            if(!ds.Tables[0].Columns.Contains("Error"))
                {
                    return ds.Tables[0];  
                }
                else
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "Invalid Token ID")
                    {
                        throw new CustomException.InvalidTokenID();
                    }
                    else
                    {
                        return null;
                    }
                } 
        }       
        
        
    }
}
 