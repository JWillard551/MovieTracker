using TMDbLib.Client;

namespace MovieTracker.TMDbModel.Client
{
    public sealed class TMDbServiceClient
    {
        public static TMDbClient Instance { get { return Client.Instance; } }

        public static TMDbExtendedServiceClient ExtendedInstance { get { return Client.ExtendedInstance; } }

        private TMDbServiceClient() { }

        private class Client
        {
            internal const string API_KEY = "bbca921a0b03d5111e60a4010cb91102";
            //Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
            static Client() { }
            internal static readonly TMDbClient Instance = new TMDbClient(API_KEY);
            internal static readonly TMDbExtendedServiceClient ExtendedInstance = new TMDbExtendedServiceClient(API_KEY);
        }

        ~TMDbServiceClient()
        {
            Client.Instance.Dispose();
            Client.ExtendedInstance.Dispose();
        }
    }
}
