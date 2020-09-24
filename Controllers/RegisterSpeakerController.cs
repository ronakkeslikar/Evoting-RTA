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
        
         [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegisterSpeaker([FromQuery] string speaker)
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result=(DataTable)null;                
                                                                    
                result = await _RegisterSpeakerService.RegisterSpeakerData(speaker,Token);
                return Ok(Reformatter.Response_ArrayObject("Speaker Registered saved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
