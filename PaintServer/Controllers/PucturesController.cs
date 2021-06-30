using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaintServer.Entities;
using System;
using System.Collections;
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

        [HttpGet]
        [Route("userid")]
        public IActionResult GetPictures([FromQuery] int userId)
        {
            PictureData[] pictures = _bl.GetPictures(userId);
            List<PictureData> res = new List<PictureData>();
            foreach(PictureData p in pictures)
            {
                res.Add(p);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("pictureById")]
        public IActionResult GetPicture([FromQuery] int userId, [FromQuery] string pictureName)
        {
            PictureData picture = _bl.GetPicture(userId, pictureName);
          
            return Ok(picture);
        }


    }
}
