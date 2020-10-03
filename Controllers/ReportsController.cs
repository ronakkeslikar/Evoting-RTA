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
            private readonly IReportsService _ReportsService;
            public ReportsController(IReportsService ReportsService)
            {
                _ReportsService = ReportsService;
            }
            
             [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> GetReports([FromQuery] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _ReportsService.ReportsGetData(event_id,Token); 
                    return Ok(Reformatter.Response_ArrayObject("Scrutinizer Reports Retrieved Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostReports([FromQuery] int event_id)
        {
            try
            {
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result = await _ReportsService.ReportsData(event_id, Token);
                return Ok(Reformatter.Response_Object("Reports will be generated now", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }
    }
}
