using MovieTracker.Model.Client;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class WatchOnViewModel : BaseViewModel, IDetailViewModel
    {
        public ProviderDetails ProviderDetails { get; set; }
        public Task Initialization { get; private set; }

        public WatchOnViewModel(int id, MediaType mediaType)
        {
            Initialization = InitializeAsync(id, mediaType);
        }

        public async Task InitializeAsync(int id, MediaType mediaType)
        {
            ProviderDetails = await TMDbServiceClientHelper.GetMovieProvidersById(id, new System.Threading.CancellationToken());
        }
    }
}
