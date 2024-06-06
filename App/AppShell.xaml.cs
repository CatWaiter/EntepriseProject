using EnterpriseMarketplace.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace EnterpriseMarketplace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(SavedListingsPage), typeof(SavedListingsPage));
            Routing.RegisterRoute(nameof(MyListingsPage), typeof(MyListingsPage));
            Routing.RegisterRoute(nameof(CreateListingPage), typeof(CreateListingPage));

            UpdateSignInButtonText();
        }

        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SignInPage));
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }


        private void UpdateSignInButtonText()
        {
            var currentUser = Preferences.Get("CurrentUser", string.Empty);
            if (!string.IsNullOrEmpty(currentUser))
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(currentUser);
                var signInButton = this.FindByName<Button>("SignInButton");
                signInButton.Text = user.Username;
            }
        }
    }
}