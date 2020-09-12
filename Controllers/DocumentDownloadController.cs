﻿using System;
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
        public async Task<IActionResult> Download_Document([FromQuery] string DownloadType)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                switch(DownloadType)
                    {
                        case "Tri_partiate_agreement":
                        var result = await _documentDownloadService.AgreementGenerator(Token);
                        return Ok(Reformatter.Response_Object("File Downloaded successfully", ref result));
                        break;
                        default:
                        throw new CustomException.InvalidActivity();
                    }                           
              
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDownload_Document()
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
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
