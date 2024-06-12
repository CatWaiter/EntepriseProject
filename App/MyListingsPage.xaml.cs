using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpriseMarketplace
{
    public partial class MyListingsPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Listing> MyListings { get; set; } = new ObservableCollection<Listing>();
        public ICommand LoadCommand { get; set; }

        public MyListingsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCommand = new Command(ExecuteLoadCommand);
            BindingContext = this;
            LoadMyListings();
        }
        private void ExecuteLoadCommand()
        {
            IsBusy = true;
            LoadMyListings();
            IsBusy = false;
        }

        private async void LoadMyListings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var myListings = await _apiService.GetListingsByUserAsync(userId);
                MyListings.Clear();
                foreach (var listing in myListings)
                {
                    MyListings.Add(listing);
                }
                MyListingsCollectionView.ItemsSource = MyListings;
            }
            catch (Exception ex)
            {
                // no message for this
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
            if (listing == null) return;

            bool isDeleted = await _apiService.DeleteListingAsync(listing.ListingId);
            if (isDeleted)
            {
                MyListings.Remove(listing);
            }
            else
            {
                // no message for this
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