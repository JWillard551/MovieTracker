using MovieTracker.App.ViewModels;
using MovieTracker.TMDbModel.Utils;
using System;
using TMDbLib.Objects.General;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LetsWatchPage : ContentPage
    {
        LetsWatchPageViewModel _viewModel;
        public LetsWatchPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            ToggleIsBusy(true);
            ToggleButtons(false);
            base.OnAppearing();
            _viewModel = new LetsWatchPageViewModel();
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            ToggleIsBusy(false);
            ToggleButtons(true);
        }

        private void ToggleButtons(bool toggle)
        {
            pickMovie_btn.IsEnabled = toggle;
            pickShow_btn.IsEnabled = toggle;
            pick_btn.IsEnabled = toggle;
        }

        private void ToggleIsBusy(bool toggle)
        {
            loadingIndicator.IsRunning = toggle;
            loadingIndicator.IsVisible = toggle;
        }

        public void OnPickMovieClicked(object sender, EventArgs e)
        {
            OnShuffle(_viewModel.FilterItemsByMediaType(MediaType.Movie));
        }

        public void OnPickShowClicked(object sender, EventArgs e)
        {
            OnShuffle(_viewModel.FilterItemsByMediaType(MediaType.Tv));
        }

        public void OnPickClicked(object sender, EventArgs e)
        {
            OnShuffle(_viewModel.FilterItemsByMediaType());
        }

        private void OnShuffle(int range)
        {
            ToggleButtons(false);
            if (range == 0)
            {
                ToggleButtons(true);
                return;
            }

            var item = _viewModel.FilteredItems[ModelUtils.Randomize(range)];
            cv_watchitems.ScrollTo(item, position: ScrollToPosition.MakeVisible);
            ToggleButtons(true);
        }
    }
}