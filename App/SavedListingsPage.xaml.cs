using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;

namespace EnterpriseMarketplace
{
    public partial class SavedListingsPage : ContentPage
    {
        private readonly ApiService _apiService;

        public SavedListingsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadSavedListings();
        }

        private async void LoadSavedListings()
        {
            var userId = GetCurrentUserId();
            var savedListings = await _apiService.GetSavedListingsAsync(userId);
            SavedListingsCollectionView.ItemsSource = savedListings;
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var savedListing = button.BindingContext as SavedListing;
            await _apiService.DeleteSavedListingAsync(savedListing.SavedListingId);
            LoadSavedListings();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            // Search if needed later
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }
    }
}