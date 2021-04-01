using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class WatchlistTabbedViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public MovieWatchlistViewModel MovieViewModel { get; set; }

        public ShowWatchlistViewModel ShowViewModel { get; set; }

        public IAccountService AccountService { get; } = DependencyService.Get<IAccountService>();

        public WatchlistTabbedViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            var sessionId = await AccountService.GetSessionIDAsync();
            var accountId = await AccountService.GetAccountIDAsync();
            MovieViewModel = new MovieWatchlistViewModel(id, accountId, sessionId);
            ShowViewModel = new ShowWatchlistViewModel(id, accountId, sessionId);
            await Task.WhenAll(MovieViewModel.Initialization, ShowViewModel.Initialization);
        }
    }
}
