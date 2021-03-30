using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Model.Services
{
    public interface ILoginService
    {
        Task<bool> IsAlreadyAuthenticatedAsync();

        Task<bool> AuthenticateAsync(string user, string pass);

        Task<string> GetSessionId();

        Task SetSessionId(string sessionId);

        Task<DateTime?> GetSessionExpiration();

        Task<bool> IsSessionIdExpired();

        void Logout();
    }
}
