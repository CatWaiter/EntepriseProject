using System;
using System.Collections.Generic;

namespace EnterpriseProject.Data;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public int Age { get; set; }

    public string? Location { get; set; }

    public string? Phone { get; set; }

    public DateTime AccountCreatedDate { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<SavedListing> SavedListings { get; set; } = new List<SavedListing>();
}
