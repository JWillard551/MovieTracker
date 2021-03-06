using MovieTracker.App.Views;
using MovieTracker.App.Views.DetailPages;
using MovieTracker.App.Views.ModalViews;
using System;
using Xamarin.Forms;

namespace MovieTracker.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SearchOptionsPage), typeof(SearchOptionsPage));
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
            Routing.RegisterRoute(nameof(ShowDetailPage), typeof(ShowDetailPage));
            Routing.RegisterRoute(nameof(PersonDetailPage), typeof(PersonDetailPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
