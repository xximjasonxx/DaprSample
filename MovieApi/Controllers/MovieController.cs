using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using System;

namespace MovieApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{

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

    public MovieController(ILogger<MovieController> logger)
    {
        
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        // simulate a heavy lookup operation
        await Task.Delay((int)TimeSpan.FromSeconds(2).TotalMilliseconds);

        var movie = _movies.FirstOrDefault(x => x.Id == id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }
}
