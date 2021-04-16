using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Movie;
using MovieTracker.App.ViewModels.DetailViewModels.Show;
using System.Threading.Tasks;
using MovieTracker.App.ViewModels.BaseViewModels;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class FavoritesTabbedPageViewModel : BaseViewModel, IViewModelInitialize
    {
        public Task Initialization { get; private set; }

        public MovieFavoritesViewModel MovieViewModel { get; set; }

        public ShowFavoritesViewModel ShowViewModel { get; set; }

        public FavoritesTabbedPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            MovieViewModel = new MovieFavoritesViewModel();
            ShowViewModel = new ShowFavoritesViewModel();
            await Task.WhenAll(MovieViewModel.Initialization, ShowViewModel.Initialization);
        }
    }
}
