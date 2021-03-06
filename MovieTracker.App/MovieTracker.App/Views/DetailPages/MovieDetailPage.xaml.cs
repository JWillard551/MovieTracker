using MovieTracker.App.ViewModels.DetailViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(MovieID), nameof(MovieID))]
    public partial class MovieDetailPage : ContentPage
    {
        private string _movieID;
        private MovieDetailViewModel _movieDetailViewModel;
        public string MovieID
        {
            get => _movieID;
            set
            {
                _movieID = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public MovieDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _movieDetailViewModel = new MovieDetailViewModel(Convert.ToInt32(MovieID));
            await _movieDetailViewModel.Initialization;
            BindingContext = _movieDetailViewModel;
        }
    }
}