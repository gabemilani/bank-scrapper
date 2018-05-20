using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Services.Entities
{
    public sealed class AccountsService : EntitiesService<Account>, IService
    {
        public AccountsService(IContext context) : base(context)
        {
        }
    }
}