using Newtonsoft.Json.Linq;

namespace BankScrapper.Web.Models
{
    public class BankConnectionInputModel
    {
        public Bank Bank { get; set; }

        public JToken ConnectionData { get; set; }
    }
}