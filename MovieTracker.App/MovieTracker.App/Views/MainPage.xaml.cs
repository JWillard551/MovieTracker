using MovieTracker.App.ViewModels;
using MovieTracker.TMDbModel.Utils;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;
        Button _selectedPopButton;
        Button _selectedTrendingButton;
        private bool StartUp { get; set; } = true;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (StartUp)
            {
                _viewModel = new MainPageViewModel();
                await _viewModel.Initialization;
                BindingContext = _viewModel;
                cv_popularItems.ScrollTo(_viewModel.PopularMovies[10], position: ScrollToPosition.MakeVisible);
                cv_trendingItems.ScrollTo(_viewModel.TrendingToday[10], position: ScrollToPosition.MakeVisible);
                StartUp = false;
            }
        }

        public void OnMoviesClicked(object sender, EventArgs e)
        {
            if (!SetSelectedPopularButton(sender as Button))
                return;

            ToggleSelectedColorOnButton(btn_Movies, true);
            ToggleSelectedColorOnButton(btn_Shows, false);
            _viewModel.OnMoviesClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularMovies[ModelUtils.RandomizeNonFirst(_viewModel.PopularMovies.Count)], position:ScrollToPosition.MakeVisible);
        }

        public void OnShowsClicked(object sender, EventArgs e)
        {
            if (!SetSelectedPopularButton(sender as Button))
                return;

            ToggleSelectedColorOnButton(btn_Shows, true);
            ToggleSelectedColorOnButton(btn_Movies, false);
            _viewModel.OnShowsClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularShows[ModelUtils.RandomizeNonFirst(_viewModel.PopularShows.Count)], position: ScrollToPosition.MakeVisible);
        }

        public void OnTrendingTodayClicked(object sender, EventArgs e)
        {
            if (!SetSelectedTrendingButton(sender as Button))
                return;

            ToggleSelectedColorOnButton(btn_Today, true);
            ToggleSelectedColorOnButton(btn_ThisWeek, false);
            _viewModel.OnTrendingTodayClicked();
            cv_trendingItems.ScrollTo(_viewModel.TrendingToday[ModelUtils.RandomizeNonFirst(_viewModel.TrendingToday.Count)], position: ScrollToPosition.MakeVisible);
        }

        public void OnTrendingThisWeekClicked(object sender, EventArgs e)
        {
            if (!SetSelectedTrendingButton(sender as Button))
                return;

            ToggleSelectedColorOnButton(btn_ThisWeek, true);
            ToggleSelectedColorOnButton(btn_Today, false);
            _viewModel.OnTrendingThisWeekClicked();
            cv_trendingItems.ScrollTo(_viewModel.TrendingThisWeek[ModelUtils.RandomizeNonFirst(_viewModel.TrendingThisWeek.Count)], position: ScrollToPosition.MakeVisible);
        }

        private void ToggleSelectedColorOnButton(Button button, bool isSelected)
        {
            button.BackgroundColor = isSelected ? Color.FromHex("3FA382") : Color.FromHex("032541");
        }

        private bool SetSelectedPopularButton(Button button)
        {
            if (_selectedPopButton == button)
                return false;

            _selectedPopButton = button;
            return true;
        }

        private bool SetSelectedTrendingButton(Button button)
        {
            if (_selectedTrendingButton == button)
                return false;

            _selectedTrendingButton = button;
            return true;
        }
    }
}