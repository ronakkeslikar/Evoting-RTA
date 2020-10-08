using evoting.Persistence.Contexts;
using evoting.Utility;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    
        public interface IDocumentDownloadService
        {
            Task<DataTable> AgreementGenerator( string Token);
            Task<DataTable> GetDocumentDownload( string Token);
        }

        public class DocumentDownloadService : IDocumentDownloadService
        {
            //db context here
            protected readonly AppDbContext _context;
            public DocumentDownloadService(AppDbContext context)
            {
                _context = context;
            }

        

        public async Task<DataTable> AgreementGenerator(string Token)
        {
            DataTable dt = await GetAgreementHtmlContent(Token);
            //ExportToPDF();
             string htmlstr=dt.Rows[0]["CONTENT"].ToString();

            DataTable dt2= await ExportToPDF(htmlstr,Token);  
            return dt2;
        }
        private async Task<DataTable> GetAgreementHtmlContent( string Token)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();            
            dictUserDetail.Add("@token", Token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("SP_GETDOCUMENTCONTENT", dictUserDetail);           
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        private async Task<DataTable> ExportToPDF(string sb,string Token)
        { 
            StringReader sr = new StringReader(sb.ToString());

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            { 
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(sr);
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                //convert byte to pdf and save
                string actPath= FolderPaths.Company.AgreementDownload();//@"C:\evoting\Agreement\";
               
                string pdffilename=System.DateTime.Now.ToString("yyyyMMdd-hhmmssfff") + "-Agreement_PDF.pdf";
                System.IO.File.WriteAllBytes(Path.Combine(actPath,pdffilename), bytes);
                string filePath=Path.Combine(actPath , pdffilename);

                //Saving PDF to Folder and database
                  Dictionary<string, object> dictfileDnld = new Dictionary<string, object>();               
                dictfileDnld.Add("@File_Name", pdffilename);
                dictfileDnld.Add("@File_Path", filePath);
                dictfileDnld.Add("@token", Token);

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("Evote_SpAgreement_Download", dictfileDnld);
                return Reformatter.Validate_DataTable(ds.Tables[0]);
                //return @"C:\evoting\Agreement\Agreement_PDF.pdf";
            }             

        }

        public async Task<DataTable> GetDocumentDownload(string Token)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();            
            dictUserDetail.Add("@token", Token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("Evote_GetDocumentDownload", dictUserDetail);           
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
    }
}
