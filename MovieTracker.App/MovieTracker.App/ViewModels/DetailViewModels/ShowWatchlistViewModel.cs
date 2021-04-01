using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using MovieTracker.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class ShowWatchlistViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<ShowWatchlistItemViewModel> WatchlistShows { get; set; }

        public ShowWatchlistViewModel(int id, int accountId, string sessionId)
        {
            Initialization = InitializeAsync(id, accountId, sessionId);
        }

        private async Task InitializeAsync(int id, int accountId, string sessionId)
        {
            var shows = await TMDbServiceClientHelper.GetShowWatchlistAsync(accountId, sessionId, 1, new CancellationToken());
            WatchlistShows = shows.Select(show => new ShowWatchlistItemViewModel(show)).ToList();
        }
    }
}
