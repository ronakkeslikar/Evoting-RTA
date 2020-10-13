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
using ClosedXML.Excel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/cust-ROM")]
    [Produces("application/json")]
    [ApiController]
     
    public class CustodianROMUploadController : ControllerBase
    {
        private readonly ICustodianROMUploadService _CustodianROMUploadService;

        public CustodianROMUploadController(ICustodianROMUploadService CustodianROMUploadService)
        {
            _CustodianROMUploadService = CustodianROMUploadService;
        }

            [Authorize]
            [HttpPost]            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]  
            public async Task<IActionResult> ROM(FJC_ROMUpload std)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _CustodianROMUploadService.Cutodian_ROMUpload_Details(std,Token);              
               return Ok(Reformatter.Response_Object("File Uploaded successfully", ref result));
            }
           catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }             

            [Authorize]
            [HttpGet]            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]  
            public async Task<IActionResult> Get_Custodian_ROM()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _CustodianROMUploadService.GetCustodian_ROMUpload_Details(Token);
               return Ok(Reformatter.Response_Object("File Details retrieved successfully", ref result));
            }
           catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }   
            
    }
}
