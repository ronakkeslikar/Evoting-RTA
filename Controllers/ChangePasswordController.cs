using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using evoting.Services;
using Microsoft.AspNetCore.Http;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using evoting.Domain.Models;
using evoting.Utility;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/ChangePassword")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public ChangePasswordController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Authorize]
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> ChangePassword(FJC_ChangePassword fJC_changePwd)
        {
          try
          {
            var identity = (ClaimsIdentity)User.Identity;  
            var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
            var result = await _loginService.ChangePasswordData(fJC_changePwd,Token);
            return Ok(Reformatter.Response_Object("Password Changed successfully", ref result));                  
          }
          catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
