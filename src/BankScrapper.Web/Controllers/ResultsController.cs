using BankScrapper.Web.AppServices;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankScrapper.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ResultsAppService _appService;

        public ResultsController(ResultsAppService appService)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        public async Task<ActionResult> Index()
        {
            var accounts = await _appService.GetEverythingAsync();

            return View(accounts);
        }
    }
}