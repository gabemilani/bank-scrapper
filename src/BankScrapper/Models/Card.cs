using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Card
    {
        public string Number { get; set; }

        public CardType Type { get; set; }

        public DateTime ExpireDate { get; set; }

        public string PrintedName { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}