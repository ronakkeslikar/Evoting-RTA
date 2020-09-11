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
    [Route("api/user-profile")]
    [Produces("application/json")]
    [ApiController]
     
    public class UserProfileController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public UserProfileController(IRegistrationService registrationService)
        {
            _registrationService = registrationService; 
        }       
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegistrationUpdate(FJC_Registration fJC_Registration)
        { 
            try
            {
                var result = await _registrationService.Registration_UpdateData(fJC_Registration);
                return Ok(Reformatter.Response_Object("Registration Updated Successfully", ref result));                
             }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
        }
         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetRegistrationID([FromQuery] int aud_id)
        {
            try
            {
                var result = await _registrationService.GetRegistrationIDData(aud_id);
                return Ok(Reformatter.Response_Object("Registration Detail retrieved Successfully", ref result));
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
