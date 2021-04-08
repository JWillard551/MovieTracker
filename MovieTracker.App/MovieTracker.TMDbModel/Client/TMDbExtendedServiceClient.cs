using MovieTracker.TMDbModel.Services;
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

namespace MovieTracker.TMDbModel.Client
{
    public sealed class TMDbExtendedServiceClient : IDisposable
    {
        private readonly string apiKey;
        private readonly HttpClient client;
        private bool disposed = false;

        private static readonly JsonSerializerSettings jsonSettings;

        public IAccountService AccountService { get; private set; }

        public TMDbExtendedServiceClient(string apiKey)
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

            this.AccountService = new AccountServiceContext(this);
        }

        static TMDbExtendedServiceClient()
        {
            TMDbExtendedServiceClient.jsonSettings = new JsonSerializerSettings
            {
                Error = new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>((s, e) => OnSerializationError(e))
            };
        }

        private Task<T> GetAsync<T>(string cmd, IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(CreateRequestUri(cmd, parameters), HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ContinueWith(t => DeserializeAsync<T>(t.Result))
            .Unwrap();
        }

        private Task<T> DeleteAsync<T>(string cmd, IDictionary<string, object> parameters, IDictionary<string, object> content, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, CreateRequestUri(cmd, parameters)) { Content = Serialize(content) };
            return this.client.SendAsync(httpRequest, cancellationToken).ContinueWith(t => DeserializeAsync<T>(t.Result)).Unwrap();
        }

        private Task<T> PostAsync<T>(string cmd, IDictionary<string, object> parameters, IDictionary<string, object> content, CancellationToken cancellationToken)
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

        internal sealed class AccountServiceContext : IAccountService
        {
            private TMDbExtendedServiceClient _client;

            internal AccountServiceContext(TMDbExtendedServiceClient client)
            {
                _client = client;
            }

            public Task<HttpResponseMessage> LogoutAsync(string sessionId, CancellationToken cancellationToken = default)
            {
                var sessionContent = new Dictionary<string, object>();
                sessionContent.Add("session_id", sessionId);
                return _client.DeleteAsync<HttpResponseMessage>($"authentication/session", null, sessionContent, cancellationToken);
            }
        }
    }
}
