using System.Security.Claims;
using EnterpriseProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class AdminToolController : Controller
    {
        private readonly ManualSecurityContext _context;
        
        public AdminToolController(ManualSecurityContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var isAdmin = UserIsAdmin();
            if (!isAdmin)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private bool UserIsAdmin()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users?.Find(Convert.ToInt32(userId));
            return user?.Role == "Admin";
        }
    }
}
