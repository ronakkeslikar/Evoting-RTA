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

namespace evoting.Controllers
{

    [Route("api/UpdateEVENT")]               
    [Produces("application/json")]
    [ApiController]
      public class UpdateEVENTController : ControllerBase
    {
          private readonly IGenerateEVENTService _GenerateEVENTService;

        public UpdateEVENTController(IGenerateEVENTService GenerateEVENTService)
        {
            _GenerateEVENTService = GenerateEVENTService;
        }

         [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> EVENTDetailUser(FJC_UpdateEVENT fJC_EVSN)

        {
            try
            {    
                var result = await _GenerateEVENTService.EVENTDetail(fJC_EVSN);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        } 

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> UpdateEVENTDetailUser([FromQuery] int EVENT_DETAIL_ID)

        {
            try
            {    
                var result = await _GenerateEVENTService.UpdateEVENTDetail(EVENT_DETAIL_ID);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

         [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> DeleteEVENTDetailUser([FromQuery] int EVENT_DETAIL_ID)

        {
            try
            {    
                var result = await _GenerateEVENTService.DeleteEVENTDetail(EVENT_DETAIL_ID);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }



    }
}