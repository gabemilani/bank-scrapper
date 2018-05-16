using BankScrapper.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }

        public Category Category { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}