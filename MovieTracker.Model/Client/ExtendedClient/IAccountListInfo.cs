using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client.ExtendedClient
{
    public interface IAccountListInfo
    {
        Task<Lists> GetAccountCreatedLists(int accountId, string sessionId, int page, CancellationToken cancellationToken = default(CancellationToken));

        Task<System.Net.TMDb.Movies> GetMovieWatchlistAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default(CancellationToken));

        Task<System.Net.TMDb.Shows> GetShowWatchlistAsync(int accountId, string sessionId, string language, int page, CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> LogoutAsync(string sessionId, CancellationToken cancellationToken = default);
    }
}
