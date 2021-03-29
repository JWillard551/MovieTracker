using MovieTracker.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new MainPageViewModel();
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            cv_popularItems.ScrollTo(_viewModel.PopularMovies[10], position: ScrollToPosition.MakeVisible);
        }

        public void OnMoviesClicked(object sender, EventArgs e)
        {
            ToggleSelectedColorOnButton(btn_Movies, true);
            ToggleSelectedColorOnButton(btn_Shows, false);
            _viewModel.OnMoviesClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularMovies[10], position:ScrollToPosition.MakeVisible);
        }

        public void OnShowsClicked(object sender, EventArgs e)
        {
            ToggleSelectedColorOnButton(btn_Shows, true);
            ToggleSelectedColorOnButton(btn_Movies, false);
            _viewModel.OnShowsClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularShows[10], position: ScrollToPosition.MakeVisible);
        }

        public void OnTrendingTodayClicked(object sender, EventArgs e)
        {
            ToggleSelectedColorOnButton(btn_Today, true);
            ToggleSelectedColorOnButton(btn_ThisWeek, false);
            _viewModel.OnTrendingTodayClicked();
            cv_trendingItems.ScrollTo(_viewModel.TrendingToday[10], position: ScrollToPosition.MakeVisible);
        }

        public void OnTrendingThisWeekClicked(object sender, EventArgs e)
        {
            ToggleSelectedColorOnButton(btn_ThisWeek, true);
            ToggleSelectedColorOnButton(btn_Today, false);
            _viewModel.OnShowsClicked();
            cv_trendingItems.ScrollTo(_viewModel.TrendingThisWeek[10], position: ScrollToPosition.MakeVisible);
        }

        private void ToggleSelectedColorOnButton(Button button, bool isSelected)
        {
            button.BackgroundColor = isSelected ? Color.FromHex("3F92A3") : Color.FromHex("3FA382");
        }
    }
}