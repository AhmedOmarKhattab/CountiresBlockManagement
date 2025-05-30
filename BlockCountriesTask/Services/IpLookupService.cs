using System.Net;
using BlockCountriesTask.Dtos;
using BlockCountriesTask.IServices;
using System.Text.Json;
namespace BlockCountriesTask.Services
{
    public class IpLookupService : IIpLookupService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public IpLookupService(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<IpLookupResultDto> LookupIpAsync(string ipAddress)
        {
            var client = _httpClientFactory.CreateClient();
            if (string.IsNullOrWhiteSpace(ipAddress) || IsLocalIp(ipAddress))
            {
                ipAddress = await client.GetStringAsync("https://api.ipify.org");
            }


            if (!IsValidIp(ipAddress))
            {
                throw new ArgumentException("Invalid IP address.");
            }

          
            var apiKey = _configuration["IpApiKey"];
            var url = $"https://ipapi.co/{ipAddress}/json/?Key={apiKey}";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                 $"IP API request failed. Status: {response.StatusCode}. Response: {content}");
            }

          //  var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IpLookupResultDto>(content);

            return result;
        }
        private bool IsLocalIp(string ip)
        {
            return ip == "127.0.0.1" || ip == "::1";
        }

       
        private bool IsValidIp(string ip)
        {
            return IPAddress.TryParse(ip, out _);
        }
    }
}
