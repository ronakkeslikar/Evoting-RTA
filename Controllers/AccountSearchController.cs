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
        
         [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> AccountSearch(FJC_AccountSearch fJC_AccountSearch)
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result=(DataTable)null;                
                                                                    
                result = await _accountSearchService.GetSearch_Details(fJC_AccountSearch,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
        [HttpPost("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> VerifyAccount([FromQuery] int aud_id)
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result=(DataTable)null;                
                                                                    
                result = await _accountSearchService.Verify_AccountData(aud_id,Token);
                return Ok(Reformatter.Response_ArrayObject("Account Verified Successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetAudienceSearch([FromQuery] int aud_id  )
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);                               
                                                                    
              var  result = await _accountSearchService.GetAudience_Details(aud_id,Token);
                return Ok(Reformatter.Response_ArrayObject("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetAccountList([FromQuery] int user_type  )
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                             
                                                                    
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
