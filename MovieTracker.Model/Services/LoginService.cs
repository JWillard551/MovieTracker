using MovieTracker.Model.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieTracker.Model.Services
{
    public sealed class LoginService : ILoginService
    {
        private readonly string SESSION_ID_TOKEN = "session_id_token";
        private readonly string SESSION_ID_EXPIRATION = "session_id_expiration";

        public async Task<bool> AuthenticateAsync(string user, string pass)
        {
            try
            {
                if (await IsAlreadyAuthenticatedAsync())
                    return true;

                var sessionId = await TMDbServiceClientHelper.LoginAsync(user, pass, new CancellationToken());
                if (string.IsNullOrWhiteSpace(sessionId))
                    return false;
                await SetSessionId(sessionId);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> IsAlreadyAuthenticatedAsync()
        {
            try
            {
                var idIsExpired = await IsSessionIdExpired();
                return !idIsExpired;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public async Task<string> GetSessionId()
        {
            return await SecureStorage.GetAsync(SESSION_ID_TOKEN);
        }

        public async Task<DateTime?> GetSessionExpiration()
        {
            var sessionExpr = await SecureStorage.GetAsync(SESSION_ID_EXPIRATION);
            if (sessionExpr == null)
                return null;
            return Convert.ToDateTime(sessionExpr);
        }

        public async Task SetSessionId(string sessionId)
        {
            //Set early expiration - 6 hours from now.
            await SecureStorage.SetAsync(SESSION_ID_TOKEN, sessionId);
            await SecureStorage.SetAsync(SESSION_ID_EXPIRATION, DateTime.Now.AddHours(6).ToString());
        }

        public async Task<bool> IsSessionIdExpired()
        {
            var id = await GetSessionId();
            if (id == null)
                return true;
            var expr = await GetSessionExpiration();
            if (expr == null)
                return true;
            return expr < DateTime.Now;
        }

        public void Logout()
        {
            SecureStorage.Remove(SESSION_ID_TOKEN);
            SecureStorage.Remove(SESSION_ID_EXPIRATION);
        }
    }
}
