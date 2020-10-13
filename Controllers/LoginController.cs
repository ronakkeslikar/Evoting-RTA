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
    [Route("api/Login")]
    [Produces("application/json")]
    [ApiController]
     
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Authorize]
        [HttpPost]      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> LoginUser(FJC_LoginRequest fJC_Login)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                var result=(System.Data.DataTable)null;
                BSC_LoginResponse loginResponse =
                        new BSC_LoginResponse()
                        {
                            Error = result.Rows[0]["Error"].ToString(),
                            Audience = result.Rows[0]["Audience"].ToString(),
                            EmailID = result.Rows[0]["EmailID"].ToString(),
                            Name = result.Rows[0]["Name"].ToString(),
                            Token = result.Rows[0]["Token"].ToString()
                        };
                loginResponse.Token = Token_Handling.Generate_token(loginResponse);
                return Ok(new { message= "User logged in succesfully", data = loginResponse });
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }
    }
}
