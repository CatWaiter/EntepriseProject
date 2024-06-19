using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace EnterpriseMarketplace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            ClearUserSession();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("SignInPage", typeof(SignInPage));
            Routing.RegisterRoute("SavedListingsPage", typeof(SavedListingsPage));
            Routing.RegisterRoute("MyListingsPage", typeof(MyListingsPage));
            Routing.RegisterRoute("CreateListingPage", typeof(CreateListingPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("WeatherPage", typeof(WeatherPage));
            DisableFlyout();
            RefreshNavigationBar();
        }
        private void ClearUserSession()
        {
            Preferences.Remove("CurrentUserId");
            Preferences.Remove("CurrentUsername");
        }
        public void EnableFlyout()
        {
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
        }

        public void DisableFlyout()
        {
            this.FlyoutBehavior = FlyoutBehavior.Disabled;
        }


        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            RefreshNavigationBar();
            await Shell.Current.GoToAsync("SignInPage");
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Remove("CurrentUserId");
            Preferences.Remove("CurrentUsername");
            UpdateSignInButtonText(string.Empty);
            RefreshNavigationBar();
            DisableFlyout();
            await Shell.Current.GoToAsync("SignInPage");
        }

        public void UpdateSignInButtonText(string username)
        {
            var signInButton = this.FindByName<Button>("SignInButton");
            signInButton.Text = string.IsNullOrEmpty(username) ? "Sign In" : $"Welcome, {username}";
        }
        public void RefreshNavigationBar()
        {
            var username = Preferences.Get("CurrentUsername", string.Empty);
            UpdateSignInButtonText(username);
        }
        public void UpdateNavigationBar()
        {
            var username = Preferences.Get("CurrentUsername", string.Empty);
            var signInButton = this.FindByName<Button>("SignInButton");
            if (string.IsNullOrEmpty(username))
            {
                signInButton.Text = "Sign In";
            }
            else
            {
                signInButton.Text = $"Welcome, {username}";
            }
        }
    }
}