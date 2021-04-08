using MovieTracker.TMDbModel.AdditionalModelObjects;
using MovieTracker.TMDbModel.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;

namespace MovieTracker.App.ViewModels.DetailViewModels.Common
{
    public class CastAndCrewViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public TMDbLib.Objects.Movies.Movie Movie { get; set; }

        public List<MediaCredits> MediaCast { get; set; } = new List<MediaCredits>();

        public CastAndCrewViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            Movie = await TMDbService.GetMovieAsync(id, MovieMethods.Credits);
            MediaCast = ModelUtils.GetMovieMediaCredits(Movie.Credits);
        }
    }
}
