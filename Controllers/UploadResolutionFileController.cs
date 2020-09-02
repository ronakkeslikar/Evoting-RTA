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
    [Route("api/UploadResolutionFile")]
    [Produces("application/json")]
    [ApiController]

    public class UploadResolutionFileController  : ControllerBase
    {

      private readonly IResolutionFileUploadService _ResolutionFileUploadService;

        public UploadResolutionFileController(IResolutionFileUploadService ResolutionFileUploadService)
        {
            _ResolutionFileUploadService = ResolutionFileUploadService;
        }

            [HttpPost]
            [Consumes("multipart/form-data")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]  
            public async Task<IActionResult> Upload_Resolution_File([FromForm]FJC_FileUpload std)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _ResolutionFileUploadService.ResolutionFileUpload_Details(std,Token);
                return Ok(new { status = true, message = "Resolution File Posted Successfully"});
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }   



    }


}