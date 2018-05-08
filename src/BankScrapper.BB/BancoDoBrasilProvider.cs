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
            var personType = AccountType.Unknown;

            if (segment.EqualsIgnoreCase("PESSOA_FISICA"))
                personType = AccountType.Natural;
            else if (segment.EqualsIgnoreCase("PESSOA_JURIDICA"))
                personType = AccountType.Legal;

            var account = new Account
            {
                Agency = bbConnectionData.Agency,
                Number = bbConnectionData.Account,
                Type = personType,
                CurrentBalance = balance
            };

            if (!string.IsNullOrEmpty(loginResult?.Login?.NomeCliente))
            {
                account.Customer = new Customer
                {
                    Name = loginResult.Login.NomeCliente
                };
            }

            account.ExtraInformation["Titularidade"] = $"{bbConnectionData.HoldershipLevel}o titular";

            return account;
        }

        public override async Task<Transaction[]> GetTransactionsAsync()
        {
            var extractDTO = await _api.GetExtractAsync();

            throw new NotImplementedException();
        }
    }
}