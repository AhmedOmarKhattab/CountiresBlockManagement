using System.Text.Json.Serialization;

namespace BlockCountriesTask.Dtos
{
    public class IpLookupResultDto
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("org")]
        public string Org { get; set; }
    }
}
