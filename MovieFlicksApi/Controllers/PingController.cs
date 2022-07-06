using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFlicksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private static readonly Counter _pingCounter = Metrics.CreateCounter("ping_total", "Number of pings.");

        [HttpGet]
        public IActionResult Get()
        {
            _pingCounter.Inc();
            return Ok("Application is up");
        }
    }
}