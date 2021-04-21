using MovieTracker.App.ViewModels;
using MovieTracker.TMDbModel.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.Services
{
    interface ILetsWatchTrackingService
    {
        Dictionary<int, LetsWatchItemViewModel> DataSource { get; set; }

        void OnUpdate(AccountListType accountListType);

        void Initialize(List<SearchMovie> wlMovies, List<SearchMovie> favMovies, List<SearchMovie> ratedMovies, List<SearchTv> wlShows, List<SearchTv> favShows, List<SearchTv> ratedShows);

        Task<List<SearchMovie>> LoadWatchlistMoviesAsync();

        Task<List<SearchMovie>> LoadFavoriteMoviesAsync();

        Task<List<SearchMovie>> LoadRatedMoviesAsync();

        Task<List<SearchTv>> LoadWatchlistShowsAsync();

        Task<List<SearchTv>> LoadFavoriteShowsAsync();

        Task<List<SearchTv>> LoadRatedShowsAsync();

        Task UpdateWatchlistItem(int mediaID, MediaType mediaType);

        Task UpdateFavoriteItem(int mediaID, MediaType mediaType);

        Task UpdateRatingItem(int mediaID, MediaType mediaType);
    }
}
