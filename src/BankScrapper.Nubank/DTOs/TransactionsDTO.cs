using Newtonsoft.Json;
using System;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public class TransactionsResultDTO
    {
        [JsonProperty("transactions")]
        public TransactionDTO[] Transactions { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class TransactionDTO
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("capture_mode")]
        public CaptureModeDTO CaptureMode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("merchant_name")]
        public string MerchantName { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class CaptureModeDTO
    {
        [JsonProperty("entry_mode")]
        public string EntryMode { get; set; }

        [JsonProperty("pin_mode")]
        public string PinMode { get; set; }
    }
}