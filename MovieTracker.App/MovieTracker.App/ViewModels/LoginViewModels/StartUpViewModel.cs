using MovieTracker.App.Services;
using MovieTracker.App.Views;
using MovieTracker.App.Views.Login;
using MovieTracker.TMDbModel.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class StartUpViewModel : BaseViewModel
    {
        public ITMDbService TMDbService => DependencyService.Get<ITMDbService>();

        public StartUpViewModel() { }

        public async void Init()
        {
            var authenticated = await TMDbService.HasActiveSessionID();
            if (authenticated)
                await Shell.Current.GoToAsync($"//Main/{nameof(MainPage)}");
            else
                await Shell.Current.GoToAsync($"//LoginPage");
        }
    }

    
}
