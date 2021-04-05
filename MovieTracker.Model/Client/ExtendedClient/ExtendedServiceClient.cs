using MovieTracker.Model.Client.ExtendedClient;
using MovieTracker.Model.ExtendedModelObjects;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public sealed class ExtendedServiceClient : IDisposable
    {
        private readonly string apiKey;
        private readonly HttpClient client;
        private bool disposed = false;

        private static readonly JsonSerializerSettings jsonSettings;

        public IProviderInfo Providers { get; private set; }
        public IAccountListInfo AccountLists { get; private set; }

        public ExtendedServiceClient(string apiKey) 
        {
            this.apiKey = apiKey;
            this.client = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                PreAuthenticate = true,
                UseDefaultCredentials = true,
                UseCookies = false,
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

            this.Providers = new ProviderContext(this);
            this.AccountLists = new AccountListsContext(this);
        }

        static ExtendedServiceClient()
        {
            ExtendedServiceClient.jsonSettings = new JsonSerializerSettings
            {
                Error = new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>((s, e) => OnSerializationError(e))
            };
        }

        public Task<T> GetAsync<T>(string cmd, IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(CreateRequestUri(cmd, parameters), HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ContinueWith(t => DeserializeAsync<T>(t.Result))
            .Unwrap();
        }

        public Task<T> DeleteAsync<T>(string cmd, IDictionary<string, object> parameters, IDictionary<string, object> content, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, CreateRequestUri(cmd, parameters)) { Content = Serialize(content) };

            return this.client.SendAsync(httpRequest, cancellationToken).ContinueWith(t => DeserializeAsync<T>(t.Result)).Unwrap();
        }

        public Task<T> PostAsync<T>(string cmd, IDictionary<string, object> parameters, IDictionary<string, object> content, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, CreateRequestUri(cmd, parameters)) { Content = Serialize(content) };

            return this.client.SendAsync(httpRequest, cancellationToken).ContinueWith(t => DeserializeAsync<T>(t.Result)).Unwrap();
        }

        private static HttpContent Serialize(object data) 
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"); 
        }

        private string CreateRequestUri(string cmd, IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder($"{cmd}?api_key={apiKey}&");

            if (parameters != null)
            {
                sb.Append(String.Join("&", parameters.Where(s => s.Value != null)
                    .Select(s => String.Concat(s.Key, "=", ConvertParameterValue(s.Value)))));
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine(sb);
#endif
            return sb.ToString();
        }

        private static string ConvertParameterValue(object value)
        {
            Type t = value.GetType();
            t = Nullable.GetUnderlyingType(t) ?? t;

            if (t == typeof(DateTime)) return ((DateTime)value).ToString("yyyy-MM-dd");
            else if (t == typeof(Decimal)) return ((Decimal)value).ToString(CultureInfo.InvariantCulture);
            else return Uri.EscapeDataString(value.ToString());
        }

        private static Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync()
                .ContinueWith<T>(t =>
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(t.Result);
#endif
                    return JsonConvert.DeserializeObject<T>(t.Result, jsonSettings);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private static void OnSerializationError(Newtonsoft.Json.Serialization.ErrorEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this.client.Dispose();
                this.disposed = true;
            }
        }
    }

    internal sealed class ProviderContext : IProviderInfo
    {
        private ExtendedServiceClient _client;

        internal ProviderContext(ExtendedServiceClient client)
        {
            _client = client;
        }

        public Task<ProviderList> GetProvidersAsync(int id, MediaType mediaType, CancellationToken cancellationToken = default)
        {
            switch (mediaType)
            {
                case MediaType.Show:
                    return _client.GetAsync<ProviderList>($"tv/{id}/watch/providers", null, new CancellationToken());
                case MediaType.Movie:
                default:
                    return _client.GetAsync<ProviderList>($"movie/{id}/watch/providers", null, new CancellationToken());
            }
        }
    }

    internal sealed class AccountListsContext : IAccountListInfo
    {
        private ExtendedServiceClient _client;

        internal AccountListsContext(ExtendedServiceClient client)
        {
            _client = client;
        }

        public Task<Lists> GetAccountCreatedLists(int accountId, string sessionId, int page, CancellationToken ct)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", "en-US");
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<Lists>($"account/{accountId}/lists", parameters, ct);
        }

        public Task<System.Net.TMDb.Movies> GetMovieWatchlistAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<System.Net.TMDb.Movies>($"account/{accountId}/watchlist/movies", parameters, cancellationToken);
        }

        public Task<System.Net.TMDb.Shows> GetShowWatchlistAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<System.Net.TMDb.Shows>($"account/{accountId}/watchlist/tv", parameters, cancellationToken);
        }

        public Task<System.Net.TMDb.Movies> GetMovieFavoritesAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<System.Net.TMDb.Movies>($"account/{accountId}/favorite/movies", parameters, cancellationToken);
        }

        public Task<System.Net.TMDb.Shows> GetShowFavoritesAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<System.Net.TMDb.Shows>($"account/{accountId}/favorite/tv", parameters, cancellationToken);
        }

        public Task<RatedMovies> GetRatedMoviesAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<RatedMovies>($"account/{accountId}/rated/movies", parameters, cancellationToken);
        }

        public Task<RatedShows> GetRatedShowsAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("language", language);
            parameters.Add("page", page);
            parameters.Add("session_id", sessionId);
            return _client.GetAsync<RatedShows>($"account/{accountId}/rated/tv", parameters, cancellationToken);
        }

        public Task<HttpResponseMessage> LogoutAsync(string sessionId, CancellationToken cancellationToken = default)
        {
            var sessionContent = new Dictionary<string, object>();
            sessionContent.Add("session_id", sessionId);
            return _client.DeleteAsync<HttpResponseMessage>($"authentication/session", null, sessionContent, cancellationToken);
        }

        public Task<HttpResponseMessage> SetMovieWatchlistAsync(int accountId, string sessionID, int movieId, bool addFlag, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("session_id", sessionID);

            var content = new Dictionary<string, object>();
            content.Add("media_type", "movie");
            content.Add("media_id", movieId);
            content.Add("watchlist", addFlag);

            return _client.PostAsync<HttpResponseMessage>($"account/{accountId}/watchlist", parameters, content, cancellationToken);
        }

        public Task<HttpResponseMessage> SetShowWatchlistAsync(int accountId, string sessionID, int showId, bool addFlag, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("session_id", sessionID);

            var content = new Dictionary<string, object>();
            content.Add("media_type", "tv");
            content.Add("media_id", showId);
            content.Add("watchlist", addFlag);

            return _client.PostAsync<HttpResponseMessage>($"account/{accountId}/watchlist", parameters, content, cancellationToken);
        }
    }

    public class OperationResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;

        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
