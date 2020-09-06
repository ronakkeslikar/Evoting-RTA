using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    [Route("api/DocumentUpload")]
    [ApiController]
    public class DocumentUploadController : ControllerBase
    {
        private readonly IDocumentUploadService _documentUploadService;

        public DocumentUploadController(IDocumentUploadService documentDownloadService)
        {
            _documentUploadService = documentDownloadService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAgreement([FromForm] FJC_DOC_Upload fJC_DOC_Upload)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _documentUploadService.AgreementUpload_Details(fJC_DOC_Upload.doc_id, Token);
                return Ok(new { status = true, message = "File uploaded succesfully" });
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

    }
}
