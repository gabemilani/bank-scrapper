using BankScrapper.BB;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Nubank;
using BankScrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public class BankScrapperService : IService
    {
        private readonly IContext _context;

        public BankScrapperService(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ScrapeBankDataAsync(IBankConnectionData connectionData)
        {
            var bankData = await GetBankDataAsync(connectionData);
            if (bankData == null)
                return;

            if (bankData.Customer != null)
            {

            }

            


        }

        private async Task<BankScrapeResult> GetBankDataAsync(IBankConnectionData connectionData)
        {
            BankScrapeResult result;

            using (var provider = GetBankProvider(connectionData))
            {
                result = await provider.GetResultAsync();
            }

            return result;
        }

        private IBankProvider GetBankProvider(IBankConnectionData connectionData)
        {
            if (connectionData == null)
                throw new Exception("É necessário informar os dados de acesso de algum banco");

            if (connectionData.IsValid())
            {
                switch (connectionData.Bank)
                {
                    case Bank.BancoDoBrasil:
                        if (connectionData is BancoDoBrasilConnectionData bbConnectionData)
                            return new BancoDoBrasilProvider(new BancoDoBrasilApiRepository(bbConnectionData));

                        break;

                    case Bank.Nubank:
                        if (connectionData is NubankConnectionData nubankConnectionData)
                            return new NubankProvider(new NubankApiRepository(nubankConnectionData));

                        break;

                    case Bank.Unknown:
                    default:
                        throw new Exception($"Banco não suportado: {connectionData.Bank.GetDescription()}");
                }
            }

            throw new Exception($"Os dados de acesso ao \"{connectionData.Bank.GetDescription()}\" são inválidos");
        }
    }
}
