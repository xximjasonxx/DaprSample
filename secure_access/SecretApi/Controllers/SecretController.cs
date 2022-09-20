using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace SecretApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SecretController : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name, [FromServices]DaprClient daprClient)
    {
        var secret = await daprClient.GetSecretAsync("azurekeyvault", name);
        return Ok(secret);
    }
}
