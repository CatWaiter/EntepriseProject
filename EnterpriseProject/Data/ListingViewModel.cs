namespace EnterpriseProject.Data
{
    public class ListingViewModel
    {
        public int ListingId { get; set; }

        public string Title { get; set; }

        public IFormFile PictureUrl { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public decimal Price { get; set; }

        public string? Category { get; set; }

        public DateTime PostedDate { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<SavedListing> SavedListings { get; set; } = new List<SavedListing>();

        public virtual User User { get; set; } = null!;
    }
}
