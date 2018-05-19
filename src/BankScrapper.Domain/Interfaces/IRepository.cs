using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task AddAsync(TEntity entity);

        TEntity FindById(int id);
    }
}