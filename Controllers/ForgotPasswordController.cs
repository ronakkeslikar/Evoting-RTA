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
using System.Text.RegularExpressions;

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
            //TypeOfUser attribute
            //Scrutinizer - S, Investor - I, Company - C, Custodian - T, Corporate Shareholder - H, RTA - R
            try
            {                
                var result = (DataTable)null;   
                if(fJC_forgot.TypeOfUser == 'S' || fJC_forgot.TypeOfUser == 'T' || fJC_forgot.TypeOfUser == 'H' 
                            || (fJC_forgot.TypeOfUser == 'I' && fJC_forgot.TypeOfUpdate!='E'  ))
                {
                    if(!Regex.IsMatch(fJC_forgot.PAN_ID, @"^[a-zA-Z0-9]*$"))
                    {
                        throw new CustomException.InvalidPanPattern();
                    }
                }
                else if(fJC_forgot.TypeOfUser == 'C' || fJC_forgot.TypeOfUser == 'R')
                {
                    fJC_forgot.PAN_ID = "XXXXXXXX";
                }
                         
                
                    if(fJC_forgot.TypeOfUser=='I')
                    { 
                    
                        switch (fJC_forgot.TypeOfUpdate)
                        {
                            case 'D': //Date of bit=rth cases                         
                                result = await _loginService.ForgotPassword_DOB_Data(fJC_forgot);
                                break;                                          
                            case 'B': //Bank Account cases
                                result = await _loginService.ForgotPassword_BANK_ACC_Data(fJC_forgot);
                                 break;
                            case 'E': //Email Cases                                 
                                 result = await _loginService.ForgotPasswordData(fJC_forgot);
                                break;
                        }
                    }
                    else
                    {                        
                       result = await _loginService.ForgotPasswordData(fJC_forgot);  
                    } 
               return Ok(Reformatter.Response_Object("Password reset successfully", ref result)); 
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
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
                 return Ok(Reformatter.Response_Object("Email ID retrieved successfully", ref result)); 
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            } 
            
        }
    }
}
