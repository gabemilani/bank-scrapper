using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Repositories;
using BankScrapper.Enums;
using BankScrapper.Utils;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data.Repositories
{
    internal sealed class CardsDbRepository : BaseDbRepository<Card>, ICardsRepository
    {
        private const string AccountIdParameter = "@accountId";
        private const string PrintedNameParameter = "@printedName";
        private const string TypeParameter = "@type";

        public CardsDbRepository(DbContext dbContext, DbSet<Card> dbSet) : base(dbContext, dbSet)
        {
        }

        public Task<Card[]> FindAsync(int? accountId = null, CardType? type = null, string printedName = null)
        {
            var conditionsBuilder = new StringBuilder();
            var parameters = new List<SqlParameter>();

            if (accountId.HasValue)
            {
                conditionsBuilder.And($"{nameof(Card.AccountId)} = {AccountIdParameter}");
                parameters.Add(new SqlParameter(AccountIdParameter, accountId.Value));
            }

            if (type.HasValue)
            {
                conditionsBuilder.And($"{nameof(Card.Type)} = {TypeParameter}");
                parameters.Add(new SqlParameter(TypeParameter, type.Value));
            }

            if (!printedName.IsNullOrEmpty())
            {
                conditionsBuilder.And($"{nameof(Card.PrintedName)} = {PrintedNameParameter}");
                parameters.Add(new SqlParameter(PrintedNameParameter, printedName));
            }

            return FindByQueryAsync(conditionsBuilder, parameters);
        }
    }
}