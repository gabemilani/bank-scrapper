using BankScrapper.BB.DTOs;
using BankScrapper.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankScrapper.BB
{
    public sealed class BancoDoBrasilApiRepository : BaseApi, IBancoDoBrasilRepository
    {
        private const string BaseApiUrl = "https://mobi.bb.com.br/mov-centralizador/";

        private readonly string _deviceId;
        private readonly string _ida;
        private readonly BancoDoBrasilConnectionData _connectionData;

        private string _idh;
        private string _nickname;

        public BancoDoBrasilApiRepository(BancoDoBrasilConnectionData connectionData) 
            : base(BaseApiUrl)
        {
            _connectionData = connectionData ?? throw new ArgumentNullException(nameof(connectionData));
            _ida = "00000000000000000000000000000000";
            _deviceId = "000000000000000";
            _idh = "";
            _nickname = "BankScrapper." + new Random().Next(1000, 99999);

            DefaultRequestHeaders.UserAgent.ParseAdd(@"Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");
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

            var result = await PostUrlEncodedAsync<BalanceResultDTO>(relativeUrl, values);

            double.TryParse(result?.ServicoSaldo?.Saldo?.Split(' ')[0], out var balance);
            return balance;
        }

        public async Task GetOtherData()
        {
            var relativeUrl = "tela/LimiteCompraSaque/entrada";

            var values = new Dictionary<string, string>
            {
                { "idh", _idh },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            var result = await PostUrlEncodedAsync<JObject>(relativeUrl, values);
            // todo
            //relativeUrl = "tela/Limites/consultarLimites?opcaoConsulta=2"; // pagamento
            //result = await PostWithJsonResponseAsync<JObject>(relativeUrl, values);

            //tela/ExtratoDeContaCorrente/entrada
            //tela/SaldoPoupanca/saldo
            //tela/SaldoConsolidado/entrada
            //tela/ExtratoFatura/entrada
            //tela/LimiteCompraSaque/entrada
            //tela/SaldoDeInvestimentos/entrada
            //tela/ExtratoFundosInvestimento/entrada
            //tela/Limites/consultarLimites ? parametros

            //relativeUrl = "tela/Limites/consultarLimites?opcaoConsulta=2"; // transferencia
            //result = await PostWithJsonResponseAsync<JObject>(relativeUrl, values);
        }

        public async Task<LayoutDTO> GetExtractLayoutAsync()
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

            var result = await PostUrlEncodedAsync<JObject>(relativeUrl, values);

            return result.ToObject<LayoutDTO>();
        }

        public async Task<LoginDTO> GetLoginAsync()
        {
            await InitializeAsync();

            var relativeUrl = "servico/ServicoLogin/login";

            var values = new Dictionary<string, string>
            {
                { "idh", _idh },
                { "senhaConta", _connectionData.ElectronicPassword },
                { "apelido", _nickname },
                { "dependenciaOrigem", _connectionData.Agency },
                { "numeroContratoOrigem",  _connectionData.Account },
                { "idRegistroNotificacao", "" },
                { "idDispositivo", _deviceId },
                { "titularidade", "1" }
            };

            var result = await PostUrlEncodedAsync<LoginResultDTO>(relativeUrl, values);

            await AfterLoginAsync();

            return result?.Login;
        }

        private async Task InitializeAsync()
        {
            if (!_idh.IsNullOrEmpty())
                return;

            var values = new Dictionary<string, string>
            {
                { "hash", "" },
                { "idh", _idh },
                { "id", _ida },
                { "idDispositivo", _deviceId },
                { "apelido", _nickname }
            };

            _idh = await PostUrlEncodedWithStringResponseAsync("hash", values);

            if (_idh.IsNullOrEmpty())
                throw new Exception("Não foi possível estabelecer conexão com o Banco do Brasil");
        }

        private async Task AfterLoginAsync()
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
                await PostUrlEncodedAsync(relativeUrl, values);
            }
        }
    }
}