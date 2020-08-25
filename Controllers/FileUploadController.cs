using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using evoting.Services;
using Microsoft.AspNetCore.Http;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using evoting.Domain.Models;
using evoting.Utility;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;

namespace evoting.Controllers
{
    [Route("api/FileUpload")]
    [Produces("application/json")]
    [ApiController]
     
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

            [HttpPost]
            [Consumes("multipart/form-data")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]  
            public async Task<IActionResult> ROM([FromForm]FJC_FileUpload std)
        {
            var result = await _fileUploadService.FileUpload_Details(std);
            return Ok(new { status = true, message = "File Posted Successfully"});
        }   
            
    }
}
