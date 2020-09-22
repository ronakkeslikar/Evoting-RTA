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
    [Route("api/shareholder/derestrict")]
    [Produces("application/json")]
    [ApiController]
    public class ShareHolder_DeRestrictController : ControllerBase
    {
            private readonly IApproveEventService _ShareHolder_DeRestrictService;
            public ShareHolder_DeRestrictController(IApproveEventService ShareHolder_DeRestrictService)
            {
                _ShareHolder_DeRestrictService = ShareHolder_DeRestrictService;
            }

            // [HttpPost]
            // [ProducesResponseType(StatusCodes.Status200OK)]
            // [ProducesResponseType(StatusCodes.Status404NotFound)]
            // public async Task<IActionResult> ShareHolder_DeRestricted([FromQuery] int event_id)
            // {
            //     try
            //     {
            //         var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
            //         var result = "";
            //         return Ok(Reformatter.Response_Object("Client Unblocked Successfully", ref result));
            //     }
            //     catch (Exception ex)
            //     {
            //         return (new HandleCatches()).ManageExceptions(ex);
            //     }
            // }
            //  [HttpGet]
            // [ProducesResponseType(StatusCodes.Status200OK)]
            // [ProducesResponseType(StatusCodes.Status404NotFound)]
            // public async Task<IActionResult> GetShareHolder_DeRestricted([FromQuery] int event_id)
            // {
            //     try
            //     {
            //         var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
            //         var result = "";
            //         return Ok(Reformatter.Response_Object("Client detail retrived Successfully", ref result));
            //     }
            //     catch (Exception ex)
            //     {
            //         return (new HandleCatches()).ManageExceptions(ex);
            //     }
            // }
    }
}
