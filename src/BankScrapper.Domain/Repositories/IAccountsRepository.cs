using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;

namespace BankScrapper.Domain.Repositories
{
    public interface IAccountsRepository : IRepository<Account>
    {
    }
}