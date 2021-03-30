using MovieTracker.App.Services;
using MovieTracker.Model.ModelObjects;
using MovieTracker.Model.Services;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.LoginViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command<Credentials> LoginCommand { get; }

        public Credentials Credentials { get; set; } = new Credentials();

        public IAccountService LoginService => DependencyService.Get<IAccountService>();
        public IMessage ToastService => DependencyService.Get<IMessage>();

        public LoginViewModel()
        {
            LoginCommand = new Command<Credentials>(OnLoginClicked);
        }

        private async void OnLoginClicked(Credentials credentials)
        {
            IsBusy = true;
            var authenticated = await LoginService.LoginAccountAsync(credentials);
            IsBusy = false;
            if (authenticated)
                await Shell.Current.GoToAsync("//Main/MainPage");
            else
                ToastService.LongAlertMessage("Login failed. Ensure your username and password are correct.");
        }
    }
}
