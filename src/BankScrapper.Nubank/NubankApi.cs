using BankScrapper.Nubank.DTOs;
using BankScrapper.Utils;
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

            UpdateCorrelationId("WEB-APP.BmpW1");
        }

        public async Task<AuthorizationResultDTO> LoginAsync(string cpf, string password)
        {
            //var url = "https://prod-auth.nubank.com.br/api/token";
            var url = "https://prod-s0-webapp-proxy.nubank.com.br/api/proxy/AJxL5LBUC2Tx4PB-W6VD1SEIOd2xp14EDQ.aHR0cHM6Ly9wcm9kLWdsb2JhbC1hdXRoLm51YmFuay5jb20uYnIvYXBpL3Rva2Vu";

            var body = new
            {
                grant_type = "password",
                login = cpf,
                password = password,
                client_id = "other.conta",
                client_secret = "yQPeLzoHuJzlMMSAjC-LgNUJdUecx8XO"
            };

            var result = await PostWithResponseAsync<AuthorizationResultDTO>(url, body);

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            return result;
        }

        private void UpdateCorrelationId(string correlationId)
        {
            DefaultRequestHeaders.Remove(CorrelationIdHeader);
            DefaultRequestHeaders.Add(CorrelationIdHeader, correlationId);
        }
    }
}
