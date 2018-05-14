using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilProvider : BankProvider
    {
        private readonly BancoDoBrasilApi _repository;

        public BancoDoBrasilProvider(BancoDoBrasilApi repository, BancoDoBrasilConnectionData connectionData) 
            : base(Bank.BancoDoBrasil, connectionData)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public static BancoDoBrasilProvider New(BancoDoBrasilConnectionData connectionData)
        {
            return new BancoDoBrasilProvider(
                new BancoDoBrasilApi(),
                connectionData);
        }

        public override void Dispose() => _repository.Dispose();

        public override async Task<BankScrapeResult> GetResultAsync()
        {
            var bbConnectionData = ConnectionData as BancoDoBrasilConnectionData;

            var loginResult = await _repository.LoginAsync(
                bbConnectionData.Agency,
                bbConnectionData.Account,
                bbConnectionData.ElectronicPassword);

            var balance = await _repository.GetBalanceAsync();

            await _repository.GetOtherData();

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

            var customer = loginResult.NomeCliente.IsNullOrEmpty() 
                ? null 
                : new Customer { Name = loginResult.NomeCliente };

            var extractLayout = await _repository.GetExtractLayoutAsync();
            var sessions = extractLayout?.Container?.Telas?.FirstOrDefault()?.Sessoes;
            if (sessions?.Any() == true)
            {
                foreach (var session in sessions.Where(s => s.Tipo.EqualsIgnoreCase("sessao")))
                {
                    
                }
            }

            return new BankScrapeResult
            {
                Account = account,
                Customer = customer
            };
        }
    }
}