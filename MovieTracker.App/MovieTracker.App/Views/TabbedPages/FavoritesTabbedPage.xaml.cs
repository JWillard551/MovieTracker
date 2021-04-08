using MovieTracker.App.ViewModels.TabbedPageViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesTabbedPage : TabbedPage
    {
        FavoritesTabbedPageViewModel _viewModel;

        public FavoritesTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new FavoritesTabbedPageViewModel();
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            movieTab.BindingContext = _viewModel.MovieViewModel;
            showTab.BindingContext = _viewModel.ShowViewModel;
        }
    }
}