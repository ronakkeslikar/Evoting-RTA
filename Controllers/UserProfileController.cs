using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegistrationUpdate(FJC_Registration fJC_Registration)
        { 
            try
            { 
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _registrationService.Registration_UpdateData(fJC_Registration,Token);
                return Ok(Reformatter.Response_Object("Profile Updated Successfully", ref result));                
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
        public async Task<IActionResult> GetRegistrationID()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _registrationService.GetRegistrationIDData(Token);
                return Ok(Reformatter.Response_Object("Profile Details retrieved Successfully", ref result));
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
        [Authorize]
        [HttpGet("investor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegistrationID_investor()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _registrationService.GetRegistrationInvestorData(Token);
                return Ok(Reformatter.Response_InvestorObject("Profile Details retrieved Successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }
    }
}
