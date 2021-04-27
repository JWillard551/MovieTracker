using MovieTracker.App.Services;
using MovieTracker.App.Views.ModalViews;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using MovieTracker.TMDbModel.Services;
using System;
using Xamarin.Forms;

namespace MovieTracker.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ITMDbService TMDbService => DependencyService.Get<ITMDbService>();
        public IMessage ToastService => DependencyService.Get<IMessage>();

        public IUserPromptService UserPromptService => DependencyService.Get<IUserPromptService>();

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SearchOptionsPage), typeof(SearchOptionsPage));
            Routing.RegisterRoute(nameof(MovieTabbedPage), typeof(MovieTabbedPage));
            Routing.RegisterRoute(nameof(ShowTabbedPage), typeof(ShowTabbedPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            OperationResult response = new OperationResult(false);
            bool logout = await UserPromptService.PromptUserYesNoAsync("Logout?", "Yes", "No");
            if (logout)
            {
                response = await TMDbService.LogoutSessionAsync();
                if (response.Success)
                    await Shell.Current.GoToAsync("//LoginPage");
                else
                    ToastService.LongAlertMessage(response.Message);
            }
        }
    }
}
