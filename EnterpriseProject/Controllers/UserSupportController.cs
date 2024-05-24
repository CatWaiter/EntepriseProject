using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class UserSupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
