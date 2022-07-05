using Microsoft.AspNetCore.Mvc;
using MovieFlicksApi.Models;
using MovieFlicksApi.Services;

namespace MovieFlicksApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly GetMovieService _getMovieService;
    private readonly GetUserAccountService _getUserAccountService;

    public UserController(ILogger<UserController> logger, GetMovieService getMovieService,
        GetUserAccountService getUserAccountService)
    {
        _logger = logger;
        _getUserAccountService = getUserAccountService;
        _getMovieService = getMovieService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("Request received");

        var accountResponse = await _getUserAccountService.GetUserAccount(id);
        if (accountResponse == null)
            return NotFound();

        var response = new User
        {
            Id = accountResponse.Id,
            FirstName = accountResponse.FirstName,
            LastName = accountResponse.LastName,
            MovieWatched = new List<Movie>()
        };

        foreach (var movieId in accountResponse.MoviesWatched)
        {
            var movieResponse = await _getMovieService.GetMovie(movieId);
            if (movieResponse != null)
            {
                response.MovieWatched.Add(new Movie
                {
                    Id = movieResponse.Id,
                    Title = movieResponse.Title
                });
            }
        }

        return Ok(response);
    }
}
