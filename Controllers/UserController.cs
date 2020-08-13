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

namespace evoting.Controllers
{
    [Route("/api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : Controller
    {
        
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string GetAllAsync()
        {
            //var result = await _userService.GetUserDataAsync();
            //var result = await _userService.RenameUsers();
            return   JsonConvert.SerializeObject("");
        }

        
    }
}
