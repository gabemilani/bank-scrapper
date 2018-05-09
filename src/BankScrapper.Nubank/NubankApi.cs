using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankApi : BaseApi
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";

        public NubankApi() : base()
        {            
            DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            DefaultRequestHeaders.Referrer = new Uri("https://conta.nubank.com.br/");
            DefaultRequestHeaders.Add("Origin", "https://app.nubank.com.br");

            UpdateCorrelationId("WEB-APP.uak6q");
        }

        public async Task<CustomerDTO> GetCustomerAsync(AuthorizationResultDTO result)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            var url = result.Links.Customer.Href;
            var customerResult = await GetAsync<CustomerResultDTO>(url);
            return customerResult.Customer;
        }

        public async Task<AccountDTO> GetAccountAsync(AuthorizationResultDTO result)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            var url = result.Links.Account.Href;
            var accountResult = await GetAsync<AccountResultDTO>(url);
            return accountResult.Account;
        }

        public async Task<JToken> GetToken(string url, string accessToken)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await GetAsync<JToken>(url);
        }

        public async Task<AuthorizationResultDTO> LoginAsync(string cpf, string password)
        {
            var url = "https://prod-s0-webapp-proxy.nubank.com.br/api/discovery";
            var discoveryResult = await GetAsync<DiscoveryDTO>(url);

            var body = new
            {
                grant_type = "password",
                login = cpf,
                password,
                client_id = "other.conta",
                client_secret = "yQPeLzoHuJzlMMSAjC-LgNUJdUecx8XO"
            };

            return await PostWithResponseAsync<AuthorizationResultDTO>(discoveryResult.Login, body);
        }

        private void UpdateCorrelationId(string correlationId)
        {
            DefaultRequestHeaders.Remove(CorrelationIdHeader);
            DefaultRequestHeaders.Add(CorrelationIdHeader, correlationId);
        }
    }
}
