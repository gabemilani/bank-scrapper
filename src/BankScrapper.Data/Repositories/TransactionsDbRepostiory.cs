using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal sealed class TransactionsDbRepostiory : BaseDbRepository<Transaction>, ITransactionsRepository
    {
        private const string AccountIdParameter = "@accountId";
        private const string CategoryIdParameter = "@categoryId";
        private const string FromDateParameter = "@fromDate";
        private const string ToDateParameter = "@toDate";

        public TransactionsDbRepostiory(DbContext dbContext, DbSet<Transaction> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Transaction[]> FindAsync(int? accountId = null, int? categoryId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var conditionsBuilder = new StringBuilder();
            var parameters = new List<SqlParameter>();

            if (accountId.HasValue)
            {
                conditionsBuilder.And($"{nameof(Transaction.AccountId)} = {AccountIdParameter}");
                parameters.Add(new SqlParameter(AccountIdParameter, accountId.Value));
            }

            if (categoryId.HasValue)
            {
                conditionsBuilder.And($"{nameof(Transaction.CategoryId)} = {CategoryIdParameter}");
                parameters.Add(new SqlParameter(CategoryIdParameter, categoryId.Value));
            }

            if (fromDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Transaction.Date)} >= {FromDateParameter}");
                parameters.Add(new SqlParameter(FromDateParameter, fromDate.Value));
            }

            if (toDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Transaction.Date)} <= {ToDateParameter}");
                parameters.Add(new SqlParameter(ToDateParameter, toDate.Value));
            }

            return FindByQueryAsync(conditionsBuilder, parameters);
        }
    }
}