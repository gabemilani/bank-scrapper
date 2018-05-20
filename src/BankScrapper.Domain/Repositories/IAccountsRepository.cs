using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface IAccountsRepository : IRepository<Account>
    {
        Task<Account[]> FindAsync(Bank? bank = null, AccountType? type = null, int? customerId = null);
    }
}