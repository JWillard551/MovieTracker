using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowFavoritesViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<ShowItemViewModel> FavoriteShows { get; set; }

        public ShowFavoritesViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var favorites = await TMDbService.AccountGetFavoriteTvAsync();
            FavoriteShows = favorites.Results.Select(show => new ShowItemViewModel(show)).ToList();
        }
    }
}
