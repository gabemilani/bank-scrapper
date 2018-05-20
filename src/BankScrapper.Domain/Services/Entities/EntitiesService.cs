using BankScrapper.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Services.Entities
{
    public abstract class EntitiesService<TEntity> where TEntity : class, IEntity
    {
        protected readonly IContext _context;
        protected readonly IRepository<TEntity> _repository;

        public EntitiesService(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = GetRepository(context) ?? throw new Exception($"Repositório da entidade \"{typeof(TEntity).Name}\" não encontrado");
        }

        public Task<TEntity> GetById(int id) => _repository.FindByIdAsync(id);

        public async Task AddAsync(TEntity entity)
        {
            await ValidateAsync(entity);
            await _repository.AddAsync(entity);
        }

        protected virtual Task ValidateAsync(TEntity entity, bool isNew = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.CompletedTask;
        }

        private static IRepository<TEntity> GetRepository(IContext context)
        {
            var repositoryInterfaceName = typeof(IRepository<>).Name;
            var entityType = typeof(TEntity);

            foreach (var property in context.GetType().GetProperties())
            {
                var repositoryInterface = property.PropertyType.GetInterface(repositoryInterfaceName);
                if (repositoryInterface == null)
                    continue;

                if (repositoryInterface.GetGenericArguments()[0] == entityType)
                    return property.GetValue(context) as IRepository<TEntity>;
            }

            return null;
        }
    }
}