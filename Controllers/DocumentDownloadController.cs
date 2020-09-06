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
    [Route("api/DocumentDownload")]
    [ApiController]
    public class DocumentDownloadController : ControllerBase
    {
        private readonly IDocumentDownloadService _documentDownloadService;

        public DocumentDownloadController(IDocumentDownloadService  documentDownloadService)
        {
            _documentDownloadService = documentDownloadService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateAgreement([FromForm] string DownloadType)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);                
                var result = await _documentDownloadService.AgreementGenerator(Token);
                return Ok(new { status = true, message = "File generated succesfully" });
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

    }
}
