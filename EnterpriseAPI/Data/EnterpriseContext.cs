using Microsoft.EntityFrameworkCore;
using EnterpriseAPI.Models;

namespace EnterpriseAPI.Data
{
    public class EnterpriseContext : DbContext
    {
        public EnterpriseContext(DbContextOptions<EnterpriseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<SavedListing> SavedListings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Listings)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedListings)
                .WithOne(sl => sl.User)
                .HasForeignKey(sl => sl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Listing>()
                .HasMany(l => l.SavedListings)
                .WithOne(sl => sl.Listing)
                .HasForeignKey(sl => sl.ListingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Listing>()
                .Property(l => l.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}