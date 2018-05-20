using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Interfaces;
using BankScrapper.Enums;
using System;
using System.Threading.Tasks;

namespace BankScrapper.Domain.Repositories
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Task<Customer> FindByCpfAsync(string cpf);

        Task<Customer[]> FindAsync(Gender? gender = null, DateTime? fromBirthDate = null, DateTime? toBirthDate = null);
    }
}