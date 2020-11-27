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
            DataSet ds = new DataSet();
            ds = await getExcelData(Token,event_id);
            if(ds.Tables[0].Rows.Count>0)
            {
                    //Name of File  
                string fileName = "Scrutinizer_Evoting_Reports.xlsx";
                using (XLWorkbook wb = new XLWorkbook())
                {
                                        
                    var summarysheet=  wb.Worksheets.Add("Summary Report");
                    //Add DataTable in Details Report worksheet  
                    var summarysheet1=wb.Worksheets.Add(ds.Tables[0],"Details Report");
                    //wb.Worksheets.Add(ds.Tables[0],"Details Report");
                    summarysheet1.Style.Font.FontColor=XLColor.Black;
                    summarysheet1.SetAutoFilter(false);
                  
                     //Add Records for  Summary Report worksheet
                    var col1 = summarysheet.Column("A");
                    var colB = summarysheet.Column("B");
                    var colC = summarysheet.Column("C");


                    //col1.Style.Font.Bold = true;
                    col1.Style.Font.FontColor=XLColor.Black;
                    col1.Width = 60;
                    colB.Width=20;
                    colC.Width=30;
                   var secondsheetstyle= summarysheet.Cell(1, 1).SetValue("Report Generation Date and Time : " + ds.Tables[1].Rows[0]["report_generated_date"].ToString()).Style.Font.Bold=true;
 
                    summarysheet.Cell(3, 1).SetValue("EVSN").Style.Font.Bold=true;
                    summarysheet.Cell(3, 2).SetValue("ISIN").Style.Font.Bold=true;
                    summarysheet.Cell(4, 1).SetValue(ds.Tables[1].Rows[0]["EVSN"].ToString());
                    summarysheet.Cell(4, 2).SetValue(ds.Tables[1].Rows[0]["ISIN"].ToString());

                    // Voting Start Date and Time : 11-06-2020 09:00
                    // Voting End Date and Time : 14-06-2020 17:00
                    // Meeting Date and Start Time :15-06-2020 11:00
                    // Voting Finalisation Date and Time: 15-06-2020 12:05
                    summarysheet.Cell(6, 1).SetValue("Voting Start Date and Time : " + ds.Tables[1].Rows[0]["Voting Start Date and Time"].ToString() );
                    summarysheet.Cell(7, 1).SetValue("Voting End Date and Time : " + ds.Tables[1].Rows[0]["Voting End Date and Time"].ToString());
                    summarysheet.Cell(8, 1).SetValue("Meeting Date and Start Time : " +ds.Tables[1].Rows[0]["Meeting Date and Start Time"].ToString());
                    summarysheet.Cell(9, 1).SetValue("Voting Finalisation Date and Time: " + ds.Tables[1].Rows[0]["Voting Finalisation Date and Time"].ToString());

                    //Row start from ROW 11
                     summarysheet.Cell(11, 1).SetValue("Res. No.").Style.Font.Bold=true;
                     summarysheet.Cell(11, 2).SetValue("Yes Count").Style.Font.Bold=true;
                     summarysheet.Cell(11, 3).SetValue("Yes (%)").Style.Font.Bold=true;
                     summarysheet.Cell(11, 4).SetValue("No Count").Style.Font.Bold=true;
                     summarysheet.Cell(11, 5).SetValue("No (%)").Style.Font.Bold=true;
                     summarysheet.Cell(11, 6).SetValue("TotalCount").Style.Font.Bold=true;
                     summarysheet.Cell(11, 7).SetValue("Total").Style.Font.Bold=true;
                     summarysheet.Range("A11:G11").Style.Fill.BackgroundColor=XLColor.Gray;
                    //summarysheet.Cell(ds.Tables[2].Rows.Count, ds.Tables[2].Columns.Count).SetValue(ds.Tables[2]);
               int a=12;
                foreach(DataRow dr in ds.Tables[2].Rows)
                {
                    //Res. No.	Yes Count	Yes (%)	No Count	No (%)	TotalCount	Total

                     summarysheet.Cell(a,1).SetValue(dr["Res. No."].ToString());
                     summarysheet.Cell(a,2).SetValue(dr["Yes Count"].ToString());
                     summarysheet.Cell(a,3).SetValue(dr["Yes (%)"].ToString());
                     summarysheet.Cell(a,4).SetValue(dr["No Count"].ToString());
                     summarysheet.Cell(a,5).SetValue(dr["No (%)"].ToString());
                     summarysheet.Cell(a,6).SetValue(dr["TotalCount"].ToString());
                     summarysheet.Cell(a,7).SetValue(dr["Total"].ToString());

                    a++;
                    
                }


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

                        string pdffilename = System.DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + fileName;
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

                        DataSet ds2 = new DataSet();
                        ds2 = await AppDBCalls.GetDataSet("Evote_SpExcelFile_Download", dictfileDnld);
                        return Reformatter.Validate_DataTable(ds2.Tables[0]);
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
        public async Task<DataSet> getExcelData(string Token,int event_id)
        {
              Dictionary<string, object> dictfileDnld = new Dictionary<string, object>();              
                    
                    dictfileDnld.Add("@token", Token);
                    dictfileDnld.Add("@event_id",event_id);

                    DataSet ds = new DataSet();
                    ds = await AppDBCalls.GetDataSet("Evote_ExportToExcel", dictfileDnld);
                    return Reformatter.Validate_Dataset(ds);
        }

    }
}
