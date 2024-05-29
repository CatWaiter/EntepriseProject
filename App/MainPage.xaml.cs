using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace EnterpriseMarketplace
{
    public partial class MainPage : ContentPage
    {
        private List<Listing> listings;

        public MainPage()
        {
            InitializeComponent();
            LoadListings();
        }

        private void LoadListings()
        {
            listings = new List<Listing>
            {
                new Listing { Title = "Sample Item 1", Location = "New York", Price = 19.99, ImageSource = "item1.jpg", Category = "Apparel" },
                new Listing { Title = "Sample Item 2", Location = "Los Angeles", Price = 29.99, ImageSource = "item2.jpg", Category = "Electronics" },
                new Listing { Title = "Sample Item 3", Location = "Chicago", Price = 39.99, ImageSource = "item3.jpg", Category = "Entertainment" },
                new Listing { Title = "Sample Item 4", Location = "Houston", Price = 49.99, ImageSource = "item4.jpg", Category = "Home Improvement" },
                new Listing { Title = "Sample Item 5", Location = "Phoenix", Price = 59.99, ImageSource = "item5.jpg", Category = "Sporting Goods" },
            };

            ListingsCollectionView.ItemsSource = listings;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterListings();
        }

        private void OnCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            FilterListings();
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            FilterListings();
        }

        private void FilterListings()
        {
            var searchText = SearchBar.Text?.ToLower() ?? "";
            var selectedCategory = CategoryPicker.SelectedItem as string;

            var filteredListings = listings.Where(l =>
                (string.IsNullOrEmpty(searchText) || l.Title.ToLower().Contains(searchText) || l.Location.ToLower().Contains(searchText)) &&
                (string.IsNullOrEmpty(selectedCategory) || selectedCategory == "All Categories" || l.Category == selectedCategory)
            ).ToList();

            ListingsCollectionView.ItemsSource = filteredListings;
        }
    }
}