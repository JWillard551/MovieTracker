using MovieTracker.App.Views;
using MovieTracker.App.Views.DetailPages;
using MovieTracker.App.Views.ModalViews;
using MovieTracker.Model.Services;
using System;
using Xamarin.Forms;

namespace MovieTracker.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ILoginService LoginService => DependencyService.Get<ILoginService>();

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SearchOptionsPage), typeof(SearchOptionsPage));
            Routing.RegisterRoute(nameof(MovieTabbedPage), typeof(MovieTabbedPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            LoginService.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
