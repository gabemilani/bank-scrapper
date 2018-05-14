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

        public async Task<AccountDTO> GetAccountAsync(AuthorizationResultDTO auth)
        {
            SetAuthorizationHeader(auth.AccessToken, auth.TokenType);
            var result = await GetAsync<AccountResultDTO>(auth.Links.Account.Href);
            return result?.Account;
        }

        public async Task<BillDTO[]> GetBillsAsync(AuthorizationResultDTO auth)
        {
            SetAuthorizationHeader(auth.AccessToken, auth.TokenType);
            var result = await GetAsync<BillsSummaryResultDTO>(auth.Links.BillsSummary.Href);
            return result?.Bills;
        }

        public async Task<AccountSimpleDTO> GetAccountSimpleAsync(AuthorizationResultDTO auth)
        {
            SetAuthorizationHeader(auth.AccessToken, auth.TokenType);
            var result = await GetAsync<AccountSimpleResultDTO>(auth.Links.AccountSimple.Href);
            return result?.Account;
        }

        public async Task<CustomerDTO> GetCustomerAsync(AuthorizationResultDTO auth)
        {
            SetAuthorizationHeader(auth.AccessToken, auth.TokenType);
            var customerResult = await GetAsync<CustomerResultDTO>(auth.Links.Customer.Href);
            return customerResult?.Customer;
        }

        public async Task Get(AuthorizationResultDTO dto)
        {
            SetAuthorizationHeader(dto.AccessToken, dto.TokenType);

            var url = dto.Links.Events.Href;
            var result = await GetAsync<JObject>(url);

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
                var configUrl = "https://app.nubank.com.br/config/config.js";

                // O que precisa ser feito aqui é receber esse arquivo javacsript de configuração
                // Dentro dele vai ter qual a URL do discovery
                // Não consegui, então deixei o valor fixo por enquanto

                //var result = await GetStringAsync(configUrl);

                //using (var configStream = await GetStreamAsync(configUrl))
                //using (var streamReader = new StreamReader(configStream))
                //{
                //    result = await streamReader.ReadToEndAsync();
                //}
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