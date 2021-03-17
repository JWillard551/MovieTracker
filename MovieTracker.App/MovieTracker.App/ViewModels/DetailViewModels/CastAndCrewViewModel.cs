using MovieTracker.Model.Client;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class CastAndCrewViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public List<MediaCredits> MediaCast { get; set; }

        public CastAndCrewViewModel(int id, MediaType mediaType)
        {
            Initialization = InitializeAsync(id, mediaType);
        }

        private async Task InitializeAsync(int id, MediaType mediaType)
        {
            //Handle initialization for the movie info.
            MediaCast = await TMDbServiceClientHelper.GetMovieCreditsById(id, new CancellationToken());
        }
    }
}
