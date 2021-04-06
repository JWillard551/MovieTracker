using MovieTracker.App.Services;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command<Credentials> LoginCommand { get; }

        public Credentials Credentials { get; set; } = new Credentials();

        public IMessage ToastService => DependencyService.Get<IMessage>();

        public LoginViewModel()
        {
            LoginCommand = new Command<Credentials>(OnLoginClicked);
        }

        private async void OnLoginClicked(Credentials credentials)
        {
            IsBusy = true;
            var authenticated = await TMDbService.LoginAndSetSessionAsync(credentials);
            IsBusy = false;
            if (authenticated.Success)
                await Shell.Current.GoToAsync("//Main/MainPage");
            else
                ToastService.LongAlertMessage(authenticated.Message);
        }
    }
}

