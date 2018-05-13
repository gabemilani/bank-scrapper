using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankApi : BaseApi
    {
        public NubankApi() : base()
        {
            DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            DefaultRequestHeaders.Referrer = new Uri("https://conta.nubank.com.br/");
            DefaultRequestHeaders.Add("Origin", "https://app.nubank.com.br");
        }

        public async Task<AccountDTO> GetAccountAsync(string accountUrl, string accessToken, string tokenType)
        {
            SetAuthorizationHeader(accessToken, tokenType);
            var accountResult = await GetAsync<AccountResultDTO>(accountUrl);
            return accountResult?.Account;
        }

        public async Task<CustomerDTO> GetCustomerAsync(string customerUrl, string accessToken, string tokenType)
        {
            SetAuthorizationHeader(accessToken, tokenType);
            var customerResult = await GetAsync<CustomerResultDTO>(customerUrl);
            return customerResult?.Customer;
        }

        public async Task Get(AuthorizationResultDTO dto)
        {
            SetAuthorizationHeader(dto.AccessToken, dto.TokenType);
            var url = dto.Links.BillsSummary.Href;
            var result = await GetAsync<JObject>(url);

            url = dto.Links.AccountSimple.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.Events.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.EventsPage.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.HealthCheck.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.Postcode.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.Purchases.Href;
            result = await GetAsync<JObject>(url);

            url = dto.Links.UserInfo.Href;
            result = await GetAsync<JObject>(url);
        }

        private async Task<string> GetDiscoveryUrl()
        {
            var discoveryUrl = string.Empty;

            try
            {
                var url = "https://app.nubank.com.br/config/config.js";

                var teste = await GetStringAsync(url);

                using (var configStream = await GetStreamAsync(url))
                using (var streamReader = new StreamReader(configStream))
                {
                    var result = await streamReader.ReadToEndAsync();
                }
            }
            catch
            {
                discoveryUrl = null;
            }

            return discoveryUrl.IsNullOrEmpty()
                ? "https://prod-s0-webapp-proxy.nubank.com.br/api/discovery"
                : discoveryUrl;
        }

        public async Task<AuthorizationResultDTO> LoginAsync(string cpf, string password)
        {
            var url = await GetDiscoveryUrl();
            var discoveryResult = await GetAsync<DiscoveryDTO>(url);

            var body = new
            {
                grant_type = "password",
                login = cpf,
                password,
                client_id = "other.conta",
                client_secret = "yQPeLzoHuJzlMMSAjC-LgNUJdUecx8XO"
            };

            return await PostJsonAsync<AuthorizationResultDTO>(discoveryResult.Login, body);
        }

        private void SetAuthorizationHeader(string accessToken, string tokenType)
        {
            tokenType = tokenType?.Capitalize();

            if (!string.Equals(DefaultRequestHeaders.Authorization?.Scheme, tokenType) ||
                !string.Equals(DefaultRequestHeaders.Authorization?.Parameter, accessToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
            }
        }
    }
}