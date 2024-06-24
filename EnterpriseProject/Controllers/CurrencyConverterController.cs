using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class CurrencyConverterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
