using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace EnterpriseMarketplace
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadListings();
        }

        private async void LoadListings()
        {
            var listings = await _apiService.GetListingsAsync();
            ListingsCollectionView.ItemsSource = listings;
        }

        private async void OnContactButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button.BindingContext as Listing;
            var user = await _apiService.GetUserAsync(listing.UserId);

            await DisplayAlert("Contact Seller", $"Username: {user.Username}\nEmail: {user.Email}\nPhone: {user.Phone}", "OK");
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue;
            var listings = await _apiService.GetListingsAsync();
            ListingsCollectionView.ItemsSource = string.IsNullOrWhiteSpace(searchText)
                ? listings
                : listings.FindAll(l => l.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var listing = button.BindingContext as Listing;
            var userId = GetCurrentUserId();

            var savedListing = new SavedListing
            {
                UserId = userId,
                ListingId = listing.ListingId,
                SavedDate = DateTime.Now
            };

            await _apiService.CreateSavedListingAsync(savedListing);
            await DisplayAlert("Success", "Listing saved successfully!", "OK");
        }

        private int GetCurrentUserId()
        {
            var currentUser = Preferences.Get("CurrentUser", string.Empty);
            return string.IsNullOrEmpty(currentUser) ? 0 : JsonConvert.DeserializeObject<User>(currentUser).UserId;
        }

        private void UpdateSignInButtonText(string username)
        {
            var signInButton = this.FindByName<Button>("SignInButton");
            signInButton.Text = $"Welcome, {username}";
        }
    }
}