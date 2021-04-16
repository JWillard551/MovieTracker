using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Movie;
using MovieTracker.App.ViewModels.DetailViewModels.Show;
using System.Threading.Tasks;
using MovieTracker.App.ViewModels.BaseViewModels;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class WatchlistTabbedViewModel : BaseViewModel, IViewModelInitialize
    {
        public Task Initialization { get; private set; }

        public MovieWatchlistViewModel MovieViewModel { get; set; }

        public ShowWatchlistViewModel ShowViewModel { get; set; }

        public WatchlistTabbedViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            MovieViewModel = new MovieWatchlistViewModel();
            ShowViewModel = new ShowWatchlistViewModel();
            await Task.WhenAll(MovieViewModel.Initialization, ShowViewModel.Initialization);
        }
    }
}
