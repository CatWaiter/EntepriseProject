using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace EnterpriseMarketplace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("SignInPage", typeof(SignInPage));
            Routing.RegisterRoute("SavedListingsPage", typeof(SavedListingsPage));
            Routing.RegisterRoute("MyListingsPage", typeof(MyListingsPage));
            Routing.RegisterRoute("CreateListingPage", typeof(CreateListingPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
        }

        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("SignInPage");
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("CurrentUserId");
            Preferences.Remove("CurrentUsername");
            UpdateSignInButtonText(string.Empty);
            await Shell.Current.GoToAsync("SignInPage");
        }

        public void UpdateSignInButtonText(string username)
        {
            var signInButton = this.FindByName<Button>("SignInButton");
            signInButton.Text = string.IsNullOrEmpty(username) ? "Sign In" : $"Welcome, {username}";
        }
    }
}