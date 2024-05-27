using System;

namespace EnterpriseAPI.Models
{
    public class SavedListing
    {
        public int SavedListingId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ListingId { get; set; }
        public Listing? Listing { get; set; }
        public DateTime SavedDate { get; set; }
    }
}