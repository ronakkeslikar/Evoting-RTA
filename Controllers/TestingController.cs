using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evoting.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly ITestingService _TestingService;

        public TestingController(ITestingService TestService)
        {
            _TestingService = TestService;
        }
        [HttpGet("fjc_Name/{fjc_Name}")]
        public string GetByClassName([FromQuery] string fjc_Name)
        {
            var result =  _TestingService.GetClassToJSON(fjc_Name);
            return result;
        }

        [HttpGet]
        public string GetByClassName()
        {
            var result = _TestingService.DBCheck();
            return result;
        }
    }
}
