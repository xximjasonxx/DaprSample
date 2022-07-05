using Dapr.Client;
using MovieFlicksApi.Models;
using Newtonsoft.Json;

namespace MovieFlicksApi.Services
{
    public class GetMovieService
    {
        private ILogger<GetMovieService> _logger;
        private readonly IConfiguration _configuration;

        public GetMovieService(IConfiguration configuration, ILogger<GetMovieService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GetMovieResponseModel> GetMovie(int movieId)
        {
            using var client = DaprClient.CreateInvokeHttpClient();

            var response = await client.GetAsync($"http://movieapi/movie/{movieId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogInformation(responseContent);
                throw new Exception(responseContent);
            }

            _logger.LogInformation("Sending back object");
            return JsonConvert.DeserializeObject<GetMovieResponseModel>(responseContent);
        }
    }
}
