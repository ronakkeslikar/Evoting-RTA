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
    [Route("api/account")]
    [Produces("application/json")]
    [ApiController]
     
    public class AccountSearchController : ControllerBase
    {

        private readonly IAccountSearchService _accountSearchService;

        public AccountSearchController(IAccountSearchService AccountSearchService)
        {
            _accountSearchService = AccountSearchService; 
        }       
        
        [Authorize]
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> AccountSearch(FJC_AccountSearch fJC_AccountSearch)
        {
            try
            { 
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);
                var result = await _accountSearchService.GetSearch_Details(fJC_AccountSearch,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }

        [Authorize]
        [HttpPost("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> VerifyAccount([FromQuery] int aud_id)
        {
            try
            {                                               
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                                                  
                var result = await _accountSearchService.Verify_AccountData(aud_id,Token);
                return Ok(Reformatter.Response_Object("Account Verified Successfully", ref result));  
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
        public async Task<IActionResult> GetAudienceSearch([FromQuery] int aud_id  )
        {
            try
            {   
                var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                var  result = await _accountSearchService.GetAudience_Details(aud_id,Token);
                return Ok(Reformatter.Response_Object("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }

        [Authorize]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetAccountList([FromQuery] int user_type  )
        {
            try
            {   
               var identity = (ClaimsIdentity)User.Identity;  
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity);                                                 
               var result = await _accountSearchService.GetAccountList_Details(user_type,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
