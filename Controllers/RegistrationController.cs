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
    [Route("api/Registration")]
    [Produces("application/json")]
    [ApiController]
     
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationSave(FJC_Registration fJC_Registration)
        { 
            var result = await _registrationService.Registration_InsertData(fJC_Registration);
            return Ok(JsonConvert.SerializeObject(result));
        }
        [HttpPut]
        public async Task<IActionResult> RegistrationUpdate(FJC_Registration fJC_Registration)
        { 
            var result = await _registrationService.Registration_UpdateData(fJC_Registration);
            return Ok(JsonConvert.SerializeObject(result));
        }
         [HttpGet]
        public async Task<IActionResult> GetRegistrationID(FJC_Registration fJC_Registration)
        {
            try
            {
                var result = await _registrationService.GetRegistrationIDData(fJC_Registration);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
