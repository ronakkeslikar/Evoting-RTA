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
 using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Web; 

namespace evoting.Services
{
    public interface IAgreementUploadService
    {  
        Task<DataTable> AgreementUpload_Details(int doc_id,string Token);        
    }

    public class AgreementUploadService : IAgreementUploadService
    {
        //db context here
        protected readonly AppDbContext _context;
        public AgreementUploadService(AppDbContext context)
        {
            _context = context;
        }  
//////////////////////////////////////////Agreement File Upload ////////////////////////////////////////////////////
        public async Task<DataTable> AgreementUpload_Details(int doc_id,string Token)
        {
           Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
            
                dictUserDetail.Add("@doc_id", doc_id);  
                dictUserDetail.Add("@token", Token);  

                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("sp_Upload_Agreement", dictUserDetail);                              
                return Reformatter.Validate_DataTable(ds.Tables[0]); 
        } 


//////////////////////////////////////////Get User Agreement Pdf from stored procedure to Upload  ////////////////////////////////////////////////////
         public async Task<DataTable> GetAgreementHtmlContent(int Event_No,string Token)
        { 
                Dictionary<string, object> dictUserDetail = new Dictionary<string, object>();               
                dictUserDetail.Add("@ID", 1); 
                dictUserDetail.Add("@CLIENT_NAME", "Lenovo"); 
                dictUserDetail.Add("@CLIENT_ADDRESS", "Mumbai"); 
                dictUserDetail.Add("@EVENT_NO", Event_No);  
                dictUserDetail.Add("@token", Token);  



                DataSet ds = new DataSet();
                ds = await AppDBCalls.GetDataSet("SP_GETDOCUMENTCONTENT", dictUserDetail);                              
                return Reformatter.Validate_DataTable(ds.Tables[0]); 
        }   
        public void ExportToPDF(string sb) 
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
 