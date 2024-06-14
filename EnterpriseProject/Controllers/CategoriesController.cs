using System.Security.Claims;
using EnterpriseProject.Context;
using EnterpriseProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly WebsiteMarketContext _context;

        public CategoriesController(WebsiteMarketContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var apparel = await _context.Listings
                .Where(l => l.Category == "Apparel")
                .ToListAsync();
            
            var userSavedListingIds = await _context.SavedListings
                .Where(sl => sl.UserId == Convert.ToInt32(userId))
                .Select(sl => sl.ListingId)
                .ToListAsync();
            
            ViewBag.UserSavedListingIds = userSavedListingIds;
            
            return View(apparel);
        }

        [HttpGet]
        public async Task<IActionResult> Electronics()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var electronics = await _context.Listings.
                Where(l => l.Category == "Electronics").
                ToListAsync();
            
            var userSavedListingIds = await _context.SavedListings
                .Where(sl => sl.UserId == Convert.ToInt32(userId))
                .Select(sl => sl.ListingId)
                .ToListAsync();
            
            ViewBag.UserSavedListingIds = userSavedListingIds;
            
            return View(electronics);
        }
        
        public async Task<IActionResult> Entertainment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var entertainment = await _context.Listings
                .Where(l => l.Category == "Entertainment")
                .ToListAsync();
            
            var userSavedListingIds = await _context.SavedListings
                .Where(sl => sl.UserId == Convert.ToInt32(userId))
                .Select(sl => sl.ListingId)
                .ToListAsync();
            
            ViewBag.UserSavedListingIds = userSavedListingIds;
            
            return View(entertainment);
        }
        public async Task<IActionResult> HomeImprovementSupplies()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var homeImprovementSupplies = await _context.Listings
                .Where(l => l.Category == "HomeImprovementSupplies")
                .ToListAsync();
            
            var userSavedListingIds = await _context.SavedListings
                .Where(sl => sl.UserId == Convert.ToInt32(userId))
                .Select(sl => sl.ListingId)
                .ToListAsync();
            
            ViewBag.UserSavedListingIds = userSavedListingIds;
            
            return View(homeImprovementSupplies);
        }
        public async Task<IActionResult> SportingGoods()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var sportingGoods = await _context.Listings
                .Where(l => l.Category == "SportingGoods")
                .ToListAsync();
            
            var userSavedListingIds = await _context.SavedListings
                .Where(sl => sl.UserId == Convert.ToInt32(userId))
                .Select(sl => sl.ListingId)
                .ToListAsync();
            
            ViewBag.UserSavedListingIds = userSavedListingIds;
            
            return View(sportingGoods);
        }
        
        public IActionResult AllListings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveListing(int listingId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "AuthnSecurity");
            }

            int intUserId = Convert.ToInt32(userId);
            var existingListing = await _context.SavedListings
                .AnyAsync(sl => sl.ListingId == listingId && sl.UserId == intUserId);
            
            if (existingListing)
            {
                TempData["Message"] = "You have already saved this listing.";
                return RedirectToAction("ViewSavedListings", "AuthnSecurity");
            }
            
            var savedListing = new SavedListing
            {
                UserId = Convert.ToInt32(userId),
                ListingId = listingId,
                SavedDate = DateTime.UtcNow
            };
            
            _context.SavedListings.Add(savedListing);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("ViewSavedListings", "AuthnSecurity");
        }
    }
}
