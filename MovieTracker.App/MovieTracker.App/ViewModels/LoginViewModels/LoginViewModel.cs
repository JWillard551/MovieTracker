using MovieTracker.App.Services;
using MovieTracker.TMDbModel.ModelObjects;
using MovieTracker.TMDbModel.Services;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command<Credentials> LoginCommand { get; }

        public Credentials Credentials { get; set; } = new Credentials();

        public ITMDbService TMDbService => DependencyService.Get<ITMDbService>();
        public IMessage ToastService => DependencyService.Get<IMessage>();

        public LoginViewModel()
        {
            LoginCommand = new Command<Credentials>(OnLoginClicked);
        }

        private async void OnLoginClicked(Credentials credentials)
        {
            IsBusy = true;
            var authenticated = await TMDbService.Authenticate(credentials);
            IsBusy = false;
            if (authenticated)
                await Shell.Current.GoToAsync("//Main/MainPage");
            else
                ToastService.LongAlertMessage("Login failed. Ensure your username and password are correct.");
        }
    }
}
