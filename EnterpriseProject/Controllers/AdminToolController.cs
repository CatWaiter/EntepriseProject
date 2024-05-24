using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class AdminToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
