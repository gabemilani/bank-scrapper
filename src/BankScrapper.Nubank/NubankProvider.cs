using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankProvider : BankProvider
    {
        private readonly NubankApi _repository;

        public NubankProvider(NubankApi repository, NubankConnectionData connectionData)
            : base(Bank.Nubank, connectionData)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override void Dispose() =>
            _repository.Dispose();

        public override async Task<BankScrapeResult> GetResultAsync()
        {
            var nubankConnectionData = ConnectionData as NubankConnectionData;

            var authResult = await _repository.LoginAsync(nubankConnectionData.CPF, nubankConnectionData.Password);

            var account = await GetAccountAsync(authResult);
            var customer = await GetCustomerAsync(authResult);
            var cards = await GetCardsAsync(authResult);

            return new BankScrapeResult
            {
                Account = account,
                Customer = customer,
                Cards = cards
            };
        }

        private async Task<Bill[]> GetBillsAsync(AuthorizationResultDTO authResult)
        {
            throw new NotImplementedException();
        }

        private async Task<Account> GetAccountAsync(AuthorizationResultDTO authResult)
        {
            var accountDTO = await _repository.GetAccountAsync(authResult);
            var account = new Account
            {
                Type = AccountType.Card,
                CreationDate = accountDTO.CreatedAt
            };

            account.ExtraInformation["Limite de crédito"] = accountDTO.CreditLimit.GetMonetaryValue().ToBrazillianCurrency();
            account.ExtraInformation["Fatura atual"] = accountDTO.CurrentBalance.GetMonetaryValue().ToBrazillianCurrency();
            account.ExtraInformation["Crédito disponível"] = accountDTO.Balances.Available.GetMonetaryValue().ToBrazillianCurrency();
            return account;
        }

        private async Task<Card[]> GetCardsAsync(AuthorizationResultDTO authResult)
        {
            var accountSimpleDTO = await _repository.GetAccountSimpleAsync(authResult);
            var result = new List<Card>();

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

            return result.ToArray();
        }

        private async Task<Customer> GetCustomerAsync(AuthorizationResultDTO authResult)
        {
            var customerDTO = await _repository.GetCustomerAsync(authResult);

            var dateOfBirthSplitted = customerDTO.DateOfBirth.Split('-');
            var dateOfBirth = new DateTime(dateOfBirthSplitted[0].ToInt(), dateOfBirthSplitted[1].ToInt(), dateOfBirthSplitted[2].ToInt());

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
                Gender = customerDTO.Gender.EqualsIgnoreCase("male") ? Gender.Male : customerDTO.Gender.EqualsIgnoreCase("female") ? Gender.Female : Gender.Unknown,
                Name = customerDTO.Name,
                Phone = customerDTO.Phone
            };

            customer.ExtraInformation["Estado civil"] = customerDTO.MaritalStatus;
            customer.ExtraInformation["Renda mensal"] = customerDTO.ReportedIncome.ToDouble().ToBrazillianCurrency();
            customer.ExtraInformation["Nome da mãe"] = customerDTO.MothersName;
            customer.ExtraInformation["Nacionalidade"] = customerDTO.Nationality;

            return customer;
        }
    }
}