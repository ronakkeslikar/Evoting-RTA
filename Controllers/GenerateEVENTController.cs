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

    [Route("api/Event")]          
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
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.GenerateEVENT(fJC_EVSN, Token);
                return Ok(Reformatter.Response_Object("Event has been generated succesfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEVENTUser(FJC_GenerateEVENT fJC_EVSN )
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.UpdateGenerateEVENT(fJC_EVSN, Token);
                return Ok(Reformatter.Response_Object("Event has been updated succesfully", ref result));

            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }           
        }
         

         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEVENTUser([FromQuery] Int32 EVENT_ID )

        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.GeteGenerateEVENT(EVENT_ID, Token);
                return Ok(Reformatter.Response_Object("Event List generated succesfully", ref result));

            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        } 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEVENTUser([FromQuery] Int32 EVENT_ID )

        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _GenerateEVENTService.DeleteGenerateEVENT(EVENT_ID, Token);
                return Ok(Reformatter.Response_Object("Event deleted succesfully", ref result));

            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }   
            
        }

    }
}