using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name);
    }
}