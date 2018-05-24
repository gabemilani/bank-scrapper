using BankScrapper.Enums;
using System;
using System.Collections.Generic;

namespace BankScrapper.Web.Models.Views
{
    public class AccountViewModel
    {
        public string Agency { get; set; }

        public Bank Bank { get; set; }

        public BillViewModel[] Bills { get; set; }

        public CardViewModel[] Cards { get; set; }

        public DateTime? CreationDate { get; set; }

        public double? CurrentBalance { get; set; }

        public CustomerViewModel Customer { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }

        public string Number { get; set; }

        public TransactionViewModel[] Transactions { get; set; }

        public AccountType Type { get; set; }
    }
}