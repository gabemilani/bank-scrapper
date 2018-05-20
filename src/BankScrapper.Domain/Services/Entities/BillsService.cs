using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Services.Entities
{
    public sealed class BillsService : EntitiesService<Bill>, IService
    {
        public BillsService(IContext context) : base(context)
        {
        }
    }
}