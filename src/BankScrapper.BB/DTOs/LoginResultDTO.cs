using Newtonsoft.Json;

namespace BankScrapper.BB.DTOs
{
    [JsonObject(IsReference = false)]
    public sealed class LoginResultDTO
    {
        [JsonProperty("login")]
        public LoginDTO Login { get; set; }
    }
}