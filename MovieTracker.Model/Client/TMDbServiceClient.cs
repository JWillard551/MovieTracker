using System.Net.TMDb;

namespace MovieTracker.Model.Client
{
    public sealed class TMDbServiceClient
    {
        private const string API_KEY = "bbca921a0b03d5111e60a4010cb91102";
        public static ServiceClient Instance { get { return Client.Instance; } }
        
        private TMDbServiceClient() { }

        private class Client
        {
            //Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
            static Client() { }
            internal static readonly ServiceClient Instance = new ServiceClient(API_KEY);
        }

        ~TMDbServiceClient()
        {
            Client.Instance.Dispose();
        }
    }
}
