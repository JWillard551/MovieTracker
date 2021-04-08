using MovieTracker.App.ViewModels.TabbedPageViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingsTabbedPage : TabbedPage
    {
        RatingsTabbedPageViewModel _viewModel;

        public RatingsTabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = new RatingsTabbedPageViewModel();
            await _viewModel.Initialization;
            BindingContext = _viewModel;
            movieTab.BindingContext = _viewModel.MovieViewModel;
            showTab.BindingContext = _viewModel.ShowViewModel;
        }
    }
}