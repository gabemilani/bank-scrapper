using System;
using System.Threading.Tasks;
using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Utils;

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

        public override async Task<Account> GetAccountAsync()
        {
            var bbConnectionData = ConnectionData as BancoDoBrasilConnectionData;

            var loginResult = await _api.LoginAsync(
                bbConnectionData.Agency,
                bbConnectionData.Account,
                bbConnectionData.ElectronicPassword,
                bbConnectionData.HoldershipLevel);

            var balance = await _api.GetBalanceAsync();

            var segment = loginResult?.Login?.Segmento;
            var personType = PersonType.Unknown;

            if (segment.EqualsIgnoreCase("PESSOA_FISICA"))
                personType = PersonType.Natural;
            else if (segment.EqualsIgnoreCase("PESSOA_JURIDICA"))
                personType = PersonType.Legal;

            return new Account
            {
                Agency = bbConnectionData.Agency,
                Number = bbConnectionData.Account,
                HoldershipLevel = bbConnectionData.HoldershipLevel,
                CustomerName = loginResult?.Login?.NomeCliente,
                PersonType = personType,
                CurrentBalance = balance
            };
        }

        public override Task<Transaction[]> GetTransactionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}