using System.ComponentModel;

namespace BankScrapper
{
    public enum Bank
    {
        [Description("N/A")]
        Unknown,
        [Description("Banco do Brasil")]
        BancoDoBrasil,
        [Description("Nubank")]
        Nubank
    }
}