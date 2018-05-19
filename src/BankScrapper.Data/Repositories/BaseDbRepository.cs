using BankScrapper.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal abstract class BaseDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseDbRepository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public TEntity FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}