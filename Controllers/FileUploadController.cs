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
        public async Task<IActionResult> FileUpload([FromForm] FJC_FileUpload _fjc_fileupload)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _fileUploadService.FileUpload_Details(_fjc_fileupload, Token);
                return Ok(Reformatter.Response_Object("File Uploaded successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }


            [HttpGet]            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> FetchFile([FromQuery] int doc_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _fileUploadService.GetFileDetails(doc_id, Token);
                    return Ok(Reformatter.Response_Object("File Details retrieved successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }   
            
    }
}
