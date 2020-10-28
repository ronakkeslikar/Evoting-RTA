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
    public class Vote_InvestorController : ControllerBase
    {
            private readonly IVote_InvestorService _vote_InvestorService;

            public Vote_InvestorController(IVote_InvestorService vote_InvestorService)
            {
                _vote_InvestorService = vote_InvestorService;
            }

            [Authorize]
            [HttpPost("vote")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Vote_InvestorSave(FJC_Vote_Investor fjc_Vote_Investor)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                    var result = await _vote_InvestorService.Vote_Investor_data(fjc_Vote_Investor, Token);
                    if(fjc_Vote_Investor.submitted==0)
                    {
                    return Ok(Reformatter.Response_Object("Investor Vote saved Successfully", ref result));
                    }
                    else
                    {
                    return Ok(Reformatter.Response_Object("Investor Vote submitted Successfully", ref result));
                    }
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }

            [Authorize]
            [HttpGet("vote")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Vote_InvestorGetSaved(int event_id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                    var result = await _vote_InvestorService.Vote_Investor_Getdata(Token, event_id);
                    return Ok(Reformatter.Response_ResolutionObject("Investor Vote details retrieved Succesfully", ref result));    
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
