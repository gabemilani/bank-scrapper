using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Utils;
using System;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilProvider : BankProvider
    {
        private readonly BancoDoBrasilApi _api;

        public BancoDoBrasilProvider(BancoDoBrasilConnectionData connectionData) 
            : base(Bank.BancoDoBrasil, connectionData)
        {
            _api = new BancoDoBrasilApi();
        }

        public override void Dispose() => _api.Dispose();

        public override async Task<BankScrapeResult> GetResultAsync()
        {
            var bbConnectionData = ConnectionData as BancoDoBrasilConnectionData;

            var loginResult = await _api.LoginAsync(
                bbConnectionData.Agency,
                bbConnectionData.Account,
                bbConnectionData.ElectronicPassword,
                bbConnectionData.HoldershipLevel);

            var balance = await _api.GetBalanceAsync();

            var segment = loginResult.Segmento;
            var type = AccountType.Unknown;

            if (segment.EqualsIgnoreCase("PESSOA_FISICA"))
                type = AccountType.Natural;
            else if (segment.EqualsIgnoreCase("PESSOA_JURIDICA"))
                type = AccountType.Legal;

            var account = new Account
            {
                Agency = bbConnectionData.Agency,
                Number = bbConnectionData.Account,
                Type = type,
                CurrentBalance = balance
            };

            account.ExtraInformation["Titularidade"] = $"{bbConnectionData.HoldershipLevel}o titular";

            var customer = new Customer
            {
                Name = loginResult.NomeCliente
            };


            var extractDTO = await _api.GetExtractAsync();

            return new BankScrapeResult
            {
                Account = account,
                Customer = customer
            };
        }
    }
}