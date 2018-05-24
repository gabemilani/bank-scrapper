using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Web.Models.Views
{
    public class BillViewModel
    {
        public DateTime CloseDate { get; set; }

        public DateTime OpenDate { get; set; }

        public double Total { get; set; }

        public BillState State { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}