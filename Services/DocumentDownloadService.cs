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
        }

        public class DocumentDownloadService : IDocumentDownloadService
        {
            //db context here
            protected readonly AppDbContext _context;
            public DocumentDownloadService(AppDbContext context)
            {
                _context = context;
            }

        public async Task<DataTable> AgreementUpload_Details(int doc_id, string Token)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();

            dictUserDetail.Add("@doc_id", doc_id);
            dictUserDetail.Add("@token", Token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("sp_Upload_Agreement", dictUserDetail);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }

        public async string AgreementGenerator(string Token)
        {
            DataTable dt = await GetAgreementHtmlContent(Token);
            //ExportToPDF();
             string htmlstr=dt.Rows[0]["CONTENT"].ToString();
           string str1= ExportToPDF(htmlstr);
            return str1;

        }
        private async Task<DataTable> GetAgreementHtmlContent( string Token)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();            
            dictUserDetail.Add("@token", Token);

            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("SP_GETDOCUMENTCONTENT", dictUserDetail);           
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        private string ExportToPDF(string sb)
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
                System.IO.File.WriteAllBytes(@"C:\evoting\Agreement_PDF.pdf", bytes);
                return @"C:\evoting\Agreement_PDF.pdf";



            }

        }
    }
}
