using BankScrapper.Domain.Repositories;

namespace BankScrapper.Domain.Interfaces
{
    public interface IContext
    {
        IAccountsRepository Accounts { get; }

        IBillsRepository Bills { get; }

        ICardsRepository Cards { get; }

        ICategoriesRepository Categories { get; }

        ICustomersRepository Customers { get; }

        ITransactionsRepository Transactions { get; }
    }
}