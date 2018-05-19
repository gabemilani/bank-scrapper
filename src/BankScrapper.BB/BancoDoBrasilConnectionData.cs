using BankScrapper.Utils;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilConnectionData : IBankConnectionData
    {
        public string Agency { get; set; }

        public string Account { get; set; }

        public string ElectronicPassword { get; set; }

        public Bank Bank => Bank.BancoDoBrasil;

        public bool IsValid()
        {
            if (Agency?.Length != 6)
                return false;

            if (Account.IsNullOrEmpty())
                return false;

            if (ElectronicPassword?.Length != 8)
                return false;

            return Agency.Contains("-") && Account.Contains("-");
        }
    }
}