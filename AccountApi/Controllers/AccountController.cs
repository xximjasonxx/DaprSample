using AccountApi.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly DaprClient _daprClient;

    private readonly IList<Account> _accounts = new List<Account>
    {
        new Account { Id = 1, FirstName = "Test", LastName = "User 1", MoviesWatched = new int[] { 1, 2, 3, 4 } },
        new Account { Id = 2, FirstName = "Test", LastName = "User 2", MoviesWatched = new int[] { 5, 6, 7, 8 } },
        new Account { Id = 3, FirstName = "Test", LastName = "User 3", MoviesWatched = new int[] { 9, 10, 1, 2 } },
        new Account { Id = 4, FirstName = "Test", LastName = "User 4", MoviesWatched = new int[] { 3, 4, 5, 6 } },
        new Account { Id = 5, FirstName = "Test", LastName = "User 5", MoviesWatched = new int[] { 7, 8, 9, 10 } },
        new Account { Id = 6, FirstName = "Test", LastName = "User 6", MoviesWatched = new int[] { 2, 3, 4, 5 } },
        new Account { Id = 7, FirstName = "Test", LastName = "User 7", MoviesWatched = new int[] { 8, 7, 9, 1 } },
        new Account { Id = 8, FirstName = "Test", LastName = "User 8", MoviesWatched = new int[] { 10, 2, 6, 7 } },
        new Account { Id = 9, FirstName = "Test", LastName = "User 9", MoviesWatched = new int[] { 9, 2, 5, 8 } },
    };

    public AccountController(ILogger<AccountController> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var account = await _daprClient.GetStateAsync<Account>("account-statestore", $"account-{id}");
        if (account == null)
        {
            // simulate a heavy lookup operation
            await Task.Delay((int)TimeSpan.FromSeconds(3).TotalMilliseconds);

            account = _accounts.FirstOrDefault(x => x.Id == id);
            await _daprClient.SaveStateAsync("account-statestore", $"account-{id}", account);
        }

        if (account == null)
            return NotFound();

        return Ok(account);
    }
}
