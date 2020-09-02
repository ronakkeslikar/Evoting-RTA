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

    [Route("api/EVENTResolution")]          
    [Produces("application/json")]
    [ApiController]

     public class EVENTResolutionController : ControllerBase
    {
          private readonly IGenerateEVENTService _GenerateEVENTService;

        public EVENTResolutionController(IGenerateEVENTService GenerateEVENTService)
        {
            _GenerateEVENTService = GenerateEVENTService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> EVENTResolutionUser(FJC_EVENT_Resolution FJC_EVENTRESOLUTION)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.EVENTResolution(FJC_EVENTRESOLUTION,Token);
                return Ok(Reformatter.Response_Object("Event Resolution Created Successfully", ref result));
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }
         

         [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEVENTResolutionUser(FJC_EVENT_Resolution FJC_EVENTRESOLUTION )
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.UpdateEVENTResolution(FJC_EVENTRESOLUTION,Token);
                return Ok(Reformatter.Response_Object("Event Resolution Updated Successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }             
        }
         

         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEVENTresolution([FromQuery] Int32 EVENT_RESOLUTION_ID )
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.GetEVENTResolution(EVENT_RESOLUTION_ID,Token);
                return Ok(Reformatter.Response_Object("Event Resolution details retrieved Successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        } 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEVENTResolution([FromQuery] Int32 EVENT_RESOLUTION_ID)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.DeleteEVENTResolution(EVENT_RESOLUTION_ID,Token);
                return Ok(Reformatter.Response_Object("Event Resolution deleted Successfully", ref result));  
            }
           catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }   
        }

    } 

}