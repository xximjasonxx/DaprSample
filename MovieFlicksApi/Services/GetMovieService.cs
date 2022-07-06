using Dapr;
using Dapr.Client;
using MovieFlicksApi.Models;
using Newtonsoft.Json;

namespace MovieFlicksApi.Services
{
    public class GetMovieService
    {
        private readonly ILogger<GetMovieService> _logger;
        private readonly IConfiguration _configuration;
        private readonly DaprClient _daprClient;

        public GetMovieService(IConfiguration configuration, ILogger<GetMovieService> logger, DaprClient daprClient)
        {
            _configuration = configuration;
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<GetMovieResponseModel> GetMovie(int movieId)
        {
            try
            {
                var result = await _daprClient.GetStateAsync<GetMovieResponseModel>("statestore", $"movie-{movieId}");
                _logger.LogInformation(result?.ToString());

                if (result == null)
                {
                    var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "movieapi", $"movie/{movieId}");
                    result = await _daprClient.InvokeMethodAsync<GetMovieResponseModel>(request);

                    await _daprClient.SaveStateAsync("statestore", $"movie-{movieId}", result);
                }

                return result;
            }
            catch (Exception dex)
            {
                do
                {
                    _logger.LogInformation(dex.Message);
                    dex = dex.InnerException;
                } while (dex != null);

                throw;
            }
        }
    }
}
