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
    internal sealed class CustomersDbRepository : BaseDbRepository<Customer>, ICustomersRepository
    {
        private const string CpfParameter = "@cpf";
        private const string FromBirthDateParameter = "@fromBirthDate";
        private const string GenderParameter = "@gender";
        private const string ToBirthDateParameter = "@toBirthDate";

        public CustomersDbRepository(DbContext dbContext, DbSet<Customer> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Customer[]> FindAsync(Gender? gender = null, DateTime? fromBirthDate = null, DateTime? toBirthDate = null)
        {
            var conditionsBuilder = new StringBuilder();
            var parameters = new List<SqlParameter>();

            if (gender.HasValue)
            {
                conditionsBuilder.And($"{nameof(Customer.Gender)} = {GenderParameter}");
                parameters.Add(new SqlParameter(GenderParameter, gender.Value));
            }

            if (fromBirthDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Customer.DateOfBirth)} >= {FromBirthDateParameter}");
                parameters.Add(new SqlParameter(FromBirthDateParameter, fromBirthDate.Value));
            }

            if (toBirthDate.HasValue)
            {
                conditionsBuilder.And($"{nameof(Customer.DateOfBirth)} <= {ToBirthDateParameter}");
                parameters.Add(new SqlParameter(ToBirthDateParameter, toBirthDate.Value));
            }

            return FindByQueryAsync(conditionsBuilder, parameters);
        }

        public Task<Customer> FindByCpfAsync(string cpf)
        {
            return _dbSet
                .SqlQuery(
                    $"SELECT * FROM dbo.{_tableName} WHERE {nameof(Customer.Cpf)} = {CpfParameter}",
                    new SqlParameter(CpfParameter, cpf))
                .FirstOrDefaultAsync();
        }
    }
}