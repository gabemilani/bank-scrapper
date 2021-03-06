﻿using Newtonsoft.Json;

namespace BankScrapper.BB.DTOs
{
    [JsonObject(IsReference = false)]
    public class BalanceResultDTO
    {
        [JsonProperty("servicoSaldo")]
        public BalanceDTO ServicoSaldo { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class BalanceDTO
    {
        [JsonProperty("saldo")]
        public string Saldo { get; set; }
    }
}