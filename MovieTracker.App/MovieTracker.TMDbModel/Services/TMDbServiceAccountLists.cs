using MovieTracker.TMDbModel.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Account;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieTracker.TMDbModel.Services
{
    public partial class TMDbService : ITMDbService
    {
        #region Account Watchlist & States

        public async Task<AccountState> GetMovieAccountStateAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetMovieAccountStateAsync(movieId, cancellationToken);
        }

        public async Task<AccountState> GetTvShowAccountStateAsync(int showId, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.GetTvShowAccountStateAsync(showId, cancellationToken);
        }

        public async Task<SearchContainer<SearchMovie>> AccountGetMovieWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetMovieWatchlistAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<SearchContainer<SearchTv>> AccountGetTvWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetTvWatchlistAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<bool> AccountChangeWatchlistStatusAsync(MediaType mediaType, int mediaId, bool isOnWatchlist, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountChangeWatchlistStatusAsync(mediaType, mediaId, isOnWatchlist, cancellationToken);
        }

        #endregion

        #region Account Favorites

        public async Task<SearchContainer<SearchMovie>> AccountGetFavoriteMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetFavoriteMoviesAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<SearchContainer<SearchTv>> AccountGetFavoriteTvAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetFavoriteTvAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<bool> AccountChangeFavoriteStatusAsync(MediaType mediaType, int mediaId, bool isFavorite, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountChangeFavoriteStatusAsync(mediaType, mediaId, isFavorite, cancellationToken);
        }

        #endregion

        #region Account Ratings

        public async Task<SearchContainer<SearchMovieWithRating>> AccountGetRatedMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetRatedMoviesAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<SearchContainer<AccountSearchTv>> AccountGetRatedTvShowsAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetRatedTvShowsAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public async Task<bool> MovieRemoveRatingAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.MovieRemoveRatingAsync(movieId, cancellationToken);
        }

        public async Task<bool> MovieSetRatingAsync(int movieId, double rating, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.MovieSetRatingAsync(movieId, rating, cancellationToken);
        }

        public async Task<bool> TvEpisodeRemoveRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.TvEpisodeRemoveRatingAsync(tvShowId, seasonNumber, episodeNumber, cancellationToken);
        }

        public async Task<bool> TvEpisodeSetRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, double rating, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.TvEpisodeSetRatingAsync(tvShowId, seasonNumber, episodeNumber, rating, cancellationToken);
        }

        public async Task<bool> TvShowRemoveRatingAsync(int tvShowId, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.TvShowRemoveRatingAsync(tvShowId, cancellationToken);
        }

        public async Task<bool> TvShowSetRatingAsync(int tvShowId, double rating, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.TvShowSetRatingAsync(tvShowId, rating, cancellationToken);
        }

        #endregion
    }
}
