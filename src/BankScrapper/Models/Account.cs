using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Account
    {
        public DateTime? CreationDate { get; set; }

        public string Agency { get; set; }

        public double CurrentBalance { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string Number { get; set; }

        public AccountType Type { get; set; }
    }
}