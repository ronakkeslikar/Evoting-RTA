﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download_Document([FromQuery] string DownloadType)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                switch(DownloadType.ToLower())
                    {
                        case "tri_partiate_agreement":
                        var result = await _documentDownloadService.AgreementGenerator(Token);
                        return Ok(Reformatter.Response_Object("File Downloaded successfully", ref result));
                        break;
                        default:
                        throw new CustomException.InvalidAttempt();
                    }                           
              
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
        public async Task<IActionResult> GetDownload_Document()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                var result = await _documentDownloadService.GetDocumentDownload(Token);
                return Ok(Reformatter.Response_Object("File Detail retieved successfully", ref result));              
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

    }
}
