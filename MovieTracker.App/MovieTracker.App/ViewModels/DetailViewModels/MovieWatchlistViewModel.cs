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

        public List<MovieItemViewModel> WatchlistMovies { get; set; }
        

        public MovieWatchlistViewModel(int id, int accountId, string sessionId)
        {
            Initialization = InitializeAsync(id, accountId, sessionId);
        }

        private async Task InitializeAsync(int id, int accountId, string sessionId)
        {
            ////var addMovie = await TMDbServiceClientHelper.SetMovieWatchlistAsync(accountId, 8587, sessionId, true, new CancellationToken()); //Lion King add
            ////var addTV = await TMDbServiceClientHelper.SetShowWatchlistAsync(accountId, 2426, sessionId, true, new CancellationToken()); //Angel add
            ////var removeMovie = await TMDbServiceClientHelper.SetMovieWatchlistAsync(accountId, 8587, sessionId, false, new CancellationToken()); //Lion King remove
            ////var removeTV = await TMDbServiceClientHelper.SetShowWatchlistAsync(accountId, 2426, sessionId, false, new CancellationToken()); //Angel remove

            ////var ratedMovies = await TMDbServiceClientHelper.GetAccountRatedMovies(accountId, sessionId, 1, new CancellationToken());
            ////var ratedTV = await TMDbServiceClientHelper.GetAccountRatedShows(accountId, sessionId, 1, new CancellationToken());

            //var favoritesMovies = await TMDbServiceClientHelper.GetAccountFavoriteMovies(accountId, sessionId, 1, new CancellationToken());
            //var favoritesShows = await TMDbServiceClientHelper.GetAccountFavoriteShows(accountId, sessionId, 1, new CancellationToken());

            var movies = await TMDbServiceClientHelper.GetMovieWatchlistAsync(accountId, sessionId, 1, new CancellationToken());
            WatchlistMovies = movies.Select(movie => new MovieItemViewModel(movie)).ToList();
        }
    }
}
