using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaintServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.Controllers
{
    [ApiController]
    [Route("pictures")]
    public class PicturesController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        BusinessLogic _bl;

        public PicturesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _bl = BusinessLogic.Create();
        }

        [HttpPost]
        [Route("savepicture")]
        public IActionResult Registration([FromBody] PictureData picture)
        {
            _bl.AddPicture(picture);
            return Ok("Picture added successfully");
        }

    }
}
