using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Services.Entities
{
    public sealed class TransactionsService : EntitiesService<Transaction>, IService
    {
        public TransactionsService(IContext context) : base(context)
        {
        }
    }
}