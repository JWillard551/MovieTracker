using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.TMDbModel.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class WatchlistTabbedViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public MovieWatchlistViewModel MovieViewModel { get; set; }

        public ShowWatchlistViewModel ShowViewModel { get; set; }

        public WatchlistTabbedViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            MovieViewModel = new MovieWatchlistViewModel();
            ShowViewModel = new ShowWatchlistViewModel();
            await Task.WhenAll(MovieViewModel.Initialization, ShowViewModel.Initialization);
        }
    }
}
