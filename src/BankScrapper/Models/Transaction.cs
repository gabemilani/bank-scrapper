using System;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Transaction
    {
        public string Category { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}