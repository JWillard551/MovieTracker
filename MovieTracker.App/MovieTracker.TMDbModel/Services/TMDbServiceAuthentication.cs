using MovieTracker.TMDbModel.Client;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Authentication;
using Xamarin.Essentials;

namespace MovieTracker.TMDbModel.Services
{
    public partial class TMDbService : ITMDbService
    {
        /// <summary>
        /// Logs in a TMDb user in via the API and retrieves/sets a session ID for future API calls.
        /// </summary>
        /// <param name="creds">TMDb user credentials.</param>
        /// <returns>Returns an OperationResult indicating success and a message if failure occurs.</returns>
        public async Task<OperationResult> LoginAndSetSessionAsync(Credentials creds)
        {
            try
            {
                //If an existing session is found, load/set that session ID and continue.
                bool hasExistingSession = await HasExistingSessionIDAsync();
                if (hasExistingSession)
                    return new OperationResult(true);

                UserSession userSession = await TMDbServiceClient.Instance.AuthenticationGetUserSessionAsync(creds.Username, creds.Password);
                if (userSession != null)
                {
                    await SaveSessionIDAsync(userSession.SessionId);
                    await SetActiveSessionIDAsync(userSession.SessionId);
                }
                else
                    return new OperationResult(false, "Could not authenticate with TMDb.");

                return new OperationResult(true);
            }
            catch
            {
                Console.WriteLine("AccountService.LoginAccountAsync(Credentials creds) - Exception occurred.");
                return new OperationResult(false, "Exception occurred while authenticating with TMDb.");
            }
        }

        /// <summary>
        /// Logs out a TMDb user and deletes the session ID in use.
        /// </summary>
        /// <param name="sessionId">Session ID to be deleted.</param>
        /// <param name="cancellationToken">Cancellation token to use for network/api calls.</param>
        /// <returns>Returns an OperationResult indicating success and a message if failure occurs.</returns>
        public async Task<OperationResult> LogoutSessionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var sessionID = await LoadSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionID))
                    return new OperationResult(false, "Could not load Session ID to logout.");

                var response = await TMDbServiceClient.ExtendedInstance.AccountService.LogoutAsync(sessionID, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    return new OperationResult(false, message);
                }

                ClearSessionID();
                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message);
            }
        }

        /// <summary>
        /// Determines whether a Session ID exists in SecureStorage. If a session ID exists, it is set as the active session ID.
        /// </summary>
        /// <returns>Returns true if an existing session ID was loaded/set. Returns false if no session ID was found.</returns>
        public async Task<bool> HasExistingSessionIDAsync()
        {
            try
            {
                string sessionID = await LoadSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionID))
                    return false;

                await SetActiveSessionIDAsync(sessionID);
                return true;
            }
            catch
            {
                Console.WriteLine("AccountService.HasActiveSessionID() - Exception occurred.");
                return false;
            }
        }

        /// <summary>
        /// Loads a Session ID from SecureStorage.
        /// </summary>
        /// <returns>Returns a session ID string if sucessful, otherwise null.</returns>
        private async Task<string> LoadSessionIDAsync()
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

        /// <summary>
        /// Saves a Session ID to SecureStorage.
        /// </summary>
        /// <param name="id">Session ID to be saved.</param>
        /// <returns></returns>
        private async Task SaveSessionIDAsync(string id)
        {
            try
            {
                var existingID = await LoadSessionIDAsync();
                if (!string.IsNullOrWhiteSpace(existingID))
                {
                    try
                    {
                        //Attempt to remove old session ID.
                        var result = await LogoutSessionAsync();
                    }
                    catch
                    {
                        //Nothing we can do at this point. Just let the session Id be replaced.
                    }
                }

                await SecureStorage.SetAsync(SESSION_ID_TOKEN, id);
            }
            catch
            {
                Console.WriteLine("AccountService.SetSessionID(string sessionId) - Exception occurred.");
            }
        }

        /// <summary>
        /// Sets the specified session ID as the active session for usage on the TMDbClient API.
        /// </summary>
        /// <param name="id">Session ID to be set.</param>
        /// <returns></returns>
        private async Task SetActiveSessionIDAsync(string id)
        {
            await TMDbServiceClient.Instance.SetSessionInformationAsync(id, TMDbLib.Objects.Authentication.SessionType.UserSession);
        }

        /// <summary>
        /// Clears a session ID from SecureStorage.
        /// </summary>
        /// <returns>Returns an OperationResult indicating success and a message if failure occurs.</returns>
        private OperationResult ClearSessionID()
        {
            if (!SecureStorage.Remove(SESSION_ID_TOKEN))
                return new OperationResult(false, "Session ID could not be removed from secure storage");

            return new OperationResult(true, string.Empty);
        }
    }
}
