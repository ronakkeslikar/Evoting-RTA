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
using System.Text;

namespace evoting.Services
{
    public interface ICustodianROMUploadService
    {  
       
        Task<DataTable> Cutodian_ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token);
           Task<DataTable> GetCustodian_ROMUpload_Details(string Token);
         
    }

    public class CustodianROMUploadService : ICustodianROMUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public CustodianROMUploadService(AppDbContext context)
        {
            _context = context;
        }  

//////////////////////////////////////////ROM File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> Cutodian_ROMUpload_Details(FJC_ROMUpload fjc_ROMUpload,string Token)
        {
            //validation job            
            //Here Validate bulk file
            DataTable dt1 =new DataTable();
                dt1= await RegisterCustodianROM(fjc_ROMUpload.event_id ,fjc_ROMUpload.doc_id, Token,0)  ;
            if (dt1.Rows.Count > 0)
            {
                //Cust-ROM-start
                DataSet ds1 = new DataSet();
                ds1=await Check(fjc_ROMUpload.doc_id, fjc_ROMUpload.event_id, Token);

              if( ds1.Tables[0].Columns.Contains("Error"))
                {
                    string error_log_file_name = System.DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-Error.txt";

                    string default_path = FolderPaths.Custodian.ROMFileError() + "\\" + error_log_file_name;
                    DataTable dt3 = new DataTable();
                    dt3 = UpdateCustodianROM(fjc_ROMUpload.event_id, dt1.Rows[0]["upload_id"].ToString(), error_log_file_name, default_path, Token, 0);//int Event_No,int DocID, string Token,int flag


                    //-Start-Error file created
                    if (!File.Exists(default_path))
                    {
                        FileStream fs = File.Create(default_path);
                        fs.Flush();
                        fs.Close();
                    }
                    //-End-Error file created 
                    StringBuilder bs = new StringBuilder();
                   
                        foreach (DataRow row in ds1.Tables[1].Rows)
                        {                           
                                foreach (DataColumn column in ds1.Tables[1].Columns)
                                {
                                    bs.Append(column.ColumnName.Replace("ErrorNum", "Error Number").Replace("remark", ", Error Description") + ": ").Append(row[column].ToString());  
                                }                           
                            bs.AppendLine();
                        }
                    File.WriteAllText(default_path, bs.ToString());
                    //return (DataTable)ds1.Tables[1].Rows[0]["remark"];
                    return Reformatter.Validate_DataTable(ds1.Tables[1]);

                }
                else
                {
                   // return (DataTable) ds1.Tables[0].Rows[0]["remark"];
                    return Reformatter.Validate_DataTable(ds1.Tables[0]);

                }

            }
            else
            {
                throw new CustomException.InvalidFileNotUploaded();

            } 
        }
        /// <summary>
        /// ////////////////////////////Check Custodian Vote file and return Error-Remark //////////////////////
        /// </summary>
        /// <param name="doc_id"></param>
        /// <param name="event_id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<DataSet> Check(int doc_id, int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@doc_id", doc_id);
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("SP_CHK_Custodian_Input_Datatble", dictLogin);
            return Reformatter.Validate_Dataset(ds);
        }
        private DataTable UpdateCustodianROM(int Event_No, string upload_id, string error_log_file_name, string default_path, string Token, int flag)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            dictUserDetail.Add("@upload_id", Convert.ToInt32(upload_id));
            dictUserDetail.Add("@error_log_file_name", error_log_file_name);
            dictUserDetail.Add("@default_path", default_path);
            dictUserDetail.Add("@event_id", Event_No);
            dictUserDetail.Add("@token", Token);
            dictUserDetail.Add("@flag", flag);

            DataSet ds = Persistence.Contexts.AppDBCalls.GetDataSet("Evote_Sp_Cust_ROM_Register", dictUserDetail).Result;
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        //////////////////////////////////////////Bulk Upload stored Procedure called here  ////////////////////////////////////////////////////     
        private async Task<DataTable> InsertBulkFileUpload(int Event_No,int DocID, string Token)
        {         
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@DocumentID", DocID);                
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@Flag", 0);
                
            DataSet ds=  await AppDBCalls.GetDataSet("Sp_ValidateAndInsert_CustodianROM", dictUserDetail);
           return Reformatter.Validate_DataTable(ds.Tables[0]);   
                
        }   

        private async Task<DataTable> RegisterCustodianROM(int Event_No,int DocID, string Token,int flag)   
        {
             Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@doc_id", DocID);   
                dictUserDetail.Add("@event_id", Event_No);
                dictUserDetail.Add("@token", Token);
                dictUserDetail.Add("@flag", flag);

            DataSet ds=  await AppDBCalls.GetDataSet("Evote_Sp_Cust_ROM_Register", dictUserDetail);
          return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        /////////////////////Get//////////////////
        public async Task<DataTable> GetCustodian_ROMUpload_Details(string Token)
        {
           return await RegisterCustodianROM(0,0,Token,1);
        } 

    }
}
 