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
    [Route("api/reports")]
    [Produces("application/json")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
            private readonly IApproveEventService _ReportsService;
            public ReportsController(IApproveEventService ReportsService)
            {
                _ReportsService = ReportsService;
            }

            // [HttpPost]
            // [ProducesResponseType(StatusCodes.Status200OK)]
            // [ProducesResponseType(StatusCodes.Status404NotFound)]
            // public async Task<IActionResult> Reports([FromQuery] int event_id)
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
            // public async Task<IActionResult> GetReports([FromQuery] int event_id)
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
