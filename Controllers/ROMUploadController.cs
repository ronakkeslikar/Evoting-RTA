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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/ROM")]
    [Produces("application/json")]
    [ApiController]
     
    public class ROMUploadController : ControllerBase
    {
        private readonly IROMUploadService _romUploadService;

        public ROMUploadController(IROMUploadService romUploadService)
        {
            _romUploadService = romUploadService;
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
                var result = await _romUploadService.ROMUpload_Details(std,Token);
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
            public async Task<IActionResult> GetROM()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _romUploadService.GetROMUpload_Details(Token);
               return Ok(Reformatter.Response_ArrayObject("File Details retrieved successfully", ref result));
            }
           catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }   
            
    }
}
