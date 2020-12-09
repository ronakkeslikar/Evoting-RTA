using evoting.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static evoting.Utility.FolderPaths;

namespace evoting.Utility
{
    public class ManageErrorUploads
    {
        public DataTable Manage_ROM_ErrorUploads(DataTable dt1, ProcessType _processType, int doc_id, int event_id, string token)//dt1 here means response from uploading resp file
        {
            if (dt1.Rows.Count > 0)
            {                
                DataSet ds1 = new DataSet();
                string default_path = default;
                string error_log_file_name = System.DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-Error.txt";

                if (_processType == ProcessType.Custodian_ROMUpload)
                {
                    ds1 = Custodian_VerifyFileProvided(doc_id, event_id, token);//(fjc_ROMUpload.doc_id, fjc_ROMUpload.event_id, Token);
                    default_path = FolderPaths.Custodian.ROMFileError() + "\\" + error_log_file_name;
                }
                else if (_processType == ProcessType.Scrutinizer_Reports)
                {
                    //ds1 = Custodian_VerifyFileProvided(doc_id, event_id, token);//(fjc_ROMUpload.doc_id, fjc_ROMUpload.event_id, Token);
                    DataTable _dt = new DataTable();
                    _dt.TableName = "Table0";
                    _dt.Columns.Add("Error");
                    ds1.Tables.Add(_dt);
                    ds1.Tables.Add(dt1.Copy());
                    default_path = FolderPaths.Scrutinizer.Scrutinizer_FileError() + "\\" + error_log_file_name ;
                }

                if (ds1.Tables[0].Columns.Contains("Error"))
                {
                    WriteErrorFile(default_path, ds1);
                     
                    if (_processType == ProcessType.Custodian_ROMUpload)
                    {
                        UpdateCustodianROM(event_id, dt1.Rows[0]["upload_id"].ToString(), error_log_file_name, default_path, token, 0);//int Event_No,int DocID, string Token,int flag
                        return Reformatter.Validate_DataTable(ds1.Tables[1]);
                    }
                    else if(_processType == ProcessType.Scrutinizer_Reports)
                    {
                        DataTable dt = new DataTable();
                        dt=Update_ScrutinizerReport_failure(error_log_file_name, default_path, token, event_id);
                         DataSet ds = new DataSet();
                        ds.Tables.Add(dt.Copy());
                       return ds.Tables[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return Reformatter.Validate_DataTable(ds1.Tables[1]);
                }
            }
            else
            {
                throw new CustomException.InvalidFileNotUploaded();                
            }
        }

        public DataSet Custodian_VerifyFileProvided(int doc_id, int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@doc_id", doc_id);
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds =  AppDBCalls.GetDataSet("SP_CHK_Custodian_Input_Datatble", dictLogin).Result;
            return  ds;
        }
        private void WriteErrorFile(string default_path, DataSet ds1)
        {
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
                    bs.Append(column.ColumnName.Replace("Error_Num", "Error Number").Replace("remark", ", Error Description") + ": ").Append(row[column].ToString());
                }
                bs.AppendLine();
            }
            File.WriteAllText(default_path, bs.ToString());
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
        private DataTable Update_ScrutinizerReport_failure(string error_log_file_name,string default_path, string Token, int event_id)
        {
            Dictionary<string, object> dictfileDnld = new Dictionary<string, object>();               
                    dictfileDnld.Add("@File_Name", error_log_file_name);
                    dictfileDnld.Add("@File_Path", default_path);
                    dictfileDnld.Add("@token", Token);
                    dictfileDnld.Add("@event_id",event_id);
                    dictfileDnld.Add("@flag",1);

                    return AppDBCalls.GetDataSet("Evote_SpExcelFile_Download", dictfileDnld).Result.Tables[0];
                
        }
    }
}
