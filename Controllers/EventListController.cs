using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using evoting.Services;
using Microsoft.AspNetCore.Http;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using evoting.Domain.Models;
using evoting.Utility;

namespace evoting.Controllers
{
    [Route("api/event-list")]
    [Produces("application/json")]
    [ApiController]
     
    public class EventListController : ControllerBase
    {
        private readonly IEventListService _eventListService;

        public EventListController(IEventListService eventListService)
        {
            _eventListService = eventListService; 
        }       
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetList([FromQuery] string str)
        {  
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                                     
                var result = await _eventListService.GetEventList_Details(str,Token);
                return Ok(Reformatter.Response_ArrayObject("", ref result));
            }        
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
