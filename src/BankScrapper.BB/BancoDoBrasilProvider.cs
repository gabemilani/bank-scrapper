using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilProvider : IBankProvider
    {
        private readonly IBancoDoBrasilRepository _repository;

        public BancoDoBrasilProvider(IBancoDoBrasilRepository repository) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Dispose() => _repository.Dispose();

        public async Task<BankScrapeResult> GetResultAsync()
        {
            var loginResult = await _repository.GetLoginAsync();

            var balance = await _repository.GetBalanceAsync();

            //await _repository.GetOtherData();

            var segment = loginResult.Segmento;
            var type = AccountType.Unknown;

            if (segment.EqualsIgnoreCase("PESSOA_FISICA"))
                type = AccountType.Natural;
            else if (segment.EqualsIgnoreCase("PESSOA_JURIDICA"))
                type = AccountType.Legal;

            var account = new Account
            {
                Agency = loginResult.DependenciaOrigem,
                Number = loginResult.NumeroContratoOrigem,
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