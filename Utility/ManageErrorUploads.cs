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
                //Cust-ROM-start
                DataSet ds1 = new DataSet();
                string default_path = default;
                string error_log_file_name = System.DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-Error.txt";

                if (_processType == ProcessType.Custodian_ROMUpload)
                {
                    ds1 = Custodian_VerifyFileProvided(doc_id, event_id, token);//(fjc_ROMUpload.doc_id, fjc_ROMUpload.event_id, Token);
                    default_path = FolderPaths.Custodian.ROMFileError() + "\\" + error_log_file_name;
                }

                if (ds1.Tables[0].Columns.Contains("Error"))
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
                            bs.Append(column.ColumnName.Replace("ErrorNum", "Error Number").Replace("remark", ", Error Description") + ": ").Append(row[column].ToString());
                        }
                        bs.AppendLine();
                    }
                    File.WriteAllText(default_path, bs.ToString());
                    if (_processType == ProcessType.Custodian_ROMUpload)
                    {
                        UpdateCustodianROM(event_id, dt1.Rows[0]["upload_id"].ToString(), error_log_file_name, default_path, token, 0);//int Event_No,int DocID, string Token,int flag

                    }//return (DataTable)ds1.Tables[1].Rows[0]["remark"];
                    return Reformatter.Validate_DataTable(ds1.Tables[0]);

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

        public DataSet Custodian_VerifyFileProvided(int doc_id, int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();
            dictLogin.Add("@doc_id", doc_id);
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@token", token);

            DataSet ds = new DataSet();
            ds =  AppDBCalls.GetDataSet("SP_CHK_Custodian_Input_Datatble", dictLogin).Result;
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
    }
}
