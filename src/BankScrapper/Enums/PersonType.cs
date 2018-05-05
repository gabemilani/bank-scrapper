using System.ComponentModel;

namespace BankScrapper.Enums
{
    public enum PersonType
    {
        [Description("N/A")]
        Unknown,
        [Description("Pessoa Física")]
        Natural,
        [Description("Pessoa Jurídica")]
        Legal
    }
}