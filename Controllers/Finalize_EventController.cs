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
    [Route("api/finalizeevent")]
    [Produces("application/json")]
    [ApiController]
    public class Finalize_EventController : ControllerBase
    {
            private readonly IApproveEventService _FinalizeEventService;

            public Finalize_EventController(IApproveEventService finalizeEventService)
            {
                _FinalizeEventService = finalizeEventService;
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Finalize_Event([FromQuery] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _FinalizeEventService.FinalizeEVENT(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Finalize Sccessfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
