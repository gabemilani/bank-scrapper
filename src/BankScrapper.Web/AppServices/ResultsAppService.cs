using AutoMapper;
using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Services;
using BankScrapper.Web.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankScrapper.Web.AppServices
{
    public sealed class ResultsAppService : IAppService
    {
        private readonly AccountsService _accountsService;
        private readonly BillsService _billsService;
        private readonly CardsService _cardsService;
        private readonly CategoriesService _categoriesService;
        private readonly CustomersService _customersService;
        private readonly IMapper _mapper;
        private readonly TransactionsService _transactionsService;

        public ResultsAppService(
            IMapper mapper,
            AccountsService accountsService,
            BillsService billsService,
            CardsService cardsService,
            CategoriesService categoriesService,
            CustomersService customersService,
            TransactionsService transactionsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accountsService = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
            _billsService = billsService ?? throw new ArgumentNullException(nameof(billsService));
            _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
            _customersService = customersService ?? throw new ArgumentNullException(nameof(customersService));
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
            _cardsService = cardsService ?? throw new ArgumentNullException(nameof(cardsService));
        }

        public async Task<AccountViewModel[]> GetEverythingAsync()
        {
            var result = new List<AccountViewModel>();

            var accounts = await _accountsService.GetAllAsync();
            if (accounts?.Any() == true)
            {
                foreach (var account in accounts)
                {
                    result.Add(await MapToViewModelAsync(account));
                }
            }

            return result.ToArray();
        }

        private async Task<AccountViewModel> MapToViewModelAsync(Account account)
        {
            var result = _mapper.Map<AccountViewModel>(account);

            var customer = account.Customer;
            if (customer == null && account.CustomerId.HasValue)
                customer = await _customersService.GetByIdAsync(account.CustomerId.Value);

            result.Customer = _mapper.Map<CustomerViewModel>(customer);

            var bills = await _billsService.GetManyAsync(account.Id);
            result.Bills = bills?.Select(_mapper.Map<BillViewModel>).ToArray();

            var cards = await _cardsService.GetManyAsync(account.Id);
            result.Cards = cards?.Select(_mapper.Map<CardViewModel>).ToArray();

            var transactions = await _transactionsService.GetManyAsync(account.Id);
            if (transactions?.Any() == true)
            {
                var resultTransactions = new List<TransactionViewModel>();
                foreach (var transaction in transactions)
                {
                    resultTransactions.Add(await MapToViewModelAsync(transaction));
                }
                result.Transactions = resultTransactions.ToArray();
            }

            return result;
        }

        private async Task<TransactionViewModel> MapToViewModelAsync(Transaction transaction)
        {
            var result = _mapper.Map<TransactionViewModel>(transaction);

            var category = transaction.Category;
            if (category == null && transaction.CategoryId.HasValue)
                category = await _categoriesService.GetByIdAsync(transaction.CategoryId.Value);

            result.CategoryName = category?.Name;

            return result;
        }
    }
}