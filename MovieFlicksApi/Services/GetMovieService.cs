using Dapr.Client;
using MovieFlicksApi.Models;

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
                var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "movieapi", $"movie/{movieId}");
                return await _daprClient.InvokeMethodAsync<GetMovieResponseModel>(request);
            }
            catch (Exception dex)
            {
                _logger.LogError(dex.Message, dex);
                throw;
            }
        }
    }
}
