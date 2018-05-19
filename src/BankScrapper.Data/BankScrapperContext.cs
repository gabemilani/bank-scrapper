using BankScrapper.Domain.Interfaces;
using BankScrapper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankScrapper.Data
{
    public sealed class BankScrapperContext : IContext
    {
        private readonly BankScrapperDbContext _dbContext;

        public BankScrapperContext(BankScrapperDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IAccountsRepository Accounts => throw new NotImplementedException();

        public IBillsRepository Bills => throw new NotImplementedException();

        public ICardsRepository Cards => throw new NotImplementedException();

        public ICategoriesRepository Categories => throw new NotImplementedException();

        public ICustomersRepository Customers => throw new NotImplementedException();

        public ITransactionsRepository Transactions => throw new NotImplementedException();
    }
}
