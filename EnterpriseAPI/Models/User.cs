using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnterpriseAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public DateTime AccountCreatedDate { get; set; }
        [JsonIgnore]
        public ICollection<Listing>? Listings { get; set; }
        [JsonIgnore]
        public ICollection<SavedListing>? SavedListings { get; set; }
        public string? Role { get; set; }
    }
}