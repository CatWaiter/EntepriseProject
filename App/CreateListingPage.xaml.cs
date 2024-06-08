using Microsoft.Maui.Controls;
using System;
using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;

namespace EnterpriseMarketplace
{
    public partial class CreateListingPage : ContentPage
    {
        private readonly ApiService _apiService;

        public CreateListingPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnCreateListingButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var listing = new Listing
                {
                    Title = TitleEntry.Text,
                    Location = LocationEntry.Text,
                    Price = decimal.Parse(PriceEntry.Text),
                    Category = CategoryPicker.SelectedItem.ToString(),
                    PostedDate = DateTime.Now,
                    UserId = GetCurrentUserId()
                };

                await _apiService.CreateListingAsync(listing);
                await DisplayAlert("Success", "Listing created successfully.", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to create listing: {ex.Message}", "OK");
            }
        }

        private int GetCurrentUserId()
        {
            return Preferences.Get("CurrentUserId", 0);
        }
    }
}