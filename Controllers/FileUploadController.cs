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
using System.IO;

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

        [HttpPost , DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> ROM(FJC_FileUpload fjc_FileUpload)
        {
            // try
            // {
            //     var result = await _fileUploadService.FileUpload_Details(fjc_FileUpload);
            //     return Ok(JsonConvert.SerializeObject(result));
            // }
            // catch (CustomException.InvalidUserID ex)
            // {
            //     return Unauthorized(ex.Message);
            // }
            // catch
            // {
            //     return Unauthorized();
            // }   
            
            try
    {
        var file = Request.Form.Files[0];
        var folderName = Path.Combine("ROM", "RTA");           
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
         if (!Directory.Exists(pathToSave))  
            {                 
               Directory.CreateDirectory(pathToSave);
            } 
 
        if (file.Length > 0)
        {
            var fileName = file.FileName;//ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            // string[] paths = {@"d:\FileUpload", "ROM", "RTA", "XLS"};
            // string fullPath = Path.Combine(paths);
            //d:\FileUpload\ROM\RTA\XLS
            var fullPath = Path.Combine(pathToSave, fileName);
            fjc_FileUpload.File_Path = Path.Combine(folderName,System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")+ fjc_FileUpload.Token_No + fileName);
            
            //var dbPath = Path.Combine(folderName, fileName);//System.DateTime.Now().ToString("yyyy-MM-dd hh:mm:ss:fff")
 
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
         var result = await _fileUploadService.FileUpload_Details(fjc_FileUpload);
         return Ok(JsonConvert.SerializeObject(result));
           // return Ok(new { dbPath });
        }
        else
        {
            return BadRequest();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex}");
    }             
        }
    }
}
