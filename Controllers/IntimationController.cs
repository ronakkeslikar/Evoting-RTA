using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/event")]
    [Produces("application/json")]
    [ApiController]
    public class IntimationController : ControllerBase
    {
        private readonly IIntimationService _intimationService;

        public IntimationController(IIntimationService intimationService)
        {
            _intimationService = intimationService;
        }

        [Authorize]
        [HttpPost("intimation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(policy: "MustWorkForBigshareOnline")]
        public async Task<IActionResult> IntimationSave(FJC_Intimation fjc_intimation)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _intimationService.Intimation_data(fjc_intimation, Token);
                return Ok(Reformatter.Response_Object("Intimation Record Saved Successfully", ref result));

            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

        [Authorize]
        [HttpGet("intimation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(policy: "MustWorkForBigshareOnline")]
        public async Task<IActionResult> Vote_InvestorGetSaved(string type)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _intimationService.Intimation_Getdata(Token, type);
                return Ok(Reformatter.Response_ArrayObject("Intimation Record retrieved Successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }
    }
}
