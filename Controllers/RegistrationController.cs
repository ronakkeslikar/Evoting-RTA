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
    [Route("api/Registration")]
    [Produces("application/json")]
    [ApiController]
     
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService; 
        }
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public async Task<IActionResult> RegistrationSave(FJC_Registration fJC_Registration)
        { 
            try
            {   
                 if(fJC_Registration.reg_type_id == 1 || fJC_Registration.reg_type_id == 2 )
                {
                  fJC_Registration.panid="XXXXX0000X";  
                } 

                if(fJC_Registration.reg_type_id != 3)
                {
                    if(!Regex.IsMatch(fJC_Registration.reg_no, @"^[a-zA-Z0-9 -,_]*$"))
                    {
                        throw new CustomException.InvalidValue();
                    }
                }                               
                var result = await _registrationService.Registration_InsertData(fJC_Registration);               
                  return Ok(Reformatter.Response_Object("UserID: "+ result.Rows[0][0] + " generated. New Registration completed Successfully", ref result));              
            }
             catch (Exception ex)
            {
                return (new HandleCatches()).ManageExceptions(ex);
            }           
        }
        
    }
}
