using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankProvider : IBankProvider
    {
        private static IDictionary<string, Gender> _possibleGenders = new Dictionary<string, Gender>(StringComparer.OrdinalIgnoreCase)
        {
            { "male", Gender.Male },
            { "female", Gender.Female }
        };

        private static IDictionary<string, string> _possibleMaritalStatus = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "single", "Solteiro(a)" },
            { "married", "Casado(a)" },
            { "divorced", "Divorciado(a)" },
            { "widowed", "Viúvo(a)" }
        };

        private static IDictionary<string, BillState> _possibleBillStates = new Dictionary<string, BillState>(StringComparer.OrdinalIgnoreCase)
        {
            { "open", BillState.Open },
            { "closed", BillState.Closed },
            { "overdue", BillState.Overdue }
        };

        private readonly INubankRepository _repository;

        public NubankProvider(INubankRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Bank Bank { get; }

        public async Task<BankScrapeResult> GetResultAsync()
        {
            return new BankScrapeResult
            {
                Bank = Bank.Nubank,
                Account = await GetAccountAsync(),
                Customer = await GetCustomerAsync(),
                Cards = await GetCardsAsync(),
                Transactions = await GetTransactionsAsync()
            };
        }

        private async Task<Account> GetAccountAsync()
        {
            var accountDTO = await _repository.GetAccountAsync();
            if (accountDTO == null)
                return null;

            var account = new Account
            {
                Type = AccountType.Card,
                CreationDate = accountDTO.CreatedAt
            };

            account.ExtraInformation["Limite de crédito"] = accountDTO.CreditLimit.ToPreciseValue().ToBrazillianCurrency();
            account.ExtraInformation["Fatura atual"] = accountDTO.CurrentBalance.ToPreciseValue().ToBrazillianCurrency();
            account.ExtraInformation["Crédito disponível"] = accountDTO.Balances.Available.ToPreciseValue().ToBrazillianCurrency();

            return account;
        }

        private async Task<Bill[]> GetBillsAsync()
        {
            var result = new List<Bill>();

            var billsDTO = await _repository.GetBillsAsync();
            if (billsDTO?.Any() == true)
            {
                foreach (var billDTO in billsDTO)
                {
                    var bill = new Bill
                    {
                        CloseDate = billDTO.Summary.CloseDate.ToDateTime("yyyy-MM-dd"),
                        OpenDate = billDTO.Summary.OpenDate.ToDateTime("yyyy-MM-dd"),
                        Total = billDTO.Summary.TotalBalance.ToPreciseValue()
                    };

                    if (billDTO.Summary.Paid == billDTO.Summary.TotalBalance)
                        bill.State = BillState.Paid;
                    else if (_possibleBillStates.TryGetValue(billDTO.State, out var billState))
                        bill.State = billState;

                    bill.ExtraInformation["Data de vencimento"] = billDTO.Summary.DueDate.ToDateTime("yyyy-MM-dd").ToString("dd/MM/yyyy");
                    bill.ExtraInformation["Valor mínimo de pagamento"] = billDTO.Summary.MinimunPayment.ToPreciseValue().ToBrazillianCurrency();

                    result.Add(bill);
                }
            }

            return result.ToArray();
        }

        private async Task<Card[]> GetCardsAsync()
        {
            var result = new List<Card>();

            var accountSimpleDTO = await _repository.GetAccountSimpleAsync();
            if (accountSimpleDTO?.Cards?.Any() == true)
            {
                foreach (var cardDTO in accountSimpleDTO.Cards)
                {
                    var goodThroughSplitted = cardDTO.GoodThrough.Split('-');

                    var card = new Card
                    {
                        PrintedName = cardDTO.PrintedName,
                        Number = cardDTO.CardNumber,
                        ExpiryMonth = goodThroughSplitted[1].ToInt(),
                        ExpiryYear = goodThroughSplitted[0].ToInt(),
                        Type = cardDTO.Type.ContainsIgnoreCase("credit")
                            ? CardType.Credit
                            : cardDTO.Type.ContainsIgnoreCase("debit")
                                ? CardType.Debit
                                : CardType.Unknown
                    };

                    card.ExtraInformation["Cartão físico"] = cardDTO.Type.ContainsIgnoreCase("virtual") ? "Não" : "Sim";

                    result.Add(card);
                }
            }

            return result.ToArray();
        }

        private async Task<Customer> GetCustomerAsync()
        {
            var customerDTO = await _repository.GetCustomerAsync();
            if (customerDTO == null)
                return null;

            var dateOfBirthSplitted = customerDTO.DateOfBirth.Split('-');
            var dateOfBirth = new DateTime(dateOfBirthSplitted[0].ToInt(), dateOfBirthSplitted[1].ToInt(), dateOfBirthSplitted[2].ToInt());

            _possibleGenders.TryGetValue(customerDTO.Gender, out var gender);

            var customer = new Customer
            {
                Address = new Address
                {
                    City = customerDTO.AddressCity,
                    Country = customerDTO.AddressCountry,
                    Neighborhood = customerDTO.AddressLocality,
                    Number = customerDTO.AddressNumber,
                    Postcode = customerDTO.AddressPostcode,
                    State = customerDTO.AddressState,
                    Street = $"{customerDTO.AddressLine1}, {customerDTO.AddressLine2}"
                },
                BillingAddress = new Address
                {
                    City = customerDTO.BillingAddressCity,
                    Country = customerDTO.BillingAddressCountry,
                    Neighborhood = customerDTO.BillingAddressLocality,
                    Number = customerDTO.BillingAddressNumber,
                    Postcode = customerDTO.BillingAddressPostcode,
                    State = customerDTO.BillingAddressState,
                    Street = $"{customerDTO.BillingAddressLine1}, {customerDTO.BillingAddressLine2}"
                },
                CPF = customerDTO.CPF,
                DateOfBirth = dateOfBirth,
                Email = customerDTO.Email,
                Gender = gender,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone
            };

            if (_possibleMaritalStatus.TryGetValue(customerDTO.MaritalStatus, out var maritalStatus))
                customer.ExtraInformation["Estado civil"] = maritalStatus;

            customer.ExtraInformation["Renda mensal"] = customerDTO.ReportedIncome.ToDouble().ToBrazillianCurrency();
            customer.ExtraInformation["Nome da mãe"] = customerDTO.MothersName;
            customer.ExtraInformation["Nacionalidade"] = customerDTO.Nationality;

            return customer;
        }

        private async Task<Transaction[]> GetTransactionsAsync()
        {
            var result = new List<Transaction>();

            var transactionsDTO = await _repository.GetTransactionsAsync();
            if (transactionsDTO?.Any() == true)
            {
                foreach (var transactionDTO in transactionsDTO)
                {
                    var transaction = new Transaction
                    {
                        Category = transactionDTO.Category,
                        Date = transactionDTO.Time,
                        Amount = transactionDTO.Amount.ToPreciseValue()
                    };

                    transaction.ExtraInformation["Nome do comerciante"] = transactionDTO.MerchantName;
                    transaction.ExtraInformation["Código postal do comerciante"] = transactionDTO.Postcode;
                    transaction.ExtraInformation["Modo de captura"] = transactionDTO.CaptureMode.EntryMode;
                    transaction.ExtraInformation["Cartão presente"] = transactionDTO.EventType.ContainsIgnoreCase("card_present") ? "Sim" : "Não";

                    result.Add(transaction);
                }
            }

            return result.ToArray();
        }
    }
}