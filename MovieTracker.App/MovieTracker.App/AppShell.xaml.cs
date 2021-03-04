using MovieTracker.App.ViewModels;
using MovieTracker.App.Views;
using MovieTracker.App.Views.ModalViews;
using System;
using System.Collections.Generic;
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
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
