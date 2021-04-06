using MovieTracker.TMDbModel.Client;
using MovieTracker.TMDbModel.ModelObjects;
using System;
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
using Xamarin.Essentials;

namespace MovieTracker.TMDbModel.Services
{
    public class TMDbService : ITMDbService
    {
        private readonly string ACCOUNT_ID = "account_id";
        private readonly string SESSION_ID_TOKEN = "session_id_token";
        private readonly string LANGUAGE_CODE = "en_US";
        public int PageSize = 20;

        public string CurrentQuery { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public async Task<bool> Authenticate(Credentials creds)
        {
            try
            {
                if (await HasActiveSessionID())
                {
                    //return false;
                }

                var userSession = await TMDbServiceClient.Instance.AuthenticationGetUserSessionAsync(creds.Username, creds.Password);
                if (userSession != null)
                {
                    await SetSessionID(userSession.SessionId);
                }

                return true;
            }
            catch
            {
                Console.WriteLine("AccountService.LoginAccountAsync(Credentials creds) - Exception occurred.");
                return false;
            }
        }

        public async Task<OperationResult> LogoutAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await TMDbServiceClient.ExtendedInstance.AccountService.LogoutAsync(sessionId, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    return new OperationResult(false, message);
                }
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message);
            }
        }

        public async Task<string> GetSessionIDAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(SESSION_ID_TOKEN);
            }
            catch
            {
                Console.WriteLine("AccountService.GetSessionIDAsync() - Exception occurred.");
                return string.Empty;
            }
        }

        public async Task SetSessionID(string id)
        {
            try
            {
                var existingID = await SecureStorage.GetAsync(SESSION_ID_TOKEN);
                if (!string.IsNullOrWhiteSpace(existingID))
                {
                    try
                    {
                        //Attempt to remove old session ID.
                        var result = await LogoutAsync(existingID);
                    }
                    catch
                    {
                        //Nothing we can do at this point. Just let the session Id be replaced.
                    }
                }

                await SecureStorage.SetAsync(SESSION_ID_TOKEN, id);
            }
            catch
            {
                Console.WriteLine("AccountService.SetSessionID(string sessionId) - Exception occurred.");
            }
        }

        public async Task<bool> HasActiveSessionID()
        {
            try
            {
                string sessionID = await GetSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionID))
                    return false;

                await TMDbServiceClient.Instance.SetSessionInformationAsync(sessionID, TMDbLib.Objects.Authentication.SessionType.UserSession);

                return true;
            }
            catch
            {
                Console.WriteLine("AccountService.HasActiveSessionID() - Exception occurred.");
                return false;
            }
        }

        public OperationResult ClearFromStorage()
        {
            if (!SecureStorage.Remove(ACCOUNT_ID))
                return new OperationResult(false, "Account ID could not be removed from secure storage");

            if (!SecureStorage.Remove(SESSION_ID_TOKEN))
                return new OperationResult(false, "Session ID could not be removed from secure storage");

            return new OperationResult(true, string.Empty);
        }

        public Task<bool> AccountChangeFavoriteStatusAsync(MediaType mediaType, int mediaId, bool isFavorite, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AccountChangeWatchlistStatusAsync(MediaType mediaType, int mediaId, bool isOnWatchlist, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await TMDbServiceClient.Instance.AccountChangeWatchlistStatusAsync(mediaType, mediaId, isOnWatchlist, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public Task<SearchContainer<SearchMovie>> AccountGetFavoriteMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchTv>> AccountGetFavoriteTvAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<SearchContainer<SearchMovie>> AccountGetMovieWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            return await TMDbServiceClient.Instance.AccountGetMovieWatchlistAsync(page, sortBy, sortOrder, language, cancellationToken);
        }

        public Task<SearchContainer<SearchMovieWithRating>> AccountGetRatedMoviesAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<AccountSearchTv>> AccountGetRatedTvShowsAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchTv>> AccountGetTvWatchlistAsync(int page = 1, AccountSortBy sortBy = AccountSortBy.Undefined, SortOrder sortOrder = SortOrder.Undefined, string language = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResultContainer<Dictionary<string, WatchProviders>>> GetMovieWatchProvidersAsync(int movieId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(int personId, PersonMethods extraMethods = PersonMethods.Undefined, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchMovie>> GetPopularMoviesAsync(string language, int page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchTv>> GetPopularShowsAsync(string language, int page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchMovie>> GetTrendingMoviesAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchTv>> GetTrendingTvAsync(TimeWindow timeWindow, int page = 0, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TvShow> GetTVShowAsync(int showId, TvShowMethods extraMethods = TvShowMethods.Undefined, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieRemoveRatingAsync(int movieId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MovieSetRatingAsync(int movieId, double rating, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void PopulateSearchResultFromResourceAsync(SearchBase searchBase)
        {
            throw new NotImplementedException();
        }

        public Task<SearchContainer<SearchBase>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TvEpisodeRemoveRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TvEpisodeSetRatingAsync(int tvShowId, int seasonNumber, int episodeNumber, double rating, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TvShowRemoveRatingAsync(int tvShowId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TvShowSetRatingAsync(int tvShowId, double rating, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateQueryAndPage(string query)
        {
            throw new NotImplementedException();
        }

        
    }
}
