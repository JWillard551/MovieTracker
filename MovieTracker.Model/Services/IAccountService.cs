using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Threading.Tasks;

namespace MovieTracker.Model.Services
{
    public interface IAccountService
    {
        Task<string> GetSessionIDAsync();
        Task<int> GetAccountIDAsync();
        //Task<bool> ActiveSessionHasExpired();
        Task SetSessionID(string id);
        Task SetAccountID(string id);
        Task<bool> LoginAccountAsync(Credentials credentials);
        Task<bool> HasActiveSessionID();
        Task<OperationResult> LogoutAccountAsync();
        OperationResult ClearFromStorage();
    }
}
