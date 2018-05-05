using BankScrapper.Models;
using System;
using System.Threading.Tasks;

namespace BankScrapper
{
    public interface IBankProvider : IDisposable
    {
        IBankConnectionData ConnectionData { get; }

        Bank Bank { get; }

        Task<Account> GetAccountAsync();

        Task<Transaction[]> GetTransactionsAsync();
    }
}