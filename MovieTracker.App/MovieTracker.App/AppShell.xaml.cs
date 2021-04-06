﻿using MovieTracker.App.Services;
using MovieTracker.App.Views;
using MovieTracker.App.Views.DetailPages;
using MovieTracker.App.Views.ModalViews;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Services;
using System;
using Xamarin.Forms;

namespace MovieTracker.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ITMDbService TMDbService => DependencyService.Get<ITMDbService>();
        public IMessage ToastService => DependencyService.Get<IMessage>();

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SearchOptionsPage), typeof(SearchOptionsPage));
            Routing.RegisterRoute(nameof(MovieTabbedPage), typeof(MovieTabbedPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {

            //var response = await TMDbService.LogoutAccountAsync();
            if (true)
            {
                //TMDbService.ClearFromStorage();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                ToastService.LongAlertMessage("Failed!");
                //ToastService.LongAlertMessage(response.Message);
            }
        }
    }
}
