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
                var result = await _GenerateEVENTService.EVENTResolution(FJC_EVENTRESOLUTION);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (CustomException.InvalidEventId ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }   
            
        }
         

         [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEVENTResolutionUser(FJC_EVENT_Resolution FJC_EVENTRESOLUTION )

        {
            try
            {
                var result = await _GenerateEVENTService.UpdateEVENTResolution(FJC_EVENTRESOLUTION);
                return Ok(JsonConvert.SerializeObject(result));

            }
            catch (CustomException.InvalidEventId ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }   
            
        }
         

         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEVENTresolution([FromQuery] Int32 EVENT_RESOLUTION_ID )

        {
            try
            {
                var result = await _GenerateEVENTService.GetEVENTResolution(EVENT_RESOLUTION_ID);
                return Ok(JsonConvert.SerializeObject(result));

            }
            catch (CustomException.InvalidEventId ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }   
            
        } 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEVENTResolution([FromQuery] Int32 EVENT_RESOLUTION_ID)

        {
            try
            {
                var result = await _GenerateEVENTService.DeleteEVENTResolution(EVENT_RESOLUTION_ID);
                return Ok(JsonConvert.SerializeObject(result));

            }
           catch (CustomException.InvalidEventId ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }   
        }

    } 

}