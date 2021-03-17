using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client
{
    public sealed class TMDbJWServiceClient
    {
        private const string API_KEY = "bbca921a0b03d5111e60a4010cb91102";
        public static TMDbJustWatchServiceClient Instance { get { return Client.Instance; } }

        private TMDbJWServiceClient() { }

        private class Client
        {
            //Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
            static Client() { }
            internal static readonly TMDbJustWatchServiceClient Instance = new TMDbJustWatchServiceClient(API_KEY);
        }

        ~TMDbJWServiceClient()
        {
            Client.Instance.Dispose();
        }
    }
}
