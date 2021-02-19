using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public static class TMDbServiceClientHelper
    {
        public static async Task<IEnumerable<SearchResult>> SearchAsync(string query, int page, CancellationToken ct)
        {
            try
            {
                var result = await TMDbServiceClient.Instance.SearchAsync(query, "en-US", false, page, ct);
                return ExtractDataFromResult(result.Results);
            }
            catch (Exception e)
            {

            }
            return new List<SearchResult>();
        }

        private static List<SearchResult> ExtractDataFromResult(IEnumerable<System.Net.TMDb.Resource> resources)
        {
            List<SearchResult> result = new List<SearchResult>();
            resources.ToList().ForEach(r => result.Add(new SearchResult(r)));
            return result;
        }
    }
}
