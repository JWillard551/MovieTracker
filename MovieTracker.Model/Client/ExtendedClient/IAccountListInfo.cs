using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.Model.Client.ExtendedClient
{
    public interface IAccountListInfo
    {
        Task<Lists> GetAccountCreatedLists(int accountId, string sessionId, int page, CancellationToken cancellationToken = default(CancellationToken));
    }
}
