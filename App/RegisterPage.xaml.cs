using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using System;
using Microsoft.Maui.Controls;

namespace EnterpriseMarketplace
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService;

        public RegisterPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                Email = EmailEntry.Text,
                Age = int.Parse(AgeEntry.Text),
                Location = LocationEntry.Text,
                Phone = PhoneEntry.Text,
                AccountCreatedDate = DateTime.UtcNow
            };

            try
            {
                await _apiService.RegisterAsync(user);
                await DisplayAlert("Success", "Registration successful", "OK");
                await Shell.Current.GoToAsync("SignInPage", true);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
            }
        }
    }
}