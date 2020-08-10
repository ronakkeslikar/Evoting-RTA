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
    [Route("api/ForgotPassword")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public ForgotPasswordController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]        
        public async Task<IActionResult> ForgotPassword(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                var result = (Object)null;
                switch (fJC_forgot.TypeOfUser)
                {
                    case 'I':
                        switch (fJC_forgot.TypeOfUpdate)
                        {
                            case 'D':
                                result = await _loginService.ForgotPassword_DOB_Data(fJC_forgot);
                                break;
                            case 'P':
                                result = await _loginService.ForgotPassword_PAN_ID_Data(fJC_forgot);
                                break;

                            case 'E':
                                 result = await _loginService.ForgotPasswordData(fJC_forgot);
                                break;
                        }
                        break;
                    case 'R':

                        result = await _loginService.ForgotPasswordData(fJC_forgot);
                        break;
                }
                //var result = await _loginService.ForgotPasswordData(fJC_forgot);
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestorEmailID(FJC_ForgotPassword fJC_forgot)

        {
            try
            {
                var result = await _loginService.GetInvestorEmailIDData(fJC_forgot);
                return Ok(JsonConvert.SerializeObject(result));

            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
