using Newtonsoft.Json;

namespace BankScrapper.BB.DTOs
{
    public class BalanceDTO
    {
        [JsonProperty("saldo")]
        public string Saldo { get; set; }
    }
}