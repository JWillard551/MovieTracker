using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowRatingsViewModel : BaseDetailViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<RatedShowItemViewModel> RatedShows { get; set; }

        public ShowRatingsViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var rated = await TMDbService.AccountGetRatedTvShowsAsync();
            RatedShows = rated.Results.Select(tv => new RatedShowItemViewModel(tv)).ToList();
        }
    }
}
