using Newtonsoft.Json;

namespace BankScrapper.Nubank.DTOs
{
    [JsonObject(IsReference = false)]
    public class BillsSummaryResultDTO
    {
        [JsonProperty("bills")]
        public BillDTO[] Bills { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class BillDTO
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("summary")]
        public SummaryDTO Summary { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class SummaryDTO
    {
        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("close_date")]
        public string CloseDate { get; set; }

        [JsonProperty("total_balance")]
        public long TotalBalance { get; set; }

        [JsonProperty("paid")]
        public long Paid { get; set; }

        [JsonProperty("minimun_payment")]
        public long MinimunPayment { get; set; }

        [JsonProperty("open_date")]
        public string OpenDate { get; set; }
    }    
}