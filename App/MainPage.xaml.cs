using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace EnterpriseMarketplace
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ICommand LoadCommand { get; set; }

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCommand = new Command(ExecuteLoadCommand);
            BindingContext = this;
            LoadListings();
        }
        private void ExecuteLoadCommand()
        {
            IsBusy = true;
            LoadListings();
            IsBusy = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadListings();
            (AppShell.Current as AppShell)?.UpdateNavigationBar();
        }

        private async void CheckIfLoggedIn()
        {
            var currentUserId = Preferences.Get("CurrentUserId", 0);
            if (currentUserId == 0)
            {
                await Shell.Current.GoToAsync(nameof(SignInPage));
            }
        }

        private async Task LoadListings()
        {
            try
            {
                var listings = await _apiService.GetListingsAsync();
                ListingsCollectionView.ItemsSource = listings;
            }
            catch (Exception ex)
            {
                //no message for this
            }
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var listing = button?.BindingContext as Listing;
                var savedListing = new SavedListing
                {
                    UserId = GetCurrentUserId(),
                    ListingId = listing.ListingId,
                    SavedDate = DateTime.Now
                };

                await _apiService.CreateSavedListingAsync(savedListing);
                await DisplayAlert("Success", "Listing saved successfully.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save listing: {ex.Message}", "OK");
            }
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
    }
}