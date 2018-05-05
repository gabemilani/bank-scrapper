using Newtonsoft.Json;

namespace BankScrapper.BB.DTOs
{
    [JsonObject(IsReference = false)]
    public sealed class LoginResultDTO
    {
        [JsonProperty("login")]
        public LoginDTO Login { get; set; }
    }

    [JsonObject(IsReference = false)]
    public sealed class LoginDTO
    {
        [JsonProperty("mci")]
        public string MCI { get; set; }

        [JsonProperty("nomeCliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("titularidade")]
        public int Titularidade { get; set; }

        [JsonProperty("dependenciaOrigem")]
        public string DependenciaOrigem { get; set; }

        [JsonProperty("numeroContratoOrigem")]
        public string NumeroContratoOrigem { get; set; }

        [JsonProperty("segmento")]
        public string Segmento { get; set; }

        [JsonProperty("imagemCliente")]
        public string ImagemCliente { get; set; }

        [JsonProperty("habilitadoParaAtendimentoRemoto")]
        public bool HabilitadoParaAtendimentoRemoto { get; set; }

        [JsonProperty("statusAutorizacaoTransacoesFinanceiras")]
        public string StatusAutorizacaoTransacoesFinanceiras { get; set; }
    }
}