using AutoMapper;
using BankScrapper.BB;
using BankScrapper.Domain.Entities;
using BankScrapper.Domain.Services;
using BankScrapper.Nubank;
using BankScrapper.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankScrapper.Web.AppServices
{
    public sealed class BankScrapperAppService : IAppService
    {
        private readonly AccountsService _accountsService;
        private readonly BillsService _billsService;
        private readonly CardsService _cardsService;
        private readonly CategoriesService _categoriesService;
        private readonly CustomersService _customersService;
        private readonly IMapper _mapper;
        private readonly TransactionsService _transactionsService;

        public BankScrapperAppService(
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
            _cardsService = cardsService ?? throw new ArgumentNullException(nameof(cardsService));
            _categoriesService = categoriesService ?? throw new ArgumentNullException(nameof(categoriesService));
            _customersService = customersService ?? throw new ArgumentNullException(nameof(customersService));
            _transactionsService = transactionsService ?? throw new ArgumentNullException(nameof(transactionsService));
        }

        public async Task ScrapeBankDataAsync(IBankConnectionData connectionData)
        {
            var bankData = await GetBankDataAsync(connectionData);
            if (bankData == null)
                return;

            var account = _mapper.Map<Account>(bankData.Account);
            account.Bank = bankData.Bank;

            if (bankData.Customer != null)
            {
                var customer = _mapper.Map<Customer>(bankData.Customer);
                await _customersService.AddAsync(customer);
                account.Customer = customer;
                account.CustomerId = customer.Id;
            }

            await _accountsService.AddAsync(account);

            await SaveBillsAsync(bankData.Bills, account);
            await SaveCardsAsync(bankData.Cards, account);
            await SaveTransactionsAsync(bankData.Transactions, account);
        }

        private async Task<BankScrapeResult> GetBankDataAsync(IBankConnectionData connectionData)
        {
            BankScrapeResult result;

            using (var provider = GetBankProvider(connectionData))
            {
                result = await provider.GetResultAsync();
            }

            return result;
        }

        private IBankProvider GetBankProvider(IBankConnectionData connectionData)
        {
            if (connectionData == null)
                throw new Exception("É necessário informar os dados de acesso de algum banco");

            if (connectionData.IsValid())
            {
                switch (connectionData.Bank)
                {
                    case Bank.BancoDoBrasil:
                        if (connectionData is BancoDoBrasilConnectionData bbConnectionData)
                            return new BancoDoBrasilProvider(new BancoDoBrasilApiRepository(bbConnectionData));

                        break;

                    case Bank.Nubank:
                        if (connectionData is NubankConnectionData nubankConnectionData)
                            return new NubankProvider(new NubankApiRepository(nubankConnectionData));

                        break;

                    case Bank.Unknown:
                    default:
                        throw new Exception($"Banco não suportado: {connectionData.Bank.GetDescription()}");
                }
            }

            throw new Exception($"Os dados de acesso ao \"{connectionData.Bank.GetDescription()}\" são inválidos");
        }

        private async Task<Category> GetOrCreateCategoryAsync(string name)
        {
            if (name.IsNullOrEmpty())
                return null;

            var category = await _categoriesService.GetByNameAsync(name);
            if (category == null)
            {
                category = new Category { Name = name };
                await _categoriesService.AddAsync(category);
            }

            return category;
        }

        private async Task SaveBillsAsync(BankScrapper.Models.Bill[] bills, Account account)
        {
            if (bills?.Any() != true)
                return;

            foreach (var bill in bills)
            {
                var billEntity = _mapper.Map<Bill>(bill);
                billEntity.Account = account;
                billEntity.AccountId = account.Id;

                await _billsService.AddAsync(billEntity);
            }
        }

        private async Task SaveCardsAsync(BankScrapper.Models.Card[] cards, Account account)
        {
            if (cards?.Any() != true)
                return;

            foreach (var card in cards)
            {
                var cardEntity = _mapper.Map<Card>(card);
                cardEntity.Account = account;
                cardEntity.AccountId = account.Id;

                await _cardsService.AddAsync(cardEntity);
            }
        }

        private async Task SaveTransactionsAsync(BankScrapper.Models.Transaction[] transactions, Account account)
        {
            if (transactions?.Any() != true)
                return;

            foreach (var transaction in transactions)
            {
                var category = await GetOrCreateCategoryAsync(transaction.Category);

                var transactionEntity = _mapper.Map<Transaction>(transaction);
                transactionEntity.Account = account;
                transactionEntity.AccountId = account.Id;
                transactionEntity.Category = category;
                transactionEntity.CategoryId = category?.Id;

                await _transactionsService.AddAsync(transactionEntity);
            }
        }
    }
}