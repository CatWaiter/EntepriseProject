using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Threading.Tasks;

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
            try
            {
                var userId = GetCurrentUserId();
                var savedListings = await _apiService.GetSavedListingsByUserAsync(userId);
                SavedListingsCollectionView.ItemsSource = savedListings;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load saved listings: {ex.Message}", "OK");
            }
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var savedListing = button.BindingContext as SavedListing;
            try
            {
                await _apiService.DeleteSavedListingAsync(savedListing.SavedListingId);
                LoadSavedListings();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to remove saved listing: {ex.Message}", "OK");
            }
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }
    }
}