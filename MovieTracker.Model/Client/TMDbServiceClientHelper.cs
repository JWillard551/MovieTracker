using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.TMDb;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public static class TMDbServiceClientHelper
    {
        private static string CurrentQuery { get; set; } = string.Empty;
        public static int CurrentPage { get; private set; }
        public static int TotalPages { get; private set; }

        public static int PageSize = 20;

        public static async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken ct)
        {
            try
            {
                UpdateQueryAndPage(query);
                var result = await TMDbServiceClient.Instance.SearchAsync(CurrentQuery, "en-US", false, CurrentPage, ct);
                TotalPages = result.PageCount;
                var results = ExtractDataFromResults(result.Results);
                return results;
            }
            catch (Exception e)
            {

            }
            return new List<SearchResult>();
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
