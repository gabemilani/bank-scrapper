using BankScrapper.Utils;

namespace BankScrapper.Nubank
{
    public sealed class NubankConnectionData : IBankConnectionData
    {
        public string CPF { get; set; }

        public string Password { get; set; }

        public bool IsValid() => !CPF.IsNullOrEmpty() && !Password.IsNullOrEmpty();
    }
}