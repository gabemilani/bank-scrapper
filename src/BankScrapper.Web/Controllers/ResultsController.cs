using BankScrapper.Web.AppServices;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankScrapper.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ResultsAppService _service;

        public ResultsController(ResultsAppService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ActionResult> Index()
        {
            var accounts = await _service.GetEverythingAsync();

            return View(accounts);
        }
    }
}