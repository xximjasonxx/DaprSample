using Dapr.Client;
using MovieFlicksApi.Models;
using Newtonsoft.Json;

namespace MovieFlicksApi.Services
{
    public class GetUserAccountService
    {
        private readonly ILogger<GetUserAccountService> _logger;
        private readonly IConfiguration _configuration;
        //private readonly DaprClient _daprClient;

        public GetUserAccountService(IConfiguration configuration, ILogger<GetUserAccountService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            //_daprClient = daprClient;
        }

        public async Task<GetAccountResponseModel> GetUserAccount(int accountId)
        {
            using var client = DaprClient.CreateInvokeHttpClient();

            var response = await client.GetAsync($"http://accountapi/account/{accountId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogInformation(responseContent);
                throw new Exception(responseContent);
            }

            _logger.LogInformation("Sending back object");
            return JsonConvert.DeserializeObject<GetAccountResponseModel>(responseContent);
        }
    }
}
