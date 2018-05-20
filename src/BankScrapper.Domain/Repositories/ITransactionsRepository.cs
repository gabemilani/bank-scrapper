using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface ITransactionsRepository : IRepository<Transaction>
    {
        Task<Transaction[]> FindAsync(int? accountId = null, int? categoryId = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}