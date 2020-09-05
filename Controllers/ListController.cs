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
    [Route("api/list")]
    [Produces("application/json")]
    [ApiController]
     
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService; 
        }       
        
         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> GetList([FromQuery] string str)
        {
            try
            { 
                var result=(DataTable)null;                 
                Dictionary<string, string> dictCommon = new Dictionary<string, string>()
                {
                    {"COMPANY","C"},
                    {"rta","R"},
                    {"SCRUTINIZER","Z"},//bind PCS_NO with User Name from Registration
                        {"CUSTODIAN","N"},
                    {"Evoting_Types","T"},
                    {"isin_type","I"},
                    {"events","E"},
                    {"state","ST"},
                    {"country","CN"},
                    {"evoting_type","ET"},
                    {"UnapprovedEvents","UAE"}                            
                };                                                 
                result = await _listService.GetList_Details(dictCommon[str]);
                return Ok(Reformatter.Response_Object("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
