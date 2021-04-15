using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieRatingsViewModel : BaseDetailViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<RatedMovieItemViewModel> RatedMovies { get; set; }

        public MovieRatingsViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var rated = await TMDbService.AccountGetRatedMoviesAsync();
            RatedMovies = rated.Results.Select(movie => new RatedMovieItemViewModel(movie)).ToList();
        }
    }
}
