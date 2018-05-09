using BankScrapper.Models;
using System;
using System.Threading.Tasks;

namespace BankScrapper
{
    public interface IBankProvider : IDisposable
    {
        Bank Bank { get; }
        IBankConnectionData ConnectionData { get; }

        Task<BankScrapeResult> GetResultAsync();
    }
}