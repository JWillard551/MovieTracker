using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
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

        public List<ShowItemViewModel> WatchlistShows { get; set; }

        public ShowWatchlistViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var shows = await TMDbService.AccountGetTvWatchlistAsync();
            WatchlistShows = shows.Results.Select(show => new ShowItemViewModel(show)).ToList();
        }
    }
}
