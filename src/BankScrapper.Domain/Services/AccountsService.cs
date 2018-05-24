using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Exceptions;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services
{
    public sealed class AccountsService : EntitiesService<Account>, IService
    {
        public AccountsService(IContext context) : base(context)
        {
        }

        protected override async Task ValidateAsync(Account account, bool isNew = true)
        {
            await base.ValidateAsync(account, isNew);

            if (account.Bank == Bank.Unknown)
                throw new ValidationException<Account>("O banco da conta precisa ser informado");

            if (account.Type == AccountType.Unknown)
                throw new ValidationException<Account>("O tipo da conta precisa ser informado");
        }
    }
}