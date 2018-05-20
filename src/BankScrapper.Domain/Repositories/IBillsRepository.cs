using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface IBillsRepository : IRepository<Bill>
    {
        Task<Bill[]> FindAsync(int? accountId = null, BillState? state = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}