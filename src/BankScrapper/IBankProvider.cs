using System;
using System.Threading.Tasks;

namespace BankScrapper
{
    public interface IBankProvider : IDisposable
    {
        Task<BankScrapeResult> GetResultAsync();
    }
}