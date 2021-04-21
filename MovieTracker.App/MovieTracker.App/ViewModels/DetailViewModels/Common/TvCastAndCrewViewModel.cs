using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using MovieTracker.TMDbModel.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDbLib.Objects.TvShows;

namespace MovieTracker.App.ViewModels.DetailViewModels.Common
{
    public class TvCastAndCrewViewModel : BaseViewModel, IViewModelInitialize
    {
        public Task Initialization { get; private set; }

        public TvShow Show { get; set; }

        public List<MediaCredits> MediaCast { get; set; } = new List<MediaCredits>();

        public TvCastAndCrewViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            Show = await TMDbService.GetTVShowAsync(id, TvShowMethods.Credits);
            MediaCast = ModelUtils.GetTvShowMediaCredits(Show.Credits);
        }
    }
}
