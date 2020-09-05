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
    [Route("api/private-list")]
    [Produces("application/json")]
    [ApiController]
     
    public class PrivateListController : ControllerBase
    {

        private readonly IPrivateListService _privateListService;

        public PrivateListController(IPrivateListService privateListService)
        {
            _privateListService = privateListService; 
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
                            {"COMPANY_Event_LIST","CEL"},                           
                            {"SCRUTINIZER","Z"},//bind PCS_NO with User Name from Registration  
                            {"UploadDocList","UDL"},                            
                                                   
                        };                                                 
                result = await _privateListService.Getprivate_List_Details(dictCommon[str],Token);
                return Ok(Reformatter.Response_Object("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
