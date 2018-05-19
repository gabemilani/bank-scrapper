using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class CustomersDbRepository : BaseDbRepository<Customer>, ICustomersRepository
    {
        public CustomersDbRepository(DbContext dbContext, DbSet<Customer> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}