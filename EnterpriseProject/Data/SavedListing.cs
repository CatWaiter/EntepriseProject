using System;
using System.Collections.Generic;

namespace EnterpriseProject.Data;

public partial class SavedListing
{
    public int SavedListingId { get; set; }

    public int UserId { get; set; }

    public int ListingId { get; set; }

    public DateTime SavedDate { get; set; }

    public virtual Listing Listing { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
