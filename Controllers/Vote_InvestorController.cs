using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            [HttpPost("vote")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Vote_InvestorSave(FJC_Vote_Investor fjc_Vote_Investor)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _vote_InvestorService.Vote_Investor_data(fjc_Vote_Investor, Token);
                    return Ok(Reformatter.Response_ResolutionObject("Investor Vote submitted Succesfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
            [HttpGet("vote")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Vote_InvestorGetSaved()
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _vote_InvestorService.Vote_Investor_Getdata(Token);
                    return Ok(Reformatter.Response_ResolutionObject("Investor Vote submitted Succesfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
