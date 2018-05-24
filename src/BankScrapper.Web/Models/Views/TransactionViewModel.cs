using System;
using System.Collections.Generic;

namespace BankScrapper.Web.Models.Views
{
    public class TransactionViewModel
    {
        public double Amount { get; set; }

        public string CategoryName { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}