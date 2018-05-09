using System.ComponentModel;

namespace BankScrapper.Enums
{
    public enum CardType
    {
        [Description("N/A")]
        Unknown,
        [Description("Crédito")]
        Credit,
        [Description("Débito")]
        Debit
    }
}