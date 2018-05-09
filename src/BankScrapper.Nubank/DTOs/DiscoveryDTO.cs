using Newtonsoft.Json;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public sealed class DiscoveryDTO
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}