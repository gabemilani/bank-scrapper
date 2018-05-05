using Newtonsoft.Json;

namespace BankScrapper.BB.DTOs
{
    [JsonObject(IsReference = false)]
    public class ExtractDTO
    {
        [JsonProperty("conteiner")]
        public ContainerDTO Container { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class ContainerDTO
    {
        [JsonProperty("telas")]
        public ScreenDTO[] Telas { get; set; }

        [JsonProperty("leiautes")]
        public object[] Leiautes { get; set; }

        [JsonProperty("deslizante")]
        public bool Deslizante { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class ScreenDTO
    {
        [JsonProperty("idRandomico")]
        public int IdRandomico { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [JsonProperty("orientacoesSuportadas")]
        public string[][] OrientacoesSuportadas { get; set; }

        [JsonProperty("atalho")]
        public bool Atalho { get; set; }

        [JsonProperty("offline")]
        public bool Offline { get; set; }

        [JsonProperty("parametrosDeSessao")]
        public object[] ParametrosDeSessao { get; set; }

        [JsonProperty("testeira")]
        public object[] Testeira { get; set; }

        [JsonProperty("opcoesDeContexto")]
        public object[] OpcoesDeContexto { get; set; }

        [JsonProperty("menuDeRodape")]
        public FooterMenuDTO MenuDeRodape { get; set; }

        [JsonProperty("exibirMenuDeRodape")]
        public string ExibirMenuDeRodape { get; set; }

        [JsonProperty("botaoVoltar")]
        public bool BotaoVoltar { get; set; }

        [JsonProperty("TIPO")]
        public string Tipo { get; set; }

        [JsonProperty("escondeMenu")]
        public bool EscondeMenu { get; set; }

        [JsonProperty("sessoes")]
        public SessionDTO[] Sessoes { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class FooterMenuDTO
    {
        [JsonProperty("visivel")]
        public bool Visivel { get; set; }

        [JsonProperty("TIPO")]
        public string Tipo { get; set; }

        [JsonProperty("itens")]
        public object[] Itens { get; set; }
    }

    public class SessionDTO
    {
        [JsonProperty("TIPO")]
        public string Tipo { get; set; }

        [JsonProperty("celulas")]
        public CellDTO[] Celulas { get; set; }

        [JsonProperty("ordenavel")]
        public bool Ordenavel { get; set; }

        [JsonProperty("segmentado")]
        public bool Segmentado { get; set; }

        [JsonProperty("estilo")]
        public string Estilo { get; set; }

        [JsonProperty("apresentaTesteiraTransacaoEfetivadaMobile30")]
        public bool ApresentaTesteiraTransacaoEfetivadaMobile30 { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class CellDTO
    {
        [JsonProperty("TIPO")]
        public string Tipo { get; set; }

        [JsonProperty("componentes")]
        public ComponentDTO[] Componentes { get; set; }

        [JsonProperty("esconderFundo")]
        public bool EsconderFundo { get; set; }

        [JsonProperty("executaValidacao")]
        public bool ExecutaValidacao { get; set; }

        [JsonProperty("ordenarPor")]
        public bool OrdenarPor { get; set; }

        [JsonProperty("desenhaLinha")]
        public bool DesenhaLinha { get; set; }

        [JsonProperty("visivel")]
        public bool Visivel { get; set; }

        [JsonProperty("colapsado")]
        public bool Colapsado { get; set; }

        [JsonProperty("alturaMinimaDaCelula")]
        public int AlturaMinimaDaCelula { get; set; }
    }

    [JsonObject(IsReference = false)]
    public class ComponentDTO
    {
        [JsonProperty("visivel")]
        public bool Visivel { get; set; }

        [JsonProperty("TIPO")]
        public string Tipo { get; set; }

        [JsonProperty("texto")]
        public string Texto { get; set; }

        [JsonProperty("formato")]
        public string Formato { get; set; }
    }
}