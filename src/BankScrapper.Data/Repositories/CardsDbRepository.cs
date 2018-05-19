using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class CardsDbRepository : BaseDbRepository<Card>, ICardsRepository
    {
        public CardsDbRepository(DbContext dbContext, DbSet<Card> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}