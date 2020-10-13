using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class Lock_EventController : ControllerBase
    {
            private readonly ILockEventService _Lock_EventService;
            public Lock_EventController(ILockEventService Lock_EventService)
            {
                _Lock_EventService = Lock_EventService;
            }

            [Authorize]
            [HttpPost("lock")]             
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Lock_Event([FromQuery] int event_id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                    var result = await _Lock_EventService.LockEventData(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Locked Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }

            [Authorize]
            [HttpPost("unlock")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> UnLock_Event([FromQuery] int event_id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                    var result = await _Lock_EventService.UnlockEventData(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Unlocked Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }

    }
}
