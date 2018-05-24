using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    [Collection("Customers")]
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string BillingAddress { get; set; }

        public string Cpf { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}