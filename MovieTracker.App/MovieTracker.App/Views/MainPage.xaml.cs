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
            _viewModel.OnMoviesClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularMovies[10], position:ScrollToPosition.MakeVisible);
        }

        public void OnShowsClicked(object sender, EventArgs e)
        {
            _viewModel.OnShowsClicked();
            cv_popularItems.ScrollTo(_viewModel.PopularShows[10], position: ScrollToPosition.MakeVisible);
        }
    }
}