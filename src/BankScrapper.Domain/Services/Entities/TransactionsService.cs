using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class TransactionsService : EntitiesService<Transaction>, IService
    {
        public TransactionsService(IContext context) : base(context)
        {
        }

        protected override async Task ValidateAsync(Transaction transaction, bool isNew = true)
        {
            await base.ValidateAsync(transaction, isNew);

            if (transaction.Account == null && transaction.AccountId == 0)
                throw new ValidationException<Transaction>("A conta da transação precisa ser informada");

            if (transaction.Date == default(DateTime))
                throw new ValidationException<Transaction>("A data da transação precisa ser informada");

            if (transaction.Amount == 0)
                throw new ValidationException<Transaction>("O valor da transação precisa ser informado");
        }
    }
}