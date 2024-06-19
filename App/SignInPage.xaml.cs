using EnterpriseMarketplace.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
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
            VerificationCheckBox.CheckedChanged += OnVerificationChecked;
        }

        private void OnVerificationChecked(object sender, CheckedChangedEventArgs e)
        {
            SignInButton.IsEnabled = e.Value;
        }

        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (!VerificationCheckBox.IsChecked)
            {
                await DisplayAlert("Verification Required", "Please complete the verification.", "OK");
                return;
            }

            try
            {
                var user = await _apiService.SignInAsync(username, password);
                if (user != null)
                {
                    Preferences.Set("CurrentUserId", user.UserId);
                    Preferences.Set("CurrentUsername", user.Username);
                    (App.Current.MainPage as AppShell).UpdateSignInButtonText(user.Username);
                    ((AppShell)Shell.Current).EnableFlyout();
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
    }
}