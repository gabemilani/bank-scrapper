using BankScrapper.Models;
using System.Collections.Generic;

namespace BankScrapper
{
    public class BankScrapeResult
    {
        public Account Account { get; set; }

        public Customer Customer { get; set; }

        public Transaction[] Transactions { get; set; }

        public Card[] Cards { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}