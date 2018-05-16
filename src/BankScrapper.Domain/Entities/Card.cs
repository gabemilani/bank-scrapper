using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    public class Card : IEntity
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public CardType Type { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string PrintedName { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}