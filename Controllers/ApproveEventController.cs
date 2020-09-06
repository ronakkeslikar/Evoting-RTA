using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    [Route("api/ApproveEvent")]
    [Produces("application/json")]
    [ApiController]
    public class ApproveEventController : ControllerBase
    {
        
       public class ApproveEventController : ControllerBase
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
                    if (fJC_Registration.reg_type_id == 1 || fJC_Registration.reg_type_id == 2)
                    {
                        fJC_Registration.panid = "XXXXXXXX";
                    }
                    var result = await _registrationService.Registration_InsertData(fJC_Registration);
                    return Ok(Reformatter.Response_Object("New Registration completed Successfully", ref result));
                }
                catch (Exception ex)
                {
                    return (new HandleCatches()).ManageExceptions(ex);
                }
            }
        }
}
