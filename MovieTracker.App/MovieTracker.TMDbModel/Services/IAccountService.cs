using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.TMDbModel.Services
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> LogoutAsync(string sessionId, CancellationToken cancellationToken = default);
    }
}
