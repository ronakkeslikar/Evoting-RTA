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

namespace evoting.Controllers
{
    [Route("api/LoginReq")]
    [Produces("application/json")]
    [ApiController]
     
    public class LoginController : ControllerBase
    {
        private readonly IUserService _loginService;

        public LoginController(IUserService userService)
        {
            _loginService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(FJC_LoginRequest fJC_Login)

        {
            //var result = await _userService.GetUserDataAsync(); 
            var result = await _loginService.LoginDataUser(fJC_Login);
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
