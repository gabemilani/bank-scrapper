using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    [Collection("Cards")]
    public class Card : IEntity
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public CardType Type { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string PrintedName { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}