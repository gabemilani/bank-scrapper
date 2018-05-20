using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using BankScrapper.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal sealed class BillsDbRepository : BaseDbRepository<Bill>, IBillsRepository
    {
        private const string AccountIdParameter = "@accountId";
        private const string FromDateParameter = "@fromDate";
        private const string StateParameter = "@state";
        private const string ToDateParameter = "@toDate";

        public BillsDbRepository(DbContext dbContext, DbSet<Bill> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Bill[]> FindAsync(int? accountId = null, BillState? state = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var conditionsBuilder = new StringBuilder();
            var parameters = new List<SqlParameter>();

            if (accountId.HasValue)
            {
                conditionsBuilder.And($"{nameof(Bill.AccountId)} = {AccountIdParameter}");
                parameters.Add(new SqlParameter(AccountIdParameter, accountId.Value));
            }

            if (state.HasValue)
            {
                conditionsBuilder.And($"{nameof(Bill.State)} = {StateParameter}");
                parameters.Add(new SqlParameter(StateParameter, state.Value));
            }

            if (fromDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Bill.OpenDate)} >= {FromDateParameter}");
                parameters.Add(new SqlParameter(FromDateParameter, fromDate.Value));
            }

            if (toDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Bill.CloseDate)} <= {ToDateParameter}");
                parameters.Add(new SqlParameter(ToDateParameter, toDate.Value));
            }

            return FindByQueryAsync(conditionsBuilder, parameters);
        }
    }
}