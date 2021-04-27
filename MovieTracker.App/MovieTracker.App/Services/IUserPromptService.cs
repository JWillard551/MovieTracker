using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace MovieTracker.App.Services
{
    public interface IUserPromptService
    {
        Task<bool> PromptUserYesNoAsync(string message, string confirmOption = "Confirm", string cancelOption = "Cancel");

        Task<string> PromptUserRatingAsync(int mediaID, MediaType mediaType);
    }
}
