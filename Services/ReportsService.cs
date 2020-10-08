using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;

namespace evoting.Services
{
    public interface IReportsService
    {
        Task<DataTable> ReportsData(int event_id, string Token);  
        Task<DataTable> ReportsGetData(int event_id, string Token);  

    }
    public class ReportsService : IReportsService
    {
        protected readonly AppDbContext _context;
        public ReportsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable> ReportsData(int event_id, string token)
        {
            //Export to Excel 
          var getExcelDetail= await exportToExcel(token, event_id);
          if(getExcelDetail!=null)
          {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 0);
            dictLogin.Add("@token", token);
             dictLogin.Add("@doc_no", getExcelDetail.Rows[0]["doc_no"]);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Scrutinizer_Report", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
          }
          else
          {
               throw new CustomException.InvalidFileRejected();
          }
        }

        public async Task<DataTable> ReportsGetData(int event_id, string token)
        {
            Dictionary<string, object> dictLogin = new Dictionary<string, object>();            
            dictLogin.Add("@event_id", event_id);
            dictLogin.Add("@flag", 1);
            dictLogin.Add("@token", token);
            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_Scrutinizer_Report", dictLogin);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        /////////////////////////////Export to excel/////////////////////
        public async Task<DataTable> exportToExcel(string Token,int event_id)
        {
            DataTable dt = new DataTable();
            dt = await getExcelData(Token,event_id);
            if(dt.Rows.Count>0)
            {
                    //Name of File  
                string fileName = "Sample.xlsx";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    //Add DataTable in worksheet  
                    wb.Worksheets.Add(dt);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        wb.SaveAs(memoryStream);
                        //Return xlsx Excel File  
                        // File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                        //Save To Folder
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        //convert byte to pdf and save
                        string actPath = FolderPaths.Scrutinizer.Scrutinizer_Reports();//@"C:\evoting\Agreement\";

                        string pdffilename = System.DateTime.Now.ToString("yyyyMMdd-hhmmssfff") + fileName;
                        System.IO.File.WriteAllBytes(Path.Combine(actPath , pdffilename), bytes);
                        string filePath = Path.Combine(actPath , pdffilename);

                        //Saving PDF to Folder and database
                        if(filePath.Length >0 && filePath != null)
                        {
                            Dictionary<string, object> dictfileDnld = new Dictionary<string, object>();               
                        dictfileDnld.Add("@File_Name", pdffilename);
                        dictfileDnld.Add("@File_Path", filePath);
                        dictfileDnld.Add("@token", Token);
                        dictfileDnld.Add("@event_id",event_id);

                        DataSet ds = new DataSet();
                        ds = await AppDBCalls.GetDataSet("Evote_SpExcelFile_Download", dictfileDnld);
                        return Reformatter.Validate_DataTable(ds.Tables[0]);
                        }
                        else
                        {
                            throw new CustomException.InvalidFileRejected();
                        }
                        

                    }
                }
            }
            else
            {
                throw new CustomException.InvalidActivity();
            }
            
        }
        public async Task<DataTable> getExcelData(string Token,int event_id)
        {
              Dictionary<string, object> dictfileDnld = new Dictionary<string, object>();              
                    
                    dictfileDnld.Add("@token", Token);
                    dictfileDnld.Add("@event_id",event_id);

                    DataSet ds = new DataSet();
                    ds = await AppDBCalls.GetDataSet("Evote_ExportToExcel", dictfileDnld);
                    return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

    }
}
