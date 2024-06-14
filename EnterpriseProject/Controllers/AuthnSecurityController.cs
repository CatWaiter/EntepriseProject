using System.Security.Claims;
using EnterpriseProject.Context;
using EnterpriseProject.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseProject.Controllers
{
    public class AuthnSecurityController : Controller
    {
        private readonly ManualSecurityContext _context;
        private readonly ILogger<ManualSecurityContext> _logger;

        public AuthnSecurityController(ManualSecurityContext context, ILogger<ManualSecurityContext> logger)
        {
            _context = context;
            _logger = logger;
        }
    
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null)
            {
                return RedirectToAction("Login", "AuthnSecurity");
            }

            var user = await _context.Users.FindAsync(Convert.ToInt32(userId));
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> ViewSavedListings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var listings = await _context.SavedListings
                .Include(s => s.Listing)
                .Include(s => s.User)
                .Where(s => s.UserId == Convert.ToInt32(userId))
                .ToListAsync();
            
            return View(listings);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var savedListing = await _context.SavedListings.FindAsync(id);
            if (savedListing == null)
            {
                return NotFound();
            }
            return View(savedListing);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedListing = await _context.SavedListings.FindAsync(id);
            _context.SavedListings.Remove(savedListing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewSavedListings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required");
                return View();
            }

            if (_context.Users == null || _context.Users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("", "Username already exists");
                return View();
            }

            var user = new User
            {

                Username = username,
                Password = password,
                AccountCreatedDate = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users?.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role ?? "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "User not found");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}