using BankScrapper.BB;
using BankScrapper.Nubank;
using BankScrapper.Utils;
using BankScrapper.Web.AppServices;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankScrapper.Web.Api.Controllers
{
    [RoutePrefix("api/banks")]
    public class BanksController : ApiController
    {
        private readonly BankScrapperAppService _appService;

        public BanksController(BankScrapperAppService appService) 
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [HttpPost, Route("scrape")]
        public async Task<IHttpActionResult> Scrape([FromUri]Bank bank, [FromBody]JObject body)
        {
            if (body == null)
                return BadRequest("Dados de conexão precisam ser informados");

            IBankConnectionData connectionData = null;

            switch (bank)
            {
                case Bank.BancoDoBrasil:
                    connectionData = body.ToObject<BancoDoBrasilConnectionData>();
                    break;
                case Bank.Nubank:
                    connectionData = body.ToObject<NubankConnectionData>();
                    break;
                case Bank.Unknown:
                default:
                    return BadRequest($"Banco não suportado: \"{bank.GetDescription()}\"");
            }

            await _appService.ScrapeBankDataAsync(connectionData);

            return Ok();
        }
    }
}