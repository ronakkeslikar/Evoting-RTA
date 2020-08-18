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

namespace evoting.Controllers
{
    [Route("api/LoginReq")]
    [Produces("application/json")]
    [ApiController]
     
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> LoginUser(FJC_LoginRequest fJC_Login)
        {
            try
            {
                var result = await _loginService.LoginDataUser(fJC_Login);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (CustomException.InvalidUserID ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (CustomException.InvalidPassword ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }                
        }
    }
}
