using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace EnterpriseMarketplace
{
    public partial class MyListingsPage : ContentPage
    {
        private List<Listing> myListings;

        public MyListingsPage()
        {
            InitializeComponent();
            LoadMyListings();
        }

        private void LoadMyListings()
        {
            myListings = new List<Listing>
            {
                new Listing { Title = "My Item 1", Location = "New York", Price = 19.99, ImageSource = "item1.jpg", Category = "Apparel" },
                new Listing { Title = "My Item 2", Location = "Los Angeles", Price = 29.99, ImageSource = "item2.jpg", Category = "Electronics" }
            };

            ListingsCollectionView.ItemsSource = myListings;
        }

        private async void OnCreateListingButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateListingPage));
        }
    }
}