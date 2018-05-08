using Newtonsoft.Json;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public class SelfLinkDTO
    {
        [JsonProperty("self")]
        public string Self { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class LinkDTO
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}