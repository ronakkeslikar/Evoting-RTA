using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using evoting.Services;
using Microsoft.AspNetCore.Http;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using evoting.Domain.Models;
using evoting.Utility;

namespace evoting.Controllers
{
    [Route("api/vc")]
    [Produces("application/json")]
    [ApiController]

    public class VideoConfController : ControllerBase
    {

        private readonly IVideoConfService _videoConfService;

        public VideoConfController(IVideoConfService videoConfService)
        {
            _videoConfService = videoConfService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VideoConf(FJC_VideoConf FJC_VideoConf)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _videoConfService.VideoConf_Update(FJC_VideoConf, Token);
                return Ok(Reformatter.Response_ArrayObject("VC Details updated in event successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VideoConfDetails([FromQuery] int event_id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers, identity);
                var result = await _videoConfService.Get_VideoConf(event_id, Token);
                return Ok(Reformatter.Response_ArrayObject("VC Details updated in event successfully", ref result));
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }

    }
}
