using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace TwitterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TweetController : ControllerBase
{
    private readonly ILogger<TweetController> _logger;

    public TweetController(ILogger<TweetController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{query}")]
    public async Task<IActionResult> Get(string query, [FromServices]DaprClient daprClient)
    {
        var tweets = await daprClient.InvokeBindingAsync<string, IList<Tweet>>("twitter-binding", "get", string.Empty,
            new Dictionary<string, string>
            {
                { "query", query },
                { "lang", "en" },
                { "result", "recent" }
            });

        return Ok(tweets);
    }
}
