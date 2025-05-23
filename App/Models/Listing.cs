﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMarketplace.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string? Title { get; set; }
        public string? PictureUrl { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public DateTime PostedDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public ICollection<SavedListing>? SavedListings { get; set; }
    }
}