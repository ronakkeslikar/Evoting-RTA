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
    public class Block_EventController : ControllerBase
    {
            private readonly IApproveEventService _Block_EventService;
            public Block_EventController(IApproveEventService block_EventService)
            {
                _Block_EventService = block_EventService;
            }

            [HttpPost("block")]             
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Block_Event([FromQuery] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _Block_EventService.BlockEventData(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Blocked Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
