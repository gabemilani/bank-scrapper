using BankScrapper.Enums;
using BankScrapper.Models;
using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
using System;
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

        private async Task<Customer> GetCustomerAsync(AuthorizationResultDTO authResult)
        {
            var customerDTO = await _repository.GetCustomerAsync(authResult.Links.Customer.Href, authResult.AccessToken, authResult.TokenType);

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

        public override async Task<BankScrapeResult> GetResultAsync()
        {
            var nubankConnectionData = ConnectionData as NubankConnectionData;

            var authResult = await _repository.LoginAsync(nubankConnectionData.CPF,  nubankConnectionData.Password);



            var accountDTO = await _repository.GetAccountAsync(authResult.Links.Account.Href, authResult.AccessToken, authResult.TokenType);

            var account = new Account
            {
                Type = AccountType.Card,
                CreationDate = accountDTO.CreatedAt
            };

            account.ExtraInformation["Limite de crédito"] = ((double)accountDTO.CreditLimit / 100).ToBrazillianCurrency();
            account.ExtraInformation["Fatura atual"] = ((double)accountDTO.CurrentBalance / 100).ToBrazillianCurrency();
            account.ExtraInformation["Crédito disponível"] = ((double)accountDTO.Balances.Available / 100).ToBrazillianCurrency();

            var customer = await GetCustomerAsync(authResult);
            
            return new BankScrapeResult
            {
                Account = account,
                Customer = customer,
                
            };
        }
    }
}