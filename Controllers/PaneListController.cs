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
    [Route("api/event")]
    [Produces("application/json")]
    [ApiController]
     
    public class PaneListController : ControllerBase
    {

        private readonly IPaneListService _PaneListService;

        public PaneListController(IPaneListService PaneListService)
        {
            _PaneListService = PaneListService; 
        }       
        
        [Authorize]
        [HttpPost("panelist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> PaneList(FJC_PaneList fJC_PaneList)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                              
                                                                    
                var result = await _PaneListService.PaneListData(fJC_PaneList, Token);
                return Ok(Reformatter.Response_Object("Panelist added successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
        [Authorize]
        [HttpPut("panelist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PaneListUpdate(FJC_PaneList fJC_PaneList)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);

                var result = await _PaneListService.PaneListUpdateData(fJC_PaneList, Token);
                return Ok(Reformatter.Response_Object("Panelist details Updated successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }
        [Authorize]
        [HttpDelete("panelist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PaneListDelete(FJC_PaneList fJC_PaneList)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);

                var result = await _PaneListService.PaneListDeleteData(fJC_PaneList, Token);
                return Ok(Reformatter.Response_Object("Panelist Removed Successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }

        [Authorize]
        [HttpGet("Panelist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetPaneListDetails([FromQuery] int event_id)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                
                                                                    
                var result = await _PaneListService.GetPaneList(event_id,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
