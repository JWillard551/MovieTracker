using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using MovieTracker.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class MovieWatchlistViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<MovieWatchlistItemViewModel> WatchlistMovies { get; set; }
        

        public MovieWatchlistViewModel(int id, int accountId, string sessionId)
        {
            Initialization = InitializeAsync(id, accountId, sessionId);
        }

        private async Task InitializeAsync(int id, int accountId, string sessionId)
        {
            var movies = await TMDbServiceClientHelper.GetMovieWatchlistAsync(accountId, sessionId, 1, new CancellationToken());
            WatchlistMovies = movies.Select(movie => new MovieWatchlistItemViewModel(movie)).ToList();
        }
    }
}
