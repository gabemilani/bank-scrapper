using System;

namespace BankScrapper.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);

        TEntity FindById(Guid id);
    }
}