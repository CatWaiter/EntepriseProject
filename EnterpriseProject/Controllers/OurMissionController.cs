using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class OurMissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
