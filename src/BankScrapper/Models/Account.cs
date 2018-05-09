using BankScrapper.Enums;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Account
    {
        public string Agency { get; set; }

        public double CurrentBalance { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }

        public string Number { get; set; }

        public AccountType Type { get; set; }
    }
}