using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Electronics()
        {
            return View();
        }
        public IActionResult Entertainment()
        {
            return View();
        }
        public IActionResult HomeImprovementSupplies()
        {
            return View();
        }
        public IActionResult SportingGoods()
        {
            return View();
        }
    }
}
