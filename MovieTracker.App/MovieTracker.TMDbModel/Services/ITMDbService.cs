using MovieTracker.TMDbModel.AdditionalModelObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Account;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Trending;
using TMDbLib.Objects.TvShows;

namespace MovieTracker.TMDbModel.Services
{
    public interface ITMDbService
    {
        string CurrentQuery { get; set; }
        int CurrentPage { get; set; }
        int TotalPages { get; set; }

        #region Login / Authentication Methods

        Task<OperationResult> LoginAndSetSessionAsync(Credentials creds);

        Task<OperationResult> LogoutSessionAsync(CancellationToken cancellationToken = default);

        Task<bool> HasExistingSessionIDAsync();

        #endregion

        Task<SearchContainer<SearchBase>> SearchAsync(string query, CancellationToken cancellationToken = default);

        void UpdateQueryAndPage(string query);

        Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default);

        Task<SingleResultContainer<Dictionary<string, WatchProviders>>> GetWatchProvidersAsync(int mediaId, MediaType mediaType, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchMovie>> GetPopularMoviesAsync(string language, int page, CancellationToken cancellationToken = default);

        Task<bool> MovieSetRatingAsync(int movieId, double rating, CancellationToken cancellationToken = default);

        Task<bool> MovieRemoveRatingAsync(int movieId, CancellationToken cancellationToken = default);

        Task<TvShow> GetTVShowAsync(int showId, TvShowMethods extraMethods = TvShowMethods.Undefined, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchTv>> GetPopularShowsAsync(string language, int page, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchMovie>> GetTrendingMoviesAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchTv>> GetTrendingTvAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default);

        Task<bool> TvShowSetRatingAsync(int tvShowId, double rating, CancellationToken cancellationToken = default);

        Task<bool> TvShowRemoveRatingAsync(int tvShowId, CancellationToken cancellationToken = default);

        Task<bool> TvEpisodeSetRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, double rating, CancellationToken cancellationToken = default);

        Task<bool> TvEpisodeRemoveRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, CancellationToken cancellationToken = default);

        Task<Person> GetPersonAsync(int personId, PersonMethods extraMethods = PersonMethods.Undefined, CancellationToken cancellationToken = default);

        #region TvShows

        Task<AccountState> GetTvShowAccountStateAsync(int tvShowId, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchTv>> AccountGetTvWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchTv>> AccountGetFavoriteTvAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);

        Task<SearchContainer<AccountSearchTv>> AccountGetRatedTvShowsAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);
        
        #endregion

        #region Movies

        Task<AccountState> GetMovieAccountStateAsync(int movieId, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchMovie>> AccountGetMovieWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchMovie>> AccountGetFavoriteMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);

        Task<SearchContainer<SearchMovieWithRating>> AccountGetRatedMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default);
        
        #endregion

        Task<bool> AccountChangeFavoriteStatusAsync(MediaType mediaType, int mediaId, bool isFavorite, CancellationToken cancellationToken = default);

        Task<bool> AccountChangeWatchlistStatusAsync(MediaType mediaType, int mediaId, bool isOnWatchlist, CancellationToken cancellationToken = default);
    }
}
