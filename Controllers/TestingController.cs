﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evoting.Domain.Models;
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
        [HttpGet]
        public string GetByClassName([FromQuery] string fjc_Name)
        {
            var result =  _TestingService.GetClassToJSON(fjc_Name);
            return result;
        }

        [HttpPost]
        public string CheckResolutionJSON(FJC_CompanyUpdate_Event fJC_ )
        {
            return "okay";
        }
        
    }
}
