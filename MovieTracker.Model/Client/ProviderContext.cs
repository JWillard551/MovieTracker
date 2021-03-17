using MovieTracker.Model.ModelEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public sealed class ProviderContext : IProviderInfo
    {
        private TMDbJustWatchServiceClient _client;

        internal ProviderContext(TMDbJustWatchServiceClient client)
        {
            _client = client;
        }

        public Task<ProviderList> GetProvidersAsync(int id, MediaType mediaType, CancellationToken cancellationToken = default)
        {
            switch (mediaType)
            {
                case MediaType.Show:
                    return _client.GetAsync<ProviderList>($"tv/{id}/watch/providers?api_key={_client.apiKey}", new CancellationToken());
                case MediaType.Movie:
                default:
                    return _client.GetAsync<ProviderList>($"movie/{id}/watch/providers?api_key={_client.apiKey}", new CancellationToken());
            }
        }
    }
}
