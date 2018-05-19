using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class AccountsDbRepository : BaseDbRepository<Account>, IAccountsRepository
    {
        public AccountsDbRepository(DbContext dbContext, DbSet<Account> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}