﻿using System;
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
    [Route("api/ChangePassword")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public ChangePasswordController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]        
        public async Task<IActionResult> ForgotPassword(FJC_ChangePassword fJC_changePwd)

        {
            //var result = await _userService.GetUserDataAsync(); 
            var result = await _loginService.ChangePasswordData(fJC_changePwd);
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
