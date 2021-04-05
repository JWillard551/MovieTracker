using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieTracker.Model.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly string ACCOUNT_ID = "account_id";
        private readonly string SESSION_ID_TOKEN = "session_id_token";

        public async Task<string> GetSessionIDAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(SESSION_ID_TOKEN);
            }
            catch
            {
                Console.WriteLine("AccountService.GetSessionIDAsync() - Exception occurred.");
                return string.Empty;
            }
        }

        public async Task<int> GetAccountIDAsync()
        {
            try
            {
                string id = await SecureStorage.GetAsync(ACCOUNT_ID);
                return Convert.ToInt32(id);
            }
            catch
            {
                Console.WriteLine("AccountService.GetAccountIDAsync() - Exception occurred.");
                return 0;
            }
        }

        public async Task SetSessionID(string sessionId)
        {
            try
            {
                await SecureStorage.SetAsync(SESSION_ID_TOKEN, sessionId);
            }
            catch
            {
                Console.WriteLine("AccountService.SetSessionID(string sessionId) - Exception occurred.");
            }
        }

        public async Task SetAccountID(string id)
        {
            try
            {
                await SecureStorage.SetAsync(ACCOUNT_ID, id);
            }
            catch
            {
                Console.WriteLine("AccountService.SetAccountID(string id) - Exception occurred.");
            }
        }

        public async Task<bool> LoginAccountAsync(Credentials creds)
        {
            try
            {
                if (await HasActiveSessionID())
                    return true;

                var sessionID = await TMDbServiceClientHelper.LoginAsync(creds.Username, creds.Password, new CancellationToken());
                if (string.IsNullOrWhiteSpace(sessionID))
                    return false;

                var accountID = await TMDbServiceClientHelper.GetAccountId(sessionID, new CancellationToken());

                await SetSessionID(sessionID);
                await SetAccountID(accountID.ToString());
                return true;
            }
            catch
            {
                Console.WriteLine("AccountService.LoginAccountAsync(Credentials creds) - Exception occurred.");
                return false;
            }
        }

        public async Task<bool> HasActiveSessionID()
        {
            try
            {
                string sessionID = await GetSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionID))
                    return false;

                return true;
            }
            catch
            {
                Console.WriteLine("AccountService.HasActiveSessionID() - Exception occurred.");
                return false;
            }
        }

        public async Task<OperationResult> LogoutAccountAsync()
        {
            try
            {
                var sessionId = await GetSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionId))
                    return new OperationResult(false, "Session ID could not be retrieved for logout");

                return await TMDbServiceClientHelper.LogoutAsync(sessionId, new CancellationToken());
            }
            catch
            {
                Console.WriteLine("AccountService.Logout() - Failed to remove from SecureStorage.");
            }
            return new OperationResult(false, "Something went wrong");
        }

        public OperationResult ClearFromStorage()
        {
            if (!SecureStorage.Remove(ACCOUNT_ID))
                return new OperationResult(false, "Account ID could not be removed from secure storage");

            if (!SecureStorage.Remove(SESSION_ID_TOKEN))
                return new OperationResult(false, "Session ID could not be removed from secure storage");

            return new OperationResult(true, string.Empty);
        }
    }
}
