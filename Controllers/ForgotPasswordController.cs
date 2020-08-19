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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> ForgotPassword(FJC_ForgotPassword fJC_forgot)
        {
            try
            {
                var result = (Object)null;      
                if(fJC_forgot.PAN_ID=="" && (fJC_forgot.TypeOfUser!='I'|| fJC_forgot.TypeOfUpdate =='E')) 
                {
                    fJC_forgot.PAN_ID="XXXXXXXX";
                }         
                
                    if(fJC_forgot.TypeOfUser=='I')
                    { 
                    
                        switch (fJC_forgot.TypeOfUpdate)
                        {
                            case 'D':                            
                                result = await _loginService.ForgotPassword_DOB_Data(fJC_forgot);
                                break;                                          
                            case 'B':
                                result = await _loginService.ForgotPassword_BANK_ACC_Data(fJC_forgot);
                                 break;
                            case 'E':                                 
                                 result = await _loginService.ForgotPasswordData(fJC_forgot);
                                break;
                        }
                    }
                    else
                    {                        
                       result = await _loginService.ForgotPasswordData(fJC_forgot);  
                    } 
                              
                return Ok(JsonConvert.SerializeObject(result));
            }
           catch (CustomException.InvalidEmailID ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return Unauthorized();
            }             
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvestorEmailID([FromQuery] string UserID )

        {
            try
            {
                var result = await _loginService.GetInvestorEmailIDData(UserID);
                return Ok(JsonConvert.SerializeObject(result));

            }
            catch (CustomException.InvalidValue ex)
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
