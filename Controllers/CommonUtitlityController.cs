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
                     Dictionary<string, string> dictCommon = new Dictionary<string, string>()
                        {
                            {"COMPANY","C"},
                            {"RTA","R"},
                            {"SCRUTINIZER","Z"},//bind PCS_NO with User Name from Registration
                             {"CUSTODIAN","N"},
                            {"Evoting_Types","T"},
                            {"ISIN_Types","I"},
                            {"All_Events","E"}
                        };                                                 
                result = await _commonUtitlityService.GetCommonDetails(dictCommon[str],Token);
                return Ok(Reformatter.Response_Object("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
