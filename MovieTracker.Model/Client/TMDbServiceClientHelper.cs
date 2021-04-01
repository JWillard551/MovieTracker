using MovieTracker.Model.Client.ExtendedClient;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using MovieTracker.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieTracker.Model.Client
{
    public static class TMDbServiceClientHelper
    {
        private static readonly string LANGUAGE_CODE = "en_US";
        private static string CurrentQuery { get; set; } = string.Empty;
        public static int CurrentPage { get; private set; }
        public static int TotalPages { get; private set; }

        public static int PageSize = 20;

        /// <summary>
        /// Search multiple models in a single request. Multi search currently supports searching for movies, tv shows and people in a single request.
        /// </summary>
        /// <param name="query">Search text.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken ct)
        {
            try
            {
                UpdateQueryAndPage(query);
                var result = await TMDbServiceClient.Instance.SearchAsync(CurrentQuery, LANGUAGE_CODE, false, CurrentPage, ct);
                TotalPages = result.PageCount;
                var results = ExtractDataFromResults(result.Results);
                return results;
            }
            catch (Exception e)
            {

            }
            return new List<SearchResult>();
        }

        public static async Task<Movie> GetMovieDetailsById(int movieId, CancellationToken ct)
        {
            try
            {
                List<Task> tasks = new List<Task>();
                var movieDetailTask = TMDbServiceClient.Instance.Movies.GetAsync(movieId, LANGUAGE_CODE, true, ct);
                var ratingDetailTask = TMDbServiceClient.Instance.Movies.GetReleasesAsync(movieId, ct);

                await Task.WhenAll(movieDetailTask, ratingDetailTask);

                var result = await movieDetailTask;
                var releases = await ratingDetailTask;

                return new Movie(result, releases);
            }
            catch (Exception ex)
            {

            }
            return new Movie();
        }

        public static async Task<List<MediaCredits>> GetMovieCreditsById(int movieId, CancellationToken ct)
        {
            try
            {
                var movie = await TMDbServiceClient.Instance.Movies.GetAsync(movieId, LANGUAGE_CODE, true, ct);
                return Utils.ModelUtils.GetMovieMediaCredits(movie);
            }
            catch (Exception ex)
            {

            }
            return new List<MediaCredits>();
        }

        public static async Task<ProviderModelDetails> GetMovieProvidersById(int movieId, CancellationToken ct)
        {
            try
            {
                var providers = await TMDbServiceClient.ExtendedInstance.Providers.GetProvidersAsync(movieId, MediaType.Movie, ct);

                if (providers?.Results?.US != null)
                    return new ProviderModelDetails(providers.Results.US); //TODO: We can return other regions. For now we just want US providers.
                else
                    return new ProviderModelDetails();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static async Task<ObservableCollection<PopularItem>> GetPopularMovies(CancellationToken ct)
        {
            try
            {
                var popular = await TMDbServiceClient.Instance.Movies.GetPopularAsync(LANGUAGE_CODE, 1, ct);
                var popularItems = new ObservableCollection<PopularItem>(popular.Results.Select(movie => new PopularItem(movie)));
                return StaggerPopularItemResults(popularItems);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static async Task<ObservableCollection<PopularItem>> GetPopularShows(CancellationToken ct)
        {
            try
            {
                var popular = await TMDbServiceClient.Instance.Shows.GetPopularAsync(LANGUAGE_CODE, 1, ct);
                var popularItems = new ObservableCollection<PopularItem>(popular.Results.Select(show => new PopularItem(show)));
                return StaggerPopularItemResults(popularItems);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private static ObservableCollection<PopularItem> StaggerPopularItemResults(ObservableCollection<PopularItem> popularItems)
        {
            ObservableCollection<PopularItem> result = new ObservableCollection<PopularItem>();

            //A bit of a hack, but we want the carousel view this will be displayed on to be midway through.
            if (popularItems.Count == 20)
            {
                for (int i = 10; i < popularItems.Count; i++)
                    result.Add(popularItems[i]);
                for (int i = 0; i < 10; i++)
                    result.Add(popularItems[i]);
            }
            return result;
        }

        public static async Task<Show> GetShowDetailsById(int showId, CancellationToken ct)
        {
            try
            {
                var result = await TMDbServiceClient.Instance.Shows.GetAsync(showId, LANGUAGE_CODE, true, ct);
                return new Show(result);
            }
            catch (Exception ex)
            {

            }
            return new Show();
        }

        public static async Task<Person> GetPersonDetailsById(int personId, CancellationToken ct)
        {
            try
            {
                var result = await TMDbServiceClient.Instance.People.GetAsync(personId, true, ct);
                return new Person(result);
            }
            catch (Exception ex)
            {

            }
            return new Person();
        }


        private static void UpdateQueryAndPage(string query)
        {
            if (CurrentQuery.Equals(query))
            {
                CurrentPage++;
            }
            else
            {
                CurrentQuery = query;
                CurrentPage = 1;
            }
        }

        private static List<SearchResult> ExtractDataFromResults(IEnumerable<System.Net.TMDb.Resource> resources)
        {
            List<SearchResult> result = new List<SearchResult>();
            try
            {
                resources.ToList().ForEach(r => result.Add(PopulateSearchResultFromResourceAsync(r)));

            }
            catch (Exception e)
            {

            }
            
            return result;
        }

        public static SearchResult PopulateSearchResultFromResourceAsync(System.Net.TMDb.Resource resource)
        {
            if (resource is System.Net.TMDb.Movie)
                return new SearchResult(resource as System.Net.TMDb.Movie);
            else if (resource is System.Net.TMDb.Show)
                return new SearchResult(resource as System.Net.TMDb.Show);
            else if (resource is System.Net.TMDb.Person)
                return new SearchResult(resource as System.Net.TMDb.Person);
            else
                return new SearchResult();
        }

        public static async Task<string> LoginAsync(string user, string pass, CancellationToken ct)
        {
            try
            {
                var token = await TMDbServiceClient.Instance.LoginAsync(user, pass, ct);
                if (string.IsNullOrWhiteSpace(token))
                    return null;

                return await TMDbServiceClient.Instance.GetSessionAsync(token, ct);
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public static async Task<OperationResult> LogoutAsync(string sessionId, CancellationToken ct)
        {
            try
            {
                var response = await TMDbServiceClient.ExtendedInstance.AccountLists.LogoutAsync(sessionId, ct);
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

        public static async Task<List<Movie>> GetMovieWatchlistAsync(int accountId, string sessionId, int page, CancellationToken ct)
        {
            try
            {
                var result = await TMDbServiceClient.ExtendedInstance.AccountLists.GetMovieWatchlistAsync(accountId, sessionId, LANGUAGE_CODE, page, ct);
                return result.Results.Select(movie => new Movie(movie)).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<Movie>();
        }

        public static async Task<List<Show>> GetShowWatchlistAsync(int accountId, string sessionId, int page, CancellationToken ct)
        {
            try
            {
                var result = await TMDbServiceClient.ExtendedInstance.AccountLists.GetShowWatchlistAsync(accountId, sessionId, LANGUAGE_CODE, page, ct);
                return result.Results.Select(show => new Show(show)).ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<Show>();
        }

        public static async Task<int?> GetAccountId(string sessionId, CancellationToken ct)
        {
            try
            {
                var id = await TMDbServiceClient.Instance.Settings.GetAccountAsync(sessionId, ct);
                return id.Id;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
