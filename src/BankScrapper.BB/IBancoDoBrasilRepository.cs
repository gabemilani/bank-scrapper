using BankScrapper.BB.DTOs;
using System;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    public interface IBancoDoBrasilRepository : IDisposable
    {
        Task<double> GetBalanceAsync();

        Task<LayoutDTO> GetExtractLayoutAsync();

        Task<LoginDTO> LoginAsync(string agency, string account, string password);
    }
}