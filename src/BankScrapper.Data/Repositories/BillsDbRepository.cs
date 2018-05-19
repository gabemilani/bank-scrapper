using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class BillsDbRepository : BaseDbRepository<Bill>, IBillsRepository
    {
        public BillsDbRepository(DbContext dbContext, DbSet<Bill> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}