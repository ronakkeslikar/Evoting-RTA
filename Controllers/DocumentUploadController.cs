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
    [Route("api/DocumentUpload")]
    [ApiController]
    public class DocumentUploadController : ControllerBase
    {
        private readonly IDocumentUploadService _documentUploadService;

        public DocumentUploadController(IDocumentUploadService documentDownloadService)
        {
            _documentUploadService = documentDownloadService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAgreement(FJC_DOC_Upload fJC_DOC_Upload)
        {
           
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                var result=(System.Data.DataTable)null;
                switch (fJC_DOC_Upload.upload_type.ToLower())
                {
                    case "agreement":
                        result = await _documentUploadService.AgreementUpload_Details(fJC_DOC_Upload.doc_id, Token);
                        return Ok(Reformatter.Response_Object("File uploaded successfully and will be validated soon", ref result));
                        break;

                    case "power_of_attorney":
                        result = await _documentUploadService.PowerOfAttorneyDownload(fJC_DOC_Upload.doc_id,Token);
                        return Ok(Reformatter.Response_Object("File uploaded successfully", ref result));
                        break;

                    default:
                        throw new CustomException.InvalidActivity();
                }                  
           
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDocuments()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                var result = await _documentUploadService.AllUploadedDocuments( Token);
                return Ok(Reformatter.Response_ArrayObject("File Details retrieved successfully", ref result));

            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

    }
}