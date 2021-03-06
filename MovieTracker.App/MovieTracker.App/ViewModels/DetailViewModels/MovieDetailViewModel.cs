using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public sealed class MovieDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Movie MovieInfo { get; set; }
        public Task Initialization { get; private set; }

        public MovieDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            MovieInfo = await TMDbServiceClientHelper.GetMovieDetailsById(id, new CancellationToken());
        }
    }
}
