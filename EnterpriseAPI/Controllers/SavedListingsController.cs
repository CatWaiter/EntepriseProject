using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnterpriseAPI.Data;
using EnterpriseAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedListingsController : ControllerBase
    {
        private readonly EnterpriseContext _context;

        public SavedListingsController(EnterpriseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedListing>>> GetSavedListings()
        {
            return await _context.SavedListings.Include(sl => sl.User).Include(sl => sl.Listing).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SavedListing>> GetSavedListing(int id)
        {
            var savedListing = await _context.SavedListings.Include(sl => sl.User)
                                                           .Include(sl => sl.Listing)
                                                           .FirstOrDefaultAsync(sl => sl.SavedListingId == id);

            if (savedListing == null)
            {
                return NotFound();
            }

            return savedListing;
        }

        [HttpPost]
        public async Task<ActionResult<SavedListing>> PostSavedListing(SavedListing savedListing)
        {
            _context.SavedListings.Add(savedListing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSavedListing), new { id = savedListing.SavedListingId }, savedListing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSavedListing(int id, SavedListing savedListing)
        {
            if (id != savedListing.SavedListingId)
            {
                return BadRequest();
            }

            _context.Entry(savedListing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavedListingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavedListing(int id)
        {
            var savedListing = await _context.SavedListings.FindAsync(id);
            if (savedListing == null)
            {
                return NotFound();
            }

            _context.SavedListings.Remove(savedListing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SavedListingExists(int id)
        {
            return _context.SavedListings.Any(e => e.SavedListingId == id);
        }
    }
}