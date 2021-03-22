using MovieTracker.Model.Client.ExtendedClient;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

                return new ProviderModelDetails(providers.Results.US); //TODO: We can return other regions. For now we just want US providers.
            }
            catch (Exception ex)
            {

            }
            return null;
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
    }
}
