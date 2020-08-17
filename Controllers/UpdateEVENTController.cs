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
          private readonly IUpdateEVENTService _UpdateEVENTService;

        public UpdateEVENTController(IUpdateEVENTService UpdateEVENTService)
        {
            _UpdateEVENTService = UpdateEVENTService;
        }
       

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> UpdateEVENTUser(FJC_UpdateEVENT fJC_EVSN)

        {
            try
            {
               
                 
                var result = await _UpdateEVENTService.UpdateEVENT(fJC_EVSN);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }




    }
}