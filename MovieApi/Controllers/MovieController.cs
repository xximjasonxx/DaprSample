using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using System;

namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly DaprClient _daprClient;

    private readonly IList<Movie> _movies = new List<Movie>
    {
        new Movie { Id = 1, Title = "Hancock", Genre = "Action" },
        new Movie { Id = 2, Title = "Star Trek: Into Darkness", Genre = "Sci-Fi" },
        new Movie { Id = 3, Title = "Your Name", Genre = "Anime" },
        new Movie { Id = 4, Title = "Dr. Strange: In the Multiverse of Madness", Genre = "Action" },
        new Movie { Id = 5, Title = "Avengers: Endgame", Genre = "Action" },
        new Movie { Id = 6, Title = "Grant", Genre = "Historical" },
        new Movie { Id = 7, Title = "12 Kindgdoms", Genre = "Anime" },
        new Movie { Id = 8, Title = "Sing 2", Genre = "Drama" },
        new Movie { Id = 9, Title = "Avatar", Genre = "Fantasy" },
        new Movie { Id = 10, Title = "Avengers: Infinity War", Genre = "Action" }
    };

    public MovieController(ILogger<MovieController> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movie = await _daprClient.GetStateAsync<Movie>("movie-statestore", $"movie-{id}");
        if (movie == null)
        {
            // simulate a heavy lookup operation
            await Task.Delay((int)TimeSpan.FromSeconds(2).TotalMilliseconds);

            movie = _movies.FirstOrDefault(x => x.Id == id);
            await _daprClient.SaveStateAsync("movie-statestore", $"movie-{id}", movie);
        }

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }
}
