﻿using MovieTracker.Model.Client;
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
        private readonly string SESSION_ID_EXPIRATION = "session_id_expiration";

        public async Task<string> GetSessionIDAsync()
        {
            return await SecureStorage.GetAsync(SESSION_ID_TOKEN);
        }

        public async Task<int> GetAccountIDAsync()
        {
            var id = await SecureStorage.GetAsync(ACCOUNT_ID);
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActiveSessionHasExpired()
        {
            var sessionDateTime = await SecureStorage.GetAsync(SESSION_ID_EXPIRATION);
            if (sessionDateTime == null)
                return true;

            //If conversion fails, treat it as a session has expired (relog).
            DateTime timestamp = Convert.ToDateTime(sessionDateTime);
            if (timestamp == null)
                return true;

            //If the timestamp is "greater" than DateTime.Now, it is expired and a new session ID should be requested.
            return timestamp < DateTime.Now;
        }

        public async Task SetSessionID(string sessionId)
        {
            //Set expiration - 6 hours from now.
            await SecureStorage.SetAsync(SESSION_ID_TOKEN, sessionId);
            await SecureStorage.SetAsync(SESSION_ID_EXPIRATION, DateTime.Now.AddHours(6).ToString());
        }

        public async Task SetAccountID(string id)
        {
            await SecureStorage.SetAsync(ACCOUNT_ID, id);
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
            catch (Exception ex)
            {
            }
            return false;
        }

        public async Task<bool> HasActiveSessionID()
        {
            try
            {
                string sessionID = await GetSessionIDAsync();
                if (string.IsNullOrWhiteSpace(sessionID))
                    return false;

                var idIsExpired = await ActiveSessionHasExpired();
                return !idIsExpired;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public void Logout()
        {
            SecureStorage.Remove(ACCOUNT_ID);
            SecureStorage.Remove(SESSION_ID_TOKEN);
            SecureStorage.Remove(SESSION_ID_EXPIRATION);
        }
    }
}
