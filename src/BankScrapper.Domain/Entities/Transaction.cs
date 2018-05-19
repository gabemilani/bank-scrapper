using BankScrapper.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Account Account { get; set; }

        public int AccountId { get; set; }

        public double Amount { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }

        public int Id { get; set; }
    }
}