using MovieTracker.App.ViewModels.TabbedPageViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(MovieID), nameof(MovieID))]
    public partial class MovieTabbedPage : TabbedPage
    {
        private string _movieID;
        private MovieTabbedPageViewModel _viewModel;
        public string MovieID
        {
            get => _movieID;
            set
            {
                _movieID = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public MovieTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new MovieTabbedPageViewModel(Convert.ToInt32(MovieID));
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            movieDetail.BindingContext = _viewModel.Tab1;
            castCrew.BindingContext = _viewModel.Tab2;
            watchOn.BindingContext = _viewModel.Tab3;
            _viewModel.Tab1.IsLoaded = true;
        }
    }
}