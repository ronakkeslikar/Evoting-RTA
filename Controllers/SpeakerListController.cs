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
     
    public class SpeakerListController : ControllerBase
    {

        private readonly ISpeakerListService _speakerListService;

        public SpeakerListController(ISpeakerListService speakerListService)
        {
            _speakerListService = speakerListService; 
        }       
        
        [Authorize]
        [HttpPost("speaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> SpeakerList(FJC_SpeakerList fJC_Speaker)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                              
                                                                    
                var result = await _speakerListService.SpeakerListData(fJC_Speaker, Token);
                return Ok(Reformatter.Response_Object("Speaker added successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
        [Authorize]
        [HttpPut("speaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SpeakerListUpdate(FJC_SpeakerList fJC_Speaker)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);

                var result = await _speakerListService.SpeakerListUpdateData(fJC_Speaker, Token);
                return Ok(Reformatter.Response_Object("Speaker details Updated successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }

        [Authorize]
        [HttpDelete("speaker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SpeakerListDelete(FJC_SpeakerList fJC_Speaker)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);

                var result = await _speakerListService.SpeakerListDeleteData(fJC_Speaker, Token);
                return Ok(Reformatter.Response_Object("Speaker details Deleted successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }

        [Authorize]
        [HttpGet("speakerlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetSpeakerListDetails([FromQuery] int event_id)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                
                                                                    
                var result = await _speakerListService.GetSpeakerList(event_id,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
