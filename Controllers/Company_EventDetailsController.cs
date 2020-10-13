using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{

    [Route("api/CompanyEVENTDetails")]
    [Produces("application/json")]
    [ApiController]
    public class Company_EventDetailsController : ControllerBase
    {
         private readonly IGenerateEVENTService _GenerateEVENTService;

            public Company_EventDetailsController(IGenerateEVENTService GenerateEVENTService)
            {
                _GenerateEVENTService = GenerateEVENTService;
            }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EVENTDetailUser(FJC_CompanyUpdate_Event fJC_CompanyUpdate_Event)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _GenerateEVENTService.EVENTDetail(fJC_CompanyUpdate_Event, Token);
                return Ok(Reformatter.Response_ResolutionObject("Event-Details has been submitted succesfully", ref result));
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
        public async Task<IActionResult> Update_EVENTDetail(FJC_CompanyUpdate_Event fJC_CompanyUpdate_Event)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _GenerateEVENTService.EVENTDetail(fJC_CompanyUpdate_Event, Token);
                return Ok(Reformatter.Response_ResolutionObject("Event-Details has been updated succesfully", ref result));
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
        public async Task<IActionResult> Get_CompanyEventdetail([FromQuery] int event_id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _GenerateEVENTService.GetCompanyEVENTDetail(event_id, Token);
                return Ok(Reformatter.Response_ResolutionObject("Event-Details has been submitted succesfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }

        }
    }
}
