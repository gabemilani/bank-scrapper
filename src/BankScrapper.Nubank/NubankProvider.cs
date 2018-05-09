using BankScrapper.Models;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankProvider : BankProvider
    {
        public NubankProvider(NubankConnectionData connectionData) 
            : base(Bank.Nubank, connectionData)
        {

        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override Task<BankScrapeResult> GetResultAsync()
        {
            throw new NotImplementedException();
        }
    }
}