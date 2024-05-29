namespace EnterpriseMarketplace
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(SavedListingsPage), typeof(SavedListingsPage));
            Routing.RegisterRoute(nameof(MyListingsPage), typeof(MyListingsPage));
            Routing.RegisterRoute(nameof(CreateListingPage), typeof(CreateListingPage));
        }

        private async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SignInPage));
        }
    }
}