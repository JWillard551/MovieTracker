using MovieTracker.App.Views.Popups;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace MovieTracker.App.Services
{
    public class UserPromptService : IUserPromptService
    {
        public async Task<bool> PromptUserYesNoAsync(string message, string confirmOption = "Confirm", string cancelOption = "Cancel")
        {
            var page = new ConfirmationPopUp(message, confirmOption, cancelOption);
            return await page.Show();
        }

        public async Task<string> PromptUserRatingAsync(int mediaID, MediaType mediaType)
        {
            var page = new RatingPopUp(mediaID, mediaType);
            var result = await page.Show();
            return result.ToString();
        }
    }
}
