using BankScrapper.Utils;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilConnectionData : IBankConnectionData
    {
        public string Agency { get; set; }

        public string Account { get; set; }

        public string ElectronicPassword { get; set; }

        public int HoldershipLevel { get; set; }

        public bool IsValid()
        {
            return !Agency.IsNullOrEmpty()
                && !Account.IsNullOrEmpty()
                && !ElectronicPassword.IsNullOrEmpty()
                && HoldershipLevel > 0;
        }
    }
}