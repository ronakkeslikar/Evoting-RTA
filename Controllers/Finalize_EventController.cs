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

            [Authorize]
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Finalize_Event([FromQuery] int event_id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                    var result = await _FinalizeEventService.FinalizeEVENT(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Finalize Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
