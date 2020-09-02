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
                                getpath = FolderPaths.Company.ROMUpload(); 
                                break;
                                case 2:
                                getpath = FolderPaths.RTA.ROMUpload(); 
                                break;
                                //case 3:
                                // getpath = FolderPaths.Scrutinizer.ROMUpload(); 
                                // break;
                                // case 4:
                                // getpath = FolderPaths.Custodian.ROMUpload(); 
                                // break;
                                case 5:
                                getpath = FolderPaths.EvotingAgency.ROMUpload(); 
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
                    if (!ds.Tables[0].Columns.Contains("Error"))
                    {
                        InsertBulkFileUpload(fjc_FileUpload.Event_No,SaveToFolder);     
                    }
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
 