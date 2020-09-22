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
    [Route("api/event")]     
    [Produces("application/json")]
    [ApiController]
    public class UnBlock_EventController : ControllerBase
    {
            private readonly IApproveEventService _UnBlock_EventService;
            public UnBlock_EventController(IApproveEventService unblock_EventService)
            {
                _UnBlock_EventService = unblock_EventService;
            }

            [HttpPost("unblock")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> UnBlock_Event([FromQuery] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _UnBlock_EventService.UnBlockEventData(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Unblocked Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
