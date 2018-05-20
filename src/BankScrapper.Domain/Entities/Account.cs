using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    [Collection("Accounts")]
    public class Account : IEntity
    {
        public int Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Agency { get; set; }

        public double? CurrentBalance { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }

        public string Number { get; set; }

        public AccountType Type { get; set; }

        public Bank Bank { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }
    }
}