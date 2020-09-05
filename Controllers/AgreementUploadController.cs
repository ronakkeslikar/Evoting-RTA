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
    [Route("api/Agreement")]
    [Produces("application/json")]
    [ApiController]
     
    public class AgreementUploadController : ControllerBase
    {
        private readonly IAgreementUploadService _agreementUploadService;

        public AgreementUploadController(IAgreementUploadService agreementUploadService)
        {
            _agreementUploadService = agreementUploadService;
        }

            [HttpPost]      
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]  
            public async Task<IActionResult> Agreement(int doc_id)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _agreementUploadService.AgreementUpload_Details(doc_id,Token);
                return Ok(new { status = true, message = "File Posted Successfully"});
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }   
            
    }
}
