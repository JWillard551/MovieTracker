using MovieTracker.App.ViewModels.TabbedPageViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchlistTabbedPage : TabbedPage
    {
        private WatchlistTabbedViewModel _tabbedPage;

        public WatchlistTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _tabbedPage = new WatchlistTabbedViewModel();
            await _tabbedPage.Initialization;
            BindingContext = _tabbedPage;
            movieTab.BindingContext = _tabbedPage.MovieViewModel;
            showTab.BindingContext = _tabbedPage.ShowViewModel;
        }
    }
}