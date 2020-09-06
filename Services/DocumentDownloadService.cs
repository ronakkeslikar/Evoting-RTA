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
            Task<DataTable> AgreementGenerator(int Event_No, string Token);
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

        public async Task<DataTable> AgreementGenerator(int Event_No, string Token)
        {
            DataTable dt = await GetAgreementHtmlContent(Event_No, Token);
            //ExportToPDF();
            return dt;

        }
        private async Task<DataTable> GetAgreementHtmlContent(int Event_No, string Token)
        {
            Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();
            
            dictUserDetail.Add("@EVENT_NO", Event_No);
            dictUserDetail.Add("@token", Token);



            DataSet ds = new DataSet();
            ds = await AppDBCalls.GetDataSet("SP_GETDOCUMENTCONTENT", dictUserDetail);
            return Reformatter.Validate_DataTable(ds.Tables[0]);
        }
        private void ExportToPDF(string sb)
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
                System.IO.File.WriteAllBytes(@"D:\shiv\Agreemtn_PDF.pdf", bytes);




            }

        }
    }
}
