using System.Web.Mvc;
using ManagePortfolio.TestDataFactory;
using Newtonsoft.Json;

namespace ManagePortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            return View();
        }

        public string GetEquities()
        {
            var equities = PortfolioTestDataFactory.GenerateTestEquities();

            return JsonConvert.SerializeObject(equities);
        }
    }
}