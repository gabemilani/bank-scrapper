using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface ICardsRepository : IRepository<Card>
    {
        Task<Card[]> FindAsync(int? accountId = null, CardType? type = null, string printedName = null);
    }
}