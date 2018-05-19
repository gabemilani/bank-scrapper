using BankScrapper.Nubank.DTOs;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public interface INubankRepository : IDisposable
    {
        Task<AccountDTO> GetAccountAsync();

        Task<AccountSimpleDTO> GetAccountSimpleAsync();

        Task<BillDTO[]> GetBillsAsync();

        Task<CustomerDTO> GetCustomerAsync();

        Task<TransactionDTO[]> GetTransactionsAsync();
    }
}