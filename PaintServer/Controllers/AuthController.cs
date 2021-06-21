using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaintServer.Database;
using PaintServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        BusinessLogic _bl;

        public AuthController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _bl = BusinessLogic.Create();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Registration([FromBody] NewUserData newUserData)
        {
            NewUserData userData;
            userData = _bl.CreateUser(newUserData);
            return Ok(userData);
        }
    }
}
