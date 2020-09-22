using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    [Route("api/shareholder/restrict")]
    [Produces("application/json")]
    [ApiController]
    public class ShareHolder_RestrictController : ControllerBase
    {
            private readonly IApproveEventService _ShareHolder_RestrictService;
            public ShareHolder_RestrictController(IApproveEventService ShareHolder_RestrictService)
            {
                _ShareHolder_RestrictService = ShareHolder_RestrictService;
            }

            // [HttpPost]
            // [ProducesResponseType(StatusCodes.Status200OK)]
            // [ProducesResponseType(StatusCodes.Status404NotFound)]
            // public async Task<IActionResult> ShareHolder_Restricted([FromQuery] int event_id)
            // {
            //     try
            //     {
            //         var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
            //         var result = "";
            //         return Ok(Reformatter.Response_Object("Client Blocked Successfully", ref result));
            //     }
            //     catch (Exception ex)
            //     {
            //         return (new HandleCatches()).ManageExceptions(ex);
            //     }
            // }
            //  [HttpGet]
            // [ProducesResponseType(StatusCodes.Status200OK)]
            // [ProducesResponseType(StatusCodes.Status404NotFound)]
            // public async Task<IActionResult> GetShareHolder_Restricted([FromQuery] int event_id)
            // {
            //     try
            //     {
            //         var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
            //         var result = "";
            //         return Ok(Reformatter.Response_Object("Client Blocked Successfully", ref result));
            //     }
            //     catch (Exception ex)
            //     {
            //         return (new HandleCatches()).ManageExceptions(ex);
            //     }
            // }
    }
}
