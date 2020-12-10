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
using reCAPTCHA.AspNetCore.Attributes;
using reCAPTCHA.AspNetCore;

namespace evoting.Controllers
{
    [Route("api/Login")]
    [Produces("application/json")]
    [ApiController]
     
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRecaptchaService _recaptcha;
        private readonly double _minimumScore;
        private readonly string _errorMessage;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        //public LoginController(IRecaptchaService recaptcha)
        //{
        //    _recaptcha = recaptcha;
        //    _minimumScore = 0.5;
        //    _errorMessage = "There was an error validating the google recaptcha response. Please try again, or contact no body";
        //}

        [HttpPost]
        //[ValidateRecaptcha]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> LoginUser(FJC_LoginRequest fJC_Login)
        {
            try
            {
                var result = await _loginService.LoginDataUser(fJC_Login);
                BSC_LoginResponse loginResponse =
                        new BSC_LoginResponse()
                        {
                            Error = result.Rows[0]["Error"].ToString(),
                            Audience = result.Rows[0]["Audience"].ToString(),
                            EmailID = result.Rows[0]["EmailID"].ToString(),
                            Name = result.Rows[0]["Name"].ToString(),
                            Token = result.Rows[0]["Token"].ToString()
                        };
                if (loginResponse.Error.Trim() == string.Empty)
                {
                    loginResponse.Token = Token_Handling.Generate_token(loginResponse);
                    return Ok(new { message = "User logged in successfully", data = loginResponse });
                }
                else
                {
                    switch(loginResponse.Error)
                    {
                        case "Multiple login requests":
                            throw new CustomException.MultipleRequests();
                        case "Invalid User ID OR Password":
                            throw new CustomException.InvalidPassword();
                        case "Invalid User ID":
                            throw new CustomException.InvalidUserID();
                        default: throw new CustomException.InvalidAttempt();
                           
                    }
                }
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
        }
    }
}
