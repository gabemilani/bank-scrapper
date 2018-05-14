using Newtonsoft.Json;
using System;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public sealed class AccountBalanceDTO
    {
        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("due")]
        public long Due { get; set; }

        [JsonProperty("future")]
        public long Future { get; set; }

        [JsonProperty("open")]
        public long Open { get; set; }

        [JsonProperty("prepaid")]
        public long Prepaid { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class AccountDTO
    {
        [JsonProperty("available_balance")]
        public long AvailableBalance { get; set; }

        [JsonProperty("balances")]
        public AccountBalanceDTO Balances { get; set; }

        [JsonProperty("cards")]
        public CardDTO[] Cards { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("credit_limit")]
        public long CreditLimit { get; set; }

        [JsonProperty("current_balance")]
        public long CurrentBalance { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class AccountResultDTO
    {
        [JsonProperty("account")]
        public AccountDTO Account { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class AccountSimpleDTO
    {
        public CardDTO[] Cards { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class AccountSimpleResultDTO
    {
        public AccountSimpleDTO Account { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class CardDTO
    {
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("good_through")]
        public string GoodThrough { get; set; }

        [JsonProperty("printed_name")]
        public string PrintedName { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}