using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class MovieWatchlistViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<MovieItemViewModel> WatchlistMovies { get; set; }
        

        public MovieWatchlistViewModel(string sessionId)
        {
            Initialization = InitializeAsync(sessionId);
        }

        private async Task InitializeAsync(string sessionId)
        {
            var movies = await TMDbService.AccountGetMovieWatchlistAsync();
            WatchlistMovies = movies.Results.Select(movie => new MovieItemViewModel(movie)).ToList();
        }
    }
}
