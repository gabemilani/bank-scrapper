using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class TransactionsDbRepostiory : BaseDbRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsDbRepostiory(DbContext dbContext, DbSet<Transaction> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}