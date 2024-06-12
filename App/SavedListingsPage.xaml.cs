using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpriseMarketplace
{
    public partial class SavedListingsPage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<SavedListing> SavedListings { get; set; } = new ObservableCollection<SavedListing>();
        public ICommand LoadCommand { get; set; }

        public SavedListingsPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCommand = new Command(ExecuteLoadCommand);
            BindingContext = this;
            LoadSavedListings();
        }

        private void ExecuteLoadCommand()
        {
            IsBusy = true;
            LoadSavedListings();
            IsBusy = false;
        }

        private async void LoadSavedListings()
        {
            try
            {
                var userId = GetCurrentUserId();
                var savedListings = await _apiService.GetSavedListingsByUserAsync(userId);
                SavedListings.Clear();
                foreach (var listing in savedListings)
                {
                    SavedListings.Add(listing);
                }
                SavedListingsCollectionView.ItemsSource = SavedListings;
            }
            catch (Exception ex)
            {
                // no message for this
            }
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var savedListing = button.BindingContext as SavedListing;
            try
            {
                await _apiService.DeleteSavedListingAsync(savedListing.SavedListingId);
                SavedListings.Remove(savedListing);
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
    }
}