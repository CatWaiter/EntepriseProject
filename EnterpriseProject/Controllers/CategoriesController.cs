using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
