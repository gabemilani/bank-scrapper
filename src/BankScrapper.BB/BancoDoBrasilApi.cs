using BankScrapper.BB.DTOs;
using BankScrapper.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    internal sealed class BancoDoBrasilApi : BaseApi
    {
        private const string BaseApiUrl = "https://mobi.bb.com.br/mov-centralizador/";

        private readonly string _deviceId;
        private readonly string _ida;

        private string _idh;
        private string _nickname;

        public BancoDoBrasilApi() 
            : base(BaseApiUrl)
        {
            _ida = "00000000000000000000000000000000";
            _deviceId = "000000000000000";
            _idh = "";
            _nickname = "BankScrapper." + new Random().Next(1000, 99999);
        }

        public async Task<double> GetBalanceAsync()
        {
            await InitializeAsync();

            var relativeUrl = "servico/ServicoSaldo/saldo";

            var values = new Dictionary<string, string>
            {
                { "servico/ServicoSaldo/saldo", "" },
                { "idh", _idh },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            var result = await PostWithJsonResponseAsync<BalanceResultDTO>(relativeUrl, values);

            double.TryParse(result?.ServicoSaldo?.Saldo?.Split(' ')[0], out var balance);
            return balance;
        }

        public async Task<ExtractDTO> GetExtractAsync()
        {
            await InitializeAsync();

            var relativeUrl = "tela/ExtratoDeContaCorrente/extrato";

            var values = new Dictionary<string, string>
            {
                { "abrangencia", "8" },
                { "idh", _idh },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            return await PostWithJsonResponseAsync<ExtractDTO>(relativeUrl, values);
        }

        public async Task<LoginDTO> LoginAsync(string agency, string account, string password, int holderLevel)
        {
            await InitializeAsync();

            var relativeUrl = "servico/ServicoLogin/login";

            var values = new Dictionary<string, string>
            {
                { "idh", _idh },
                { "senhaConta", password },
                { "apelido", _nickname },
                { "dependenciaOrigem", agency },
                { "numeroContratoOrigem",  account },
                { "idRegistroNotificacao", "" },
                { "idDispositivo", _deviceId },
                { "titularidade", holderLevel.ToString() }
            };

            var result = await PostWithJsonResponseAsync<LoginResultDTO>(relativeUrl, values);

            await PostLoginAsync();

            return result?.Login;
        }

        private async Task InitializeAsync()
        {
            if (!_idh.IsNullOrEmpty())
                return;

            var relativeUrl = "hash";

            var values = new Dictionary<string, string>
            {
                { "hash", "" },
                { "idh", _idh },
                { "id", _ida },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            _idh = await PostWithStringResponseAsync(relativeUrl, values);
        }

        private async Task PostLoginAsync()
        {
            var values = new Dictionary<string, string>
            {
                { "servico/ServicoVersionamento/servicosVersionados:", "" },
                { "idh", _idh },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            var relativeUrls = new[]
            {
                "servico/ServicoVersionamento/servicosVersionados",
                "servico/ServicoVersaoCentralizador/versaoDaAplicacaoWeb",
                "servico/ServicoMenuPersonalizado/menuPersonalizado",
                "servico/ServicoMenuTransacoesFavoritas/menuTransacoesFavoritas"
            };

            foreach (var relativeUrl in relativeUrls)
            {
                await PostAsync(relativeUrl, values);
            }
        }
    }
}