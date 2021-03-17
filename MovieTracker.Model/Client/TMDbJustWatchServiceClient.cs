using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.TMDb;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public sealed class TMDbJustWatchServiceClient : IDisposable
    {
        public readonly string apiKey;
        private readonly HttpClient client;
        private bool disposed = false;

        private static readonly JsonSerializerSettings jsonSettings;

        public IProviderInfo Providers { get; private set;}

        public TMDbJustWatchServiceClient(string apiKey) 
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
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            this.client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

            this.Providers = new ProviderContext(this);
        }

        static TMDbJustWatchServiceClient()
        {
            TMDbJustWatchServiceClient.jsonSettings = new JsonSerializerSettings
            {
                Error = new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>((s, e) => OnSerializationError(e))
            };
        }

        public Task<T> GetAsync<T>(string cmd, CancellationToken cancellationToken) //IDictionary<string, object> parameters, 
        {
            return this.client.GetAsync(cmd, HttpCompletionOption.ResponseHeadersRead, cancellationToken) //CreateRequestUri(cmd, parameters)
            .ContinueWith(t => DeserializeAsync<T>(t.Result))
            .Unwrap();
        }

        //TODO: May not need this. Test stuff first.
        private string CreateRequestUri(string cmd, IDictionary<string, object> parameters)
        {
            var sb = new System.Text.StringBuilder($"{cmd}?api_key={apiKey}&");

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
                if (disposing) // dispose aggregated resources
                    this.client.Dispose();
                this.disposed = true; // disposing has been done
            }
        }
    }
}
