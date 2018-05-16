using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Models
{
    public class Bill
    {
        public DateTime CloseDate { get; set; }

        public DateTime OpenDate { get; set; }

        public double Total { get; set; }

        public BillState State { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}