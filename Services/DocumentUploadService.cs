﻿﻿using evoting.Persistence.Contexts;
using evoting.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace evoting.Services
{
    public interface IDocumentUploadService
    {
        Task<DataTable> AgreementUpload_Details(int doc_id, string Token);
    }
    public class DocumentUploadService : IDocumentUploadService
    {
        protected readonly AppDbContext _context;
        public DocumentUploadService(AppDbContext context)
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
    }
}