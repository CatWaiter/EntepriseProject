using EnterpriseProject.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminToolController : Controller
    {
        private readonly ManualSecurityContext _context;
        
        public AdminToolController(ManualSecurityContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users?.ToList();
            var listingCount = _context.Listings.Count();
                
            ViewData["UserCount"] = users.Count;
            ViewData["ListingCount"] = listingCount;
            
            return View(users );
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _context.Users?.Find(id);
            
            if (user == null)
            {
                return NotFound();
            }
            
            return View(user);
        }
        
       [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        [HttpPost, ActionName("DeleteUser")]
        public IActionResult DeleteUserConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
