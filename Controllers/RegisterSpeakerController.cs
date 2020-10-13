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
    [Route("api/speaker")]
    [Produces("application/json")]
    [ApiController]
     
    public class RegisterSpeakerController : ControllerBase
    {

        private readonly IRegisterSpeakerService _RegisterSpeakerService;

        public RegisterSpeakerController(IRegisterSpeakerService RegisterSpeakerService)
        {
            _RegisterSpeakerService = RegisterSpeakerService; 
        }       
        
        [Authorize]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegisterSpeaker(FJC_SpeakerRegister fJC_Speaker)
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                              
                                                                    
                var result = await _RegisterSpeakerService.RegisterSpeakerData(fJC_Speaker, Token);
                return Ok(Reformatter.Response_Object("Speaker Registered saved successfully", ref result));  
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
        public async Task<IActionResult> RegisterGetSpeaker()
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                
                                                                    
                var result = await _RegisterSpeakerService.RegisterGetSpeakerData(Token);
                return Ok(Reformatter.Response_ArrayObject("Speaker Registered saved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
