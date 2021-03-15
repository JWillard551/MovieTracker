using MovieTracker.App.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(MovieID), nameof(MovieID))]
    public partial class MovieTabbedPage : TabbedPage
    {
        private string _movieID;
        private MovieTabbedPageViewModel _tabbedPage;
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
            _tabbedPage = new MovieTabbedPageViewModel(Convert.ToInt32(MovieID));
            await _tabbedPage.Initialization;
            BindingContext = _tabbedPage;
        }
    }
}