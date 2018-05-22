using System.Web.Mvc;

namespace BankScrapper.Web.Controllers
{
    [RoutePrefix("resultados")]
    public class ResultsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}