using MovieTracker.TMDbModel.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class CastAndCrewViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public Movie MediaCast { get; set; }

        public CastAndCrewViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            MediaCast = await TMDbService.GetMovieAsync(id, MovieMethods.Credits);
        }
    }
}
