using MovieTracker.Model.Client.ExtendedClient;
using MovieTracker.Model.ModelEnums;
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

        public IProviderInfo Providers { get; private set;}

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
}
