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
    [Route("api/ApproveEvent")]
    [Produces("application/json")]
    [ApiController]
    public class ApproveEventController : ControllerBase
    {
            private readonly IApproveEventService _ApproveEventService;

            public ApproveEventController(IApproveEventService approveEventService)
            {
                _ApproveEventService = approveEventService;
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> RegistrationSave([FromForm] int event_id)
            {
                try
                {
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                    var result = await _ApproveEventService.ApproveEVENT(event_id, Token);
                    return Ok(Reformatter.Response_Object("Event Approved Succesfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
