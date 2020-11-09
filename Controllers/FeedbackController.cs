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
    [Route("api/feedback")]
    [Produces("application/json")]
    [ApiController]
     
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService; 
        }       
        
         [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> FeedbackSave(FJC_Feedback fjc_feedback)
        {
            try
            { 
                var result=  await _feedbackService.Feedback_Details(fjc_feedback);
                return Ok(Reformatter.Response_Object("Feedback Submitted successfully", ref result));  
            }
            catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }
            
        }
    }
}
