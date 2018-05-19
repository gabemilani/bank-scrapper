using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Domain.Entities
{
    public class Bill : IEntity
    {
        public int Id { get; set; }

        public DateTime CloseDate { get; set; }

        public DateTime OpenDate { get; set; }

        public double Total { get; set; }

        public BillState State { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }

        public Account Account { get; set; }

        public int AccountId { get; set; }
    }
}