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

    [Route("api/GenerateEVENT")]          
    [Produces("application/json")]
    [ApiController]
    public class GenerateEVENTController : ControllerBase
    {
          private readonly IGenerateEVENTService _GenerateEVENTService;

        public GenerateEVENTController(IGenerateEVENTService GenerateEVENTService)
        {
            _GenerateEVENTService = GenerateEVENTService;
        }
       

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GenerateEVENTUser(FJC_GenerateEVENT fJC_EVSN)
        {
            try
            {
                var result = await _GenerateEVENTService.GenerateEVENT(fJC_EVSN);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

    }
}