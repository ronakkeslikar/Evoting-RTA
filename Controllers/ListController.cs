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
                var result=  await _listService.GetList_Details(str);
                return Ok(Reformatter.Response_Object("Records retrieved successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
