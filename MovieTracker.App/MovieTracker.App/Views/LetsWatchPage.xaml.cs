using MovieTracker.App.ViewModels;
using MovieTracker.TMDbModel.Utils;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LetsWatchPage : ContentPage
    {
        LetsWatchPageViewModel _viewModel;
        private bool StartUp { get; set; } = true;

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
            ToggleButtons(false);
            int range = _viewModel.OnPickMovie();
            if (range == 0)
                return;
            if (range == 1)
                return;

            var rand = ModelUtils.Randomize(range);

            //var index = Convert.ToInt32(Math.Round((range / 2.0m), MidpointRounding.AwayFromZero));
            var item = _viewModel.FilteredItems[rand];

            cv_watchitems.ScrollTo(item, position:ScrollToPosition.MakeVisible);
            ToggleButtons(true);
        }

        public void OnPickShowClicked(object sender, EventArgs e)
        {
            ToggleButtons(false);
            int range = _viewModel.OnPickShow();
            if (range == 0)
                return;
            if (range == 1)
                return;

            var rand = ModelUtils.Randomize(range);

            //var index = Convert.ToInt32(Math.Round((range / 2.0m), MidpointRounding.AwayFromZero));
            var item = _viewModel.FilteredItems[rand];

            cv_watchitems.ScrollTo(item, position: ScrollToPosition.MakeVisible);
            ToggleButtons(true);
        }

        public void OnPickClicked(object sender, EventArgs e)
        {
            ToggleButtons(false);
            int range = _viewModel.OnPick();
            if (range == 0)
                return;
            if (range == 1)
                return;

            var rand = ModelUtils.Randomize(range);

            //var index = Convert.ToInt32(Math.Round((range / 2.0m), MidpointRounding.AwayFromZero));
            var item = _viewModel.FilteredItems[rand];

            cv_watchitems.ScrollTo(item, position: ScrollToPosition.MakeVisible);
            ToggleButtons(true);
        }
    }
}