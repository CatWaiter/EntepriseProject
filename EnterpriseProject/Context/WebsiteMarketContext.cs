using System;
using System.Collections.Generic;
using EnterpriseProject.Data;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseProject.Context;

public partial class WebsiteMarketContext : DbContext
{
    public WebsiteMarketContext()
    {
    }

    public WebsiteMarketContext(DbContextOptions<WebsiteMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EnterpriseApiApplicationTextItem> EnterpriseApiApplicationTextItems { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<SavedListing> SavedListings { get; set; }

    public virtual DbSet<TextEntry> TextEntries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:enterpriseserver.database.windows.net,1433;Initial Catalog=EnterpriseDB;Persist Security Info=False;User ID=Bill183@enterpriseserver;Password=PTCproject123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnterpriseApiApplicationTextItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__enterpri__3213E83FFB4576B3");

            entity.ToTable("enterprise_api_application$text_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Listings_UserId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Listings).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<SavedListing>(entity =>
        {
            entity.HasIndex(e => e.ListingId, "IX_SavedListings_ListingId");

            entity.HasIndex(e => e.UserId, "IX_SavedListings_UserId");

            entity.HasOne(d => d.Listing).WithMany(p => p.SavedListings)
                .HasForeignKey(d => d.ListingId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.SavedListings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
