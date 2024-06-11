using EnterpriseProject.Context;
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
            var apparel = await _context.Listings
                .Where(l => l.Category == "Apparel")
                .ToListAsync();
            return View(apparel);
        }

        [HttpGet]
        public async Task<IActionResult> Electronics()
        {
            var electronics = await _context.Listings.
                Where(l => l.Category == "Electronics").
                ToListAsync();
            return View(electronics);
        }
        
        public async Task<IActionResult> Entertainment()
        {
            var entertainment = await _context.Listings
                .Where(l => l.Category == "Entertainment")
                .ToListAsync();
            return View(entertainment);
        }
        public async Task<IActionResult> HomeImprovementSupplies()
        {
            var homeImprovementSupplies = await _context.Listings
                .Where(l => l.Category == "HomeImprovementSupplies")
                .ToListAsync();
            return View(homeImprovementSupplies);
        }
        public async Task<IActionResult> SportingGoods()
        {
            var sportingGoods = await _context.Listings
                .Where(l => l.Category == "SportingGoods")
                .ToListAsync();
            return View(sportingGoods);
        }
        public IActionResult AllListings()
        {
            return View();
        }
    }
}
