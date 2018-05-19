using BankScrapper.Data.Repositories;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using System;
using System.Data.Entity;
using System.Linq;

namespace BankScrapper.Data
{
    public sealed class BankScrapperContext : IContext
    {
        public BankScrapperContext(BankScrapperDbContext dbContext)
        {
            CreateRepositories(dbContext);
        }

        public IAccountsRepository Accounts { get; private set; }

        public IBillsRepository Bills { get; private set; }

        public ICardsRepository Cards { get; private set; }

        public ICategoriesRepository Categories { get; private set; }

        public ICustomersRepository Customers { get; private set; }

        public ITransactionsRepository Transactions { get; private set; }

        private void CreateRepositories(BankScrapperDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            var baseRepositoryType = typeof(BaseDbRepository<>);

            var repositoryTypes = baseRepositoryType.Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType.Name == baseRepositoryType.Name)
                .ToArray();

            var dbContextProperties = dbContext.GetType().GetProperties();
            var baseInterfaceName = typeof(IRepository<>).Name;

            foreach (var property in GetType().GetProperties())
            {
                var baseInterface = property.PropertyType.GetInterface(baseInterfaceName);
                if (baseInterface == null)
                    throw new NotImplementedException($"Repositório \"{property.PropertyType.Name}\" não implementa a interface IRepository");

                var entityType = baseInterface.GetGenericArguments().FirstOrDefault();

                var dbSetType = typeof(DbSet<>).MakeGenericType(entityType);
                var dbSetProperty = dbContextProperties.FirstOrDefault(p => p.PropertyType == dbSetType);
                if (dbSetProperty == null)
                    throw new NotImplementedException($"Coleção da entidade \"{entityType.Name}\" não foi implementado");

                var genericRepositoryType = baseRepositoryType.MakeGenericType(entityType);
                var repositoryType = repositoryTypes.FirstOrDefault(t => genericRepositoryType.IsAssignableFrom(t));
                if (repositoryType == null)
                    throw new NotImplementedException($"Repositório da entidade \"{entityType.Name}\" não foi implementado");

                var dbSet = dbSetProperty.GetValue(dbContext);
                var repository = Activator.CreateInstance(repositoryType, new object[] { dbContext, dbSet });
                property.SetValue(this, repository);
            }
        }
    }
}