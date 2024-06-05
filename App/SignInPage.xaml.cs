using EnterpriseMarketplace.Models;
using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace EnterpriseMarketplace
{
    public partial class SignInPage : ContentPage
    {
        private readonly ApiService _apiService;

        public SignInPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            try
            {
                var user = await _apiService.SignInAsync(username, password);
                if (user != null)
                {
                    Preferences.Set("CurrentUser", JsonConvert.SerializeObject(user));
                    AppShell.Current.FlyoutHeader = new Label { Text = $"Welcome, {user.Username}" };
                    UpdateSignInButtonText(user.Username);
                    await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    await DisplayAlert("Error", "Sign in failed: Invalid username or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Sign in failed: {ex.Message}", "OK");
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        private void UpdateSignInButtonText(string username)
        {
            var signInButton = (Shell.Current as AppShell).FindByName<Button>("SignInButton");
            signInButton.Text = username;
        }
    }
}