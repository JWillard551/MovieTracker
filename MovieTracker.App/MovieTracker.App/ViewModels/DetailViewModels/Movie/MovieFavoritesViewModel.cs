using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieFavoritesViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<MovieItemViewModel> FavoriteMovies { get; set; }

        public MovieFavoritesViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var favorites = await TMDbService.AccountGetFavoriteMoviesAsync();
            FavoriteMovies = favorites.Results.Select(movie => new MovieItemViewModel(movie)).ToList();
        }
    }
}
