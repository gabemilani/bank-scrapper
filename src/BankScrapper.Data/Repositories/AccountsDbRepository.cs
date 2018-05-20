using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using BankScrapper.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal sealed class AccountsDbRepository : BaseDbRepository<Account>, IAccountsRepository
    {
        private const string BankParameter = "@bank";
        private const string CustomerIdParameter = "@customerId";
        private const string TypeParameter = "@type";

        public AccountsDbRepository(DbContext dbContext, DbSet<Account> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Account[]> FindAsync(Bank? bank = null, AccountType? type = null, int? customerId = null)
        {
            var conditionsBuilder = new StringBuilder();
            var parameters = new List<SqlParameter>();

            if (bank.HasValue)
            {
                conditionsBuilder.And($"{nameof(Account.Bank)} = {BankParameter}");
                parameters.Add(new SqlParameter(BankParameter, bank.Value));
            }

            if (type.HasValue)
            {
                conditionsBuilder.And($"{nameof(Account.Type)} = {TypeParameter}");
                parameters.Add(new SqlParameter(TypeParameter, type.Value));
            }

            if (customerId.HasValue)
            {
                conditionsBuilder.And($"{nameof(Account.CustomerId)} = {CustomerIdParameter}");
                parameters.Add(new SqlParameter(CustomerIdParameter, customerId.Value));
            }

            return FindByQueryAsync(conditionsBuilder, parameters);
        }
    }
}