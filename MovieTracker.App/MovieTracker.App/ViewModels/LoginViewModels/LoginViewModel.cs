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

        public ILoginService LoginService => DependencyService.Get<ILoginService>();
        public IMessage ToastService => DependencyService.Get<IMessage>();

        public LoginViewModel()
        {
            LoginCommand = new Command<Credentials>(OnLoginClicked);
        }

        private async void OnLoginClicked(Credentials credentials)
        {
            var authenticated = await LoginService.AuthenticateAsync(credentials.Username, credentials.Password);
            if (authenticated)
                await Shell.Current.GoToAsync("//Main/MainPage");
            else
                ToastService.LongAlertMessage("Login failed. Ensure your username and password are correct.");
        }
    }
}
