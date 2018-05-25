using System;
using System.Collections.Generic;

namespace BankScrapper.Web.Models.Views
{
    public class CustomerViewModel
    {
        public string Address { get; set; }

        public string BillingAddress { get; set; }

        public string Cpf { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public Dictionary<string, string> ExtraInformation { get; set; }
    }
}