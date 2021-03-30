using MovieTracker.App.Services;
using MovieTracker.App.Views;
using MovieTracker.App.Views.Login;
using MovieTracker.Model.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class StartUpViewModel : BaseViewModel
    {
        public IAccountService LoginService => DependencyService.Get<IAccountService>();

        public StartUpViewModel() { }

        public async void Init()
        {
            var authenticated = await LoginService.HasActiveSessionID();
            if (authenticated)
                await Shell.Current.GoToAsync($"//Main/{nameof(MainPage)}");
            else
                await Shell.Current.GoToAsync($"//LoginPage");
        }
    }

    
}
