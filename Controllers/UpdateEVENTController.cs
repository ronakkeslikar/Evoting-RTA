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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{

    [Route("api/EVENTDetails")]               
    [Produces("application/json")]
    [ApiController]
      public class UpdateEVENTController : ControllerBase
    {
          private readonly IGenerateEVENTService _GenerateEVENTService;

        public UpdateEVENTController(IGenerateEVENTService GenerateEVENTService)
        {
            _GenerateEVENTService = GenerateEVENTService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> EVENTDetailUser(FJC_UpdateEVENT fJC_EVSN)

        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _GenerateEVENTService.EVENTDetail(fJC_EVSN, Token);
                //return Ok(Reformatter.Response_ResolutionObject("Event-Details has been submitted succesfully", ref result));
                dynamic _obj = Reformatter.Response_ResolutionObject("Event-Details has been submitted succesfully", ref result);
                int _statuscode = _obj.StatusCode;
                return StatusCode(_statuscode, _obj);
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        } 

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> UpdateEVENTDetailUser(FJC_UpdateEVENT fJC_EVSN)

        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _GenerateEVENTService.EVENTDetail(fJC_EVSN, Token);
                //return Ok(Reformatter.Response_ResolutionObject("Event ID "+ result.Tables[0].Rows[0][0] +"Details has been updated succesfully", ref result));
                dynamic _obj = Reformatter.Response_ResolutionObject("Event ID " + result.Tables[0].Rows[0][0] + "Details has been updated succesfully", ref result);
                int _statuscode = _obj.StatusCode;
                return StatusCode(_statuscode, _obj);
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
        public async Task<IActionResult> GetEVENTDetailUser([FromQuery] int event_id)

        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var result = await _GenerateEVENTService.GetEVENTDetail(event_id, Token);
                return Ok(Reformatter.Response_ResolutionObject("Event details retrieved succesfuly", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }         



    }
}