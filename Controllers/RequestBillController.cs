using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
using evoting.Services;
using evoting.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace evoting.Controllers
{
    [Route("api/event/bill")]
    [Produces("application/json")]
    [ApiController]
    public class RequestBillController : ControllerBase
    {
            private readonly IRequestBillService _requestBillService;

            public RequestBillController(IRequestBillService requestBillService)
            {
                _requestBillService = requestBillService;
            }

            [Authorize]
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> RequestBillSave(FJC_RequestBill fjc_RequestBill)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                    var result = await _requestBillService.RequestBill_data(fjc_RequestBill, Token);                    
                    return Ok(Reformatter.Response_Object("Date Updated Successfully", ref result));
                   
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }

            [Authorize]
            [HttpGet("pending")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> RequestBill_GetPending(int id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                    var result = await _requestBillService.GetRequestBillData(Token, id,1);
                    return Ok(Reformatter.Response_ArrayObject("Pending request bill Record retrieved Successfully", ref result));    
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }

            [Authorize]
            [HttpGet("completed")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> RequestBill_GetCompleted(int id)
            {
                try
                {
                    var identity = (ClaimsIdentity)User.Identity;  
                    var Token = Token_Handling.Get_Token_FromHeader(Request.Headers,identity); 
                    var result = await _requestBillService.GetRequestBillData(Token, id,2);
                    return Ok(Reformatter.Response_ArrayObject("Completed request bill Record retrieved Successfully", ref result));    
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
    }
}
