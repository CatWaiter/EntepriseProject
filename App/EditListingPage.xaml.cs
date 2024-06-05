using System;
using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;

namespace EnterpriseMarketplace
{
    public partial class EditListingPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Listing _listing;

        public EditListingPage(Listing listing)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _listing = listing;
            BindData();
        }

        private void BindData()
        {
            TitleEntry.Text = _listing.Title;
            LocationEntry.Text = _listing.Location;
            PriceEntry.Text = _listing.Price.ToString();
            CategoryPicker.SelectedItem = _listing.Category;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _listing.Title = TitleEntry.Text;
            _listing.Location = LocationEntry.Text;
            _listing.Price = decimal.Parse(PriceEntry.Text);
            _listing.Category = CategoryPicker.SelectedItem.ToString();

            await _apiService.CreateListingAsync(_listing);
            await Navigation.PopAsync();
        }
    }
}