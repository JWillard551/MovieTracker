using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using MovieTracker.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MyListsPageViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }
        public IAccountService LoginService { get; } = DependencyService.Get<IAccountService>();

        public Lists Lists { get; set; }
        public MyListsPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            var accountId = await LoginService.GetAccountIDAsync();
            var sessionId = await LoginService.GetSessionIDAsync();
            Lists = await TMDbServiceClientHelper.GetAccountCreatedLists(accountId, sessionId, 1, new CancellationToken());
        }
    }
}
