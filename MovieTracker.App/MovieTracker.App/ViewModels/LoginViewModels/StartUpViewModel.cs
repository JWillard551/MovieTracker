using MovieTracker.App.Views;
using MovieTracker.App.Views.Login;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class StartUpViewModel
    {
        public StartUpViewModel() { }

        public async void Init()
        {
            await Task.Delay(4000);
            var authenticated = false;
            //var authenticated = await this.LoginService.Authenticate();
            if (authenticated)
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            else
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }

    
}
