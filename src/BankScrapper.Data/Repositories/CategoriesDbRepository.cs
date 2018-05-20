using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal sealed class CategoriesDbRepository : BaseDbRepository<Category>, ICategoriesRepository
    {
        private const string NameParameter = "@name";

        public CategoriesDbRepository(DbContext dbContext, DbSet<Category> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Category> FindByNameAsync(string name)
        {
            return _dbSet
                .SqlQuery(
                    $"SELECT * FROM {_tableName} WHERE {nameof(Category.Name)} = {NameParameter}",
                    new SqlParameter(NameParameter, name))
                .FirstOrDefaultAsync();
        }
    }
}