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
    [Route("api/CommonUtility")]
    [Produces("application/json")]
    [ApiController]
     
    public class CommonUtitlityController : ControllerBase
    {
        private readonly ICommonUtilityService _commonUtitlityService;

        public CommonUtitlityController(ICommonUtilityService commonUtitlityService)
        {
            _commonUtitlityService = commonUtitlityService; 
        }       
        
         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetCommonUtitlity([FromQuery] string str)
        {
            try
            {   
                var Token = Token_Handling.Get_Token_FromHeader(Request.Headers);
                var result=(DataTable)null;
                str=str.ToUpper();
                if(str=="RTA" || str=="SCRUTINIZER" || str=="COMPANY" || str=="CUSTODIAN")
                {
                    switch(str)
                    {
                        case "COMPANY":
                        result = await _commonUtitlityService.GetCommonUtilityAudienceID(1,Token);
                        break;
                        case "RTA":
                        result = await _commonUtitlityService.GetCommonUtilityAudienceID(2,Token);
                        break;
                         case "SCRUTINIZER":
                        result = await _commonUtitlityService.GetCommonUtilityAudienceID(3,Token);
                        break;
                         case "CUSTODIAN":
                        result = await _commonUtitlityService.GetCommonUtilityAudienceID(4,Token);
                        break;
                    }                     
                }
                else
                {
                     switch(str)
                    { 
                        case "DEBENTURES":
                        result = await _commonUtitlityService.GetCommonUtilityISINID(3,Token);
                        break;
                        case "EQUITY":
                         result = await _commonUtitlityService.GetCommonUtilityISINID(1,Token);
                        break;
                         case "PREFERENTIAL":
                        result = await _commonUtitlityService.GetCommonUtilityISINID(2,Token);
                        break;                        
                    }                   
                }
                return Ok(Reformatter.Response_Object("Common Details retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
