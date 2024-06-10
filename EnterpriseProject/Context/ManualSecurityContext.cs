using EnterpriseProject.Data;
using EnterpriseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseProject.Context;

public class ManualSecurityContext :  DbContext
{
    public ManualSecurityContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<User>? Users { get; set; }
    public DbSet<Listing>? Listings { get; set; }
    public DbSet<SavedListing>? SavedListings { get; set; }
    public DbSet<UserActivity>? UserActivities { get; set; }
}