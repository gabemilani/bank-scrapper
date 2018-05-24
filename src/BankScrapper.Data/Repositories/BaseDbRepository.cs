using BankScrapper.Domain.Attributes;
using BankScrapper.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal abstract class BaseDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly string _tableName;

        public BaseDbRepository(DbContext dbContext, DbSet<TEntity> dbSet)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));

            var collectionAttribute = typeof(TEntity).GetCustomAttribute<CollectionAttribute>();
            if (collectionAttribute == null)
                throw new Exception($"A entidade \"{typeof(TEntity).Name}\" não implementa o atributo Collection");

            _tableName = collectionAttribute.Name;
        }

        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<TEntity[]> FindAllAsync() => _dbSet.ToArrayAsync();

        public Task<TEntity> FindByIdAsync(int id) => _dbSet.FindAsync(id);

        protected Task<TEntity[]> FindByQueryAsync(StringBuilder conditionsBuilder, IEnumerable<SqlParameter> parameters)
        {
            var query = $"SELECT * FROM dbo.{_tableName}";
            if (conditionsBuilder.Length > 0)
                query += $" WHERE {conditionsBuilder.ToString()}";

            return _dbSet.SqlQuery(query, parameters.ToArray()).ToArrayAsync();
        }
    }
}