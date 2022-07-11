using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace TwitterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeUpdateController : ControllerBase
    {
        private readonly ILogger<TimeUpdateController> _logger;

        public TimeUpdateController(ILogger<TimeUpdateController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Get()
        {
            _logger.LogInformation($"Current time is {DateTime.Now.ToString()}");
            return Ok();
        }
    }
}