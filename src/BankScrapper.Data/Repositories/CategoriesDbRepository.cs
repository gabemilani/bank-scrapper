using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;

namespace BankScrapper.Data.Repositories
{
    internal sealed class CategoriesDbRepository : BaseDbRepository<Category>, ICategoriesRepository
    {
        public CategoriesDbRepository(DbContext dbContext, DbSet<Category> dbSet) : base(dbContext, dbSet)
        {
        }
    }
}