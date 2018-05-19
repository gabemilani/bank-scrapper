using BankScrapper.Models;

namespace BankScrapper
{
    public class BankScrapeResult
    {
        public Bank Bank { get; set; }

        public Account Account { get; set; }

        public Customer Customer { get; set; }

        public Transaction[] Transactions { get; set; }

        public Card[] Cards { get; set; }
    }
}