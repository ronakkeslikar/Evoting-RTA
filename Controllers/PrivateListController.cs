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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/private-list")]
    [Produces("application/json")]
    [ApiController]
     
    public class PrivateListController : ControllerBase
    {

        private readonly IPrivateListService _privateListService;

        public PrivateListController(IPrivateListService privateListService)
        {
            _privateListService = privateListService; 
        }       
        
        [Authorize]
        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetCommonUtitlity([FromQuery] string str)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                     
                                                                    
                var result = await _privateListService.Getprivate_List_Details(str,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
