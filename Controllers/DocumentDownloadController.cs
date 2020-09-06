using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    [Route("api/DocumentDownload")]
    [ApiController]
    public class DocumentDownloadController : ControllerBase
    {
        private readonly IDocumentDownloadService _documentDownloadService;

        public DocumentDownloadController(IROMUploadService romUploadService)
        {
            _documentDownloadService = romUploadService;
        }
    }
}
