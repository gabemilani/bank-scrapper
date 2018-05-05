using BankScrapper.Enums;

namespace BankScrapper.Models
{
    public class Account
    {
        public string Agency { get; set; }

        public double CurrentBalance { get; set; }

        public string CustomerName { get; set; }

        public int HoldershipLevel { get; set; }

        public string Number { get; set; }

        public PersonType PersonType { get; set; }
    }
}