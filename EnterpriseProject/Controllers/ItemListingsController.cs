using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnterpriseProject.Context;
using EnterpriseProject.Data;

namespace EnterpriseProject.Controllers
{
    public class ItemListingsController : Controller
    {
        private readonly WebsiteMarketContext _context;

        public ItemListingsController(WebsiteMarketContext context)
        {
            _context = context;
        }

        // GET: ItemListings
        public async Task<IActionResult> Index()
        {
            var websiteMarketContext = _context.Listings.Include(l => l.User);
            return View(await websiteMarketContext.ToListAsync());
        }

        // GET: ItemListings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: ItemListings/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: ItemListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,Title,PictureUrl,Description,Location,Price,Category,PostedDate,UserId")] Listing listing)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("ModelState is invalid");
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    Console.WriteLine($"Key: {entry.Key}, Errors: {string.Join(",", entry.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", listing.UserId);
            return View(listing);
        }

        // GET: ItemListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", listing.UserId);
            return View(listing);
        }

        // POST: ItemListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingId,Title,PictureUrl,Description,Location,Price,Category,PostedDate,UserId")] Listing listing)
        {
            if (id != listing.ListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listing.ListingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", listing.UserId);
            return View(listing);
        }

        // GET: ItemListings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Listings == null)
            {
                return NotFound();
            }

            var listing = await _context.Listings
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // POST: ItemListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Listings == null)
            {
                return Problem("Entity set 'WebsiteMarketContext.Listings'  is null.");
            }
            var listing = await _context.Listings.FindAsync(id);
            if (listing != null)
            {
                _context.Listings.Remove(listing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(int id)
        {
          return (_context.Listings?.Any(e => e.ListingId == id)).GetValueOrDefault();
        }
    }
}
