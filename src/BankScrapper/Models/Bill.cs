using BankScrapper.Enums;
using System;

namespace BankScrapper.Models
{
    public class Bill
    {
        public DateTime CloseDate { get; set; }

        public DateTime OpenDate { get; set; }

        public double Total { get; set; }

        public BillState State { get; set; }
    }
}