using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;

namespace EnterpriseMarketplace
{
    public partial class MyListingsPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MyListingsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadMyListings();
        }

        private async void LoadMyListings()
        {
            var userId = GetCurrentUserId();
            var listings = await _apiService.GetListingsByUserAsync(userId);
            ListingsCollectionView.ItemsSource = listings;
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button.BindingContext as Listing;
            await Navigation.PushAsync(new EditListingPage(listing));
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button.BindingContext as Listing;
            await _apiService.DeleteListingAsync(listing.ListingId);
            LoadMyListings();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            // Search functionality if needed later
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }
    }
}