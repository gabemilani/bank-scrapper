using BankScrapper.Enums;
using System.Collections.Generic;

namespace BankScrapper.Web.Models.Views
{
    public class CardViewModel
    {
        public string Number { get; set; }

        public CardType Type { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string PrintedName { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}