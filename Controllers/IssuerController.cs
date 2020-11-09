
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

namespace evoting.Controllers
{
    [Route("api/issuer")]
    [Produces("application/json")]
    [ApiController]
     
    public class IssuerController : ControllerBase
    {
        private readonly INoticeService _issuerService;

        public IssuerController(INoticeService issuerService)
        {
            _issuerService = issuerService; 
        }       
        
         [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetIssuer([FromQuery] string str)
        {
            try
            { 
                var result=  await _issuerService.GetIssuer_Details(str);
                return Ok(Reformatter.Response_ArrayObject("", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
