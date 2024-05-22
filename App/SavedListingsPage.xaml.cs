using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace EnterpriseMarketplace
{
    public partial class SavedListingsPage : ContentPage
    {
        private List<Listing> savedListings;

        public SavedListingsPage()
        {
            InitializeComponent();
            LoadSavedListings();
        }

        private void LoadSavedListings()
        {
            savedListings = new List<Listing>
            {
                new Listing { Title = "Saved Item 1", Location = "New York", Price = 19.99, ImageSource = "item1.jpg", Category = "Apparel" },
                new Listing { Title = "Saved Item 2", Location = "Los Angeles", Price = 29.99, ImageSource = "item2.jpg", Category = "Electronics" }
            };

            ListingsCollectionView.ItemsSource = savedListings;
        }
    }
}