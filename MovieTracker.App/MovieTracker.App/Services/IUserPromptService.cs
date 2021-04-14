using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.Services
{
    public interface IUserPromptService
    {
        Task<bool> PromptUserYesNoAsync(string title, string message, string accept = "Yes", string cancel = "No");

        Task<string> PromptUserRatingAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = null);
    }

    public class UserPromptService : IUserPromptService
    {
        public async Task<bool> PromptUserYesNoAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> PromptUserRatingAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = null)
        {
            return await App.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard, initialValue);
        }
    }
}
