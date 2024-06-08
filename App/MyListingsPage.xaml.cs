using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var userId = GetCurrentUserId();
                var listings = await _apiService.GetListingsByUserAsync(userId);
                MyListingsCollectionView.ItemsSource = listings;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load listings: {ex.Message}", "OK");
            }
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }

        private async void OnCreateListingButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateListingPage));
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button.BindingContext as Listing;
            try
            {
                await _apiService.DeleteListingAsync(listing.ListingId);
                LoadMyListings();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to remove listing: {ex.Message}", "OK");
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button?.BindingContext as Listing;
            if (listing != null)
            {
                await Navigation.PushAsync(new EditListingPage(listing));
            }
        }
    }
}