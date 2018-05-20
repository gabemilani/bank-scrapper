using BankScrapper.BB;
using BankScrapper.Domain.Services;
using BankScrapper.Nubank;
using BankScrapper.Web.Models;
using BankScrapper.Web.Models.Views;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankScrapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankScrapperService _service;

        public HomeController(BankScrapperService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("scrape")]
        public async Task<ActionResult> ScrapeAsync(BankConnectionInputModel inputModel)
        {
            if (inputModel?.ConnectionData == null)
                throw new Exception("Dados de conexão precisam ser informados");

            IBankConnectionData connectionData = null;

            switch (inputModel.Bank)
            {
                case Bank.BancoDoBrasil:
                    connectionData = inputModel.ConnectionData.ToObject<BancoDoBrasilConnectionData>();
                    break;
                case Bank.Nubank:
                    connectionData = inputModel.ConnectionData.ToObject<NubankConnectionData>();
                    break;
                case Bank.Unknown:
                default:
                    throw new Exception("Banco não suportado");
            }

            var result = await _service.GetBankDataAsync(connectionData);

            var viewModel = new BankResultViewModel()
            {
                Result = JsonConvert.SerializeObject(result)
            };

            return View(viewModel);
        }
    }
}