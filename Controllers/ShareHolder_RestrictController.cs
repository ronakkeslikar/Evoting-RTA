using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using evoting.Domain.Models;

namespace evoting.Controllers
{
    [Route("api/shareholder")]
    [Produces("application/json")]
    [ApiController]
    public class ShareHolder_RestrictController : ControllerBase
    {
            private readonly IShareHolderService _ShareHolder_RestrictService;
            public ShareHolder_RestrictController(IShareHolderService ShareHolder_RestrictService)
            {
                _ShareHolder_RestrictService = ShareHolder_RestrictService;
            }

            [HttpPost("restrict")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> ShareHolder_Restrict(FJC_SharedHolder_Restrict fjc_SharedHolder_Restrict)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result =  await _ShareHolder_RestrictService.ShareHolder_RestrictData(fjc_SharedHolder_Restrict,Token);
                    return Ok(Reformatter.Response_Object("ShareHolder restricted Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
            [HttpPut("derestrict")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> ShareHolder_DeRestrict(FJC_SharedHolder_Derestrict fjc_SharedHolder_Derestrict)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result =  await _ShareHolder_RestrictService.ShareHolder_DerestrictData(fjc_SharedHolder_Derestrict,Token);
                    return Ok(Reformatter.Response_Object("ShareHolder Derestricted Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
             [HttpGet("restricted")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> GetShareHolder_Restricted([FromQuery] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _ShareHolder_RestrictService.GetShareHolder_RestrictData(event_id, Token);
                    return Ok(Reformatter.Response_ArrayObject("ShareHolder restricted data retrieved Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
