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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegistrationSave(FJC_Registration fJC_Registration)
        { 
            try
            {   
                 if(fJC_Registration.REG_TYPE_ID==1 || fJC_Registration.REG_TYPE_ID==2 )
                {
                  fJC_Registration.PANID="XXXXXXXX";  
                }                 
                var result = await _registrationService.Registration_InsertData(fJC_Registration);               
                  return Ok(Reformatter.Response_Object("New Registration completed Successfully", ref result));              
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }           
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
        public async Task<IActionResult> GetRegistrationID([FromQuery] int SR_NO)
        {
            try
            {
                var result = await _registrationService.GetRegistrationIDData(SR_NO);
                return Ok(Reformatter.Response_Object("Registration Detail retrieved Successfully", ref result));
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
