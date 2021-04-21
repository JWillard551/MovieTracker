using MovieTracker.App.ViewModels;
using MovieTracker.TMDbModel.Services;
using MovieTracker.TMDbModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.Services
{
    public class LetsWatchTrackingService : ILetsWatchTrackingService
    {
        ITMDbService TMDbService = DependencyService.Get<ITMDbService>();

        public Dictionary<int, LetsWatchItemViewModel> DataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void OnUpdate(AccountListType accountListType)
        {
            if (DataSource == null || !DataSource.Any())
                return;
        }

        public void Initialize(List<SearchMovie> wlMovies, List<SearchMovie> favMovies, List<SearchMovie> ratedMovies, List<SearchTv> wlShows, List<SearchTv> favShows, List<SearchTv> ratedShows)
        {

        }

        public async Task<List<SearchMovie>> LoadWatchlistMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var wlMovies = await TMDbService.AccountGetMovieWatchlistAsync(page++);

                if (wlMovies.Page > wlMovies.TotalPages && wlMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlMovies.Results);
            }

            return results;
        }

        public async Task<List<SearchTv>> LoadWatchlistShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var wlShows = await TMDbService.AccountGetTvWatchlistAsync(page++);

                if (wlShows.Page > wlShows.TotalPages && wlShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlShows.Results);
            }

            return results;
        }

        public async Task<List<SearchTv>> LoadFavoriteShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var wlShows = await TMDbService.AccountGetFavoriteTvAsync(page++);

                if (wlShows.Page > wlShows.TotalPages && wlShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlShows.Results);
            }

            return results;
        }

        public async Task<List<SearchMovie>> LoadFavoriteMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var favMovies = await TMDbService.AccountGetFavoriteMoviesAsync(page++);

                if (favMovies.Page > favMovies.TotalPages && favMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(favMovies.Results);
            }

            return results;
        }

        public async Task<List<SearchTv>> LoadRatedShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var ratedShows = await TMDbService.AccountGetRatedTvShowsAsync(page++);

                if (ratedShows.Page > ratedShows.TotalPages && ratedShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(ratedShows.Results);
            }

            return results;
        }

        public async Task<List<SearchMovie>> LoadRatedMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var ratedMovies = await TMDbService.AccountGetRatedMoviesAsync(page++);

                if (ratedMovies.Page > ratedMovies.TotalPages && ratedMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(ratedMovies.Results);
            }

            return results;
        }

        public Task UpdateFavoriteItem(int mediaID, MediaType mediaType)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRatingItem(int mediaID, MediaType mediaType)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWatchlistItem(int mediaID, MediaType mediaType)
        {
            throw new NotImplementedException();
        }
    }
}
