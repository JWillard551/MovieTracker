using MovieTracker.App.ViewModels.TabbedPageViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistTabbedPage : TabbedPage
    {
        private WatchlistTabbedViewModel _tabbedPage;
        private string _movieID;
        public string MovieID
        {
            get => _movieID;
            set
            {
                _movieID = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public WatchlistTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _tabbedPage = new WatchlistTabbedViewModel(Convert.ToInt32(MovieID));
            await _tabbedPage.Initialization;
            BindingContext = _tabbedPage;
            movieTab.BindingContext = _tabbedPage.MovieViewModel;
            showTab.BindingContext = _tabbedPage.ShowViewModel;
        }
    }
}