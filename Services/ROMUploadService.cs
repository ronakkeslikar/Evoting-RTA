using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;
using evoting.Domain.Models.Validate;
using evoting.Utility;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace evoting.Services
{
    public interface IROMUploadService
    {  
       
        Task<DataTable> ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token);
           Task<DataTable> GetROMUpload_Details(string Token);
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
            DataTable dtUserType = new DataTable();
            dtUserType = await (new ManageFileUpload()).GetUserDetailsByTokenID(Token);


            switch (fjc_ROMUpload.upload_type.ToLower())
                {
                //////////////////////////////ROM Intimation////////////////////////
                    case "intimation":                          
                        DataTable dt1=new DataTable();
                        dt1= await RegisterROM_Intimation(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,0)  ; // GetROMUpload_Details(Token);
                        if(dt1.Rows.Count>0)
                        { Validate_ROM val_ROM=new Validate_ROM();
                            bool isValid= val_ROM.Validate_File(dt1.Rows[0]["url"].ToString(),Token,fjc_ROMUpload.event_id); 
                            if(isValid)
                            {
                            // return await BulkUpload_ROM_Intimation(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,dt1.Rows[0]["upload_id"].ToString());
                            
                            DataTable dt = new DataTable();

                            dt = BulkUpload_ROM_Intimation(fjc_ROMUpload.event_id, fjc_ROMUpload.doc_id, Token, dt1.Rows[0]["upload_id"].ToString());
                            //return await InsertBulkFileUpload(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,dt2.Rows[0]["upload_id"].ToString());
                            if (dt.Columns.Contains("Error_Num"))
                            {
                                ManageErrorUploads _err = new ManageErrorUploads();
                                DataTable dtSecROMErr = new DataTable();

                                //Below here return error datatble for second ROM upload file
                                switch (dtUserType.Rows[0]["type"])
                                {
                                    case "Issuer Company":
                                        dtSecROMErr = _err.Manage_ROM_ErrorUploads(dt, FolderPaths.ProcessType.Company_ROMUpload, 0, fjc_ROMUpload.event_id, Token);
                                        break;
                                    case "RTA":
                                        dtSecROMErr = _err.Manage_ROM_ErrorUploads(dt, FolderPaths.ProcessType.RTA_ROMUpload, 0, fjc_ROMUpload.event_id, Token);
                                        break;
                                }
                                if (dtSecROMErr.Rows.Count > 0)
                                {
                                    DataTable dtget = new DataTable();
                                    //Below here Register ROM for Error file for second ROM upload file
                                    return dtget = await RegisterROM_Intimation(fjc_ROMUpload.event_id, Convert.ToInt32(dtSecROMErr.Rows[0]["doc_id"]), Token, 0, "SecondROM");
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                DataSet ds = new DataSet();
                                DataTable _dt = new DataTable();
                                _dt.TableName = "Table2";
                                _dt.Columns.Add("Remark");
                                ds.Tables.Add(_dt);
                                _dt.Rows.Add("Process Completed");
                                return ds.Tables[0];
                            }
                        } 
                            else
                            {
                                return null;// throw new CustomException.InvalidFileRejected(); 
                            }
                        }
                        else
                        {
                                return null;//throw new CustomException.InvalidFileNotUploaded(); 
                        }
                        break;
                //////////////////Normal ROM Upload ////////////////////////
                    case "notice": 
                        //Here Validate bulk file
                        DataTable dt2=new DataTable();
                        dt2= await RegisterROM(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,0)  ; // GetROMUpload_Details(Token);
                        if(dt2.Rows.Count>0)
                        { Validate_ROM val_ROM=new Validate_ROM();
                            bool isValid= val_ROM.Validate_File(dt2.Rows[0]["url"].ToString(),Token,fjc_ROMUpload.event_id); //("C:\\evoting\\RTA\\ROM\\2020-09-18\\20200918-112617505-CDSL_FileUpload_128_ROM_ANUH_PHARMA_LTD_13220201.txt");//(dt1.Rows[0]["url"].ToString()); 
                            if(isValid)
                            {
                                //return await InsertBulkFileUpload(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,dt2.Rows[0]["upload_id"].ToString());
                                DataTable dt = new DataTable();

                                dt = InsertBulkFileUpload(fjc_ROMUpload.event_id, fjc_ROMUpload.doc_id, Token, dt2.Rows[0]["upload_id"].ToString());
                                //return await InsertBulkFileUpload(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,dt2.Rows[0]["upload_id"].ToString());
                                if (dt.Columns.Contains("Error_Num"))
                                {
                                    ManageErrorUploads _err = new ManageErrorUploads();
                                    DataTable dtSecROMErr = new DataTable();

                                    //Below here return error datatble for second ROM upload file
                                    switch (dtUserType.Rows[0]["type"])
                                    {
                                        case "Issuer Company":
                                            dtSecROMErr = _err.Manage_ROM_ErrorUploads(dt, FolderPaths.ProcessType.Company_ROMUpload, 0, fjc_ROMUpload.event_id, Token);
                                            break;
                                        case "RTA":
                                            dtSecROMErr = _err.Manage_ROM_ErrorUploads(dt, FolderPaths.ProcessType.RTA_ROMUpload, 0, fjc_ROMUpload.event_id, Token);
                                            break;
                                    }                                        
                                    if (dtSecROMErr.Rows.Count > 0)
                                    {
                                        DataTable dtget = new DataTable();
                                        //Below here Register ROM for Error file for second ROM upload file
                                        return dtget = await RegisterROM(fjc_ROMUpload.event_id, Convert.ToInt32(dtSecROMErr.Rows[0]["doc_id"]), Token, 0, "SecondROM");
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else
                                {
                                DataSet ds = new DataSet();
                                DataTable _dt = new DataTable();
                                _dt.TableName = "Table1";
                                _dt.Columns.Add("Remark");
                                _dt.Rows.Add("Process Completed");

                                ds.Tables.Add(_dt);
                                return ds.Tables[0];
                            }
                            } 
                            else
                            {
                            return null;//throw new CustomException.InvalidFileRejected(); 
                            }
                        }
                        else
                        {
                        return null;//throw new CustomException.InvalidFileNotUploaded(); 

                    }
                        break;
                          default:
                        throw new CustomException.InvalidActivity();
                }            
        }

        //////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
        private DataTable InsertBulkFileUpload(int Event_No, int DocID, string Token, string upload_id)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@DocumentID", DocID);
            dictUserDetail.Add("@event_no", Event_No);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@upload_id", upload_id);
            DataSet ds =  AppDBCalls.GetDataSet("SP_IMPORTROMFILE", dictUserDetail).Result;
            return ds.Tables[0];
        }

        private async Task<DataTable> RegisterROM(int Event_No,int DocID, string Token,int flag, string ROM_Error="")   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@doc_id", DocID);   
                dictUserDetail.Add("@event_id", Event_No);
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@flag", flag);
                dictUserDetail.Add("@ROM_Error", ROM_Error);

            DataSet ds=  await AppDBCalls.GetDataSet("Evote_Sp_ROM_Register", dictUserDetail);
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        }       

        ///////////////////////Get//////////////////
        public async Task<DataTable> GetROMUpload_Details(string Token)
        {
           return await RegisterROM(0,0,Token,1);            
        }
         //////////////////////////////////////////////////ROM Intimation////////////////////////////////////////////////
        private async Task<DataTable> RegisterROM_Intimation(int Event_No,int DocID, string Token,int flag, string ROM_Error = "")   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@doc_id", DocID);   
                dictUserDetail.Add("@event_id", Event_No);
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@flag", flag);
                dictUserDetail.Add("@ROM_Error", ROM_Error);

            DataSet ds =  await AppDBCalls.GetDataSet("Evote_Sp_ROM_Intimation_Register", dictUserDetail);
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        ///////////////////////////Bulk Upload stored Procedure called here  /////////////////////    
         private DataTable BulkUpload_ROM_Intimation(int Event_No,int DocID, string Token,string upload_id)
        {         
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@DocumentID", DocID);   
                dictUserDetail.Add("@event_no", Event_No);
                dictUserDetail.Add("@token", Token);
                 dictUserDetail.Add("@upload_id", upload_id);
          return  AppDBCalls.GetDataSet("SP_IMPORTROMFILE_ROM_Intimation", dictUserDetail).Result.Tables[0];            
                
        } 
        
        
    }
}
 