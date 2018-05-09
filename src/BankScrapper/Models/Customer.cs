using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Customer
    {
        public Address Address { get; set; }

        public Address BillingAddress { get; set; }

        public string CPF { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}