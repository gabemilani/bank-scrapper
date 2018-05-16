using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BankScrapper.Nubank
{
    public sealed class NubankApiRepository : BaseApi, INubankRepository
    {
        private readonly NubankConnectionData _connectionData;
        private AuthorizationResultDTO _authorization;

        public NubankApiRepository(NubankConnectionData connectionData) : base()
        {
            _connectionData = connectionData ?? throw new ArgumentNullException(nameof(connectionData));
            _authorization = null;

            DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            DefaultRequestHeaders.Referrer = new Uri("https://conta.nubank.com.br/");
            DefaultRequestHeaders.Add("Origin", "https://app.nubank.com.br");
        }

        public async Task<AccountDTO> GetAccountAsync()
        {
            await InitializeAsync();

            var result = await GetAsync<AccountResultDTO>(_authorization.Links.Account.Href);
            return result?.Account;
        }

        public async Task<AccountSimpleDTO> GetAccountSimpleAsync()
        {
            await InitializeAsync();

            var result = await GetAsync<AccountSimpleResultDTO>(_authorization.Links.AccountSimple.Href);
            return result?.Account;
        }

        public async Task<BillDTO[]> GetBillsAsync()
        {
            await InitializeAsync();

            var result = await GetAsync<BillsSummaryResultDTO>(_authorization.Links.BillsSummary.Href);
            return result?.Bills;
        }

        public async Task<CustomerDTO> GetCustomerAsync()
        {
            await InitializeAsync();

            var result = await GetAsync<CustomerResultDTO>(_authorization.Links.Customer.Href);
            return result?.Customer;
        }

        public async Task<TransactionDTO[]> GetTransactionsAsync()
        {
            await InitializeAsync();

            var result = await GetAsync<TransactionsResultDTO>(_authorization.Links.Purchases.Href);
            return result?.Transactions;
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

                var result = await GetStringAsync(configUrl);

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

        private async Task InitializeAsync()
        {
            if (_authorization != null)
                return;

            var url = await GetDiscoveryUrl();
            var discoveryResult = await GetAsync<DiscoveryDTO>(url);

            var body = new
            {
                grant_type = "password",
                login = _connectionData.CPF,
                password = _connectionData.Password,
                client_id = "other.conta",
                client_secret = "yQPeLzoHuJzlMMSAjC-LgNUJdUecx8XO"
            };

            _authorization = await PostJsonAsync<AuthorizationResultDTO>(discoveryResult.Login, body);

            if (_authorization == null)
                throw new Exception("Não foi possível estabelecer conexão com o Nubank");

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_authorization.TokenType.Capitalize(), _authorization.AccessToken);
        }
    }
}