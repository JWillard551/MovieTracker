using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class WatchOnViewModel : BaseViewModel, IDetailViewModel
    {
        //TODO: This is hella confusing. Need to relook at this and better organize the class structure.
        //public ProviderModelDetails ProviderDetails { get; set; }
        //public List<ProviderLists> ProviderLists { get; set; }
        public Task Initialization { get; private set; }

        public WatchOnViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            //ProviderDetails = await TMDbServiceClientHelper.GetMovieProvidersById(id, new System.Threading.CancellationToken());
            //ProviderLists = new List<ProviderLists>();
            //try
            //{
            //    if (ProviderDetails?.Flatrate != null)
            //        ProviderLists.Add(new ProviderLists("Stream", ProviderDetails.Flatrate.OrderBy(provider => provider.DisplayPriority)));
            //    if (ProviderDetails?.Rent != null)
            //        ProviderLists.Add(new ProviderLists("Rent", ProviderDetails.Rent.OrderBy(provider => provider.DisplayPriority)));
            //    if (ProviderDetails?.Buy != null)
            //        ProviderLists.Add(new ProviderLists("Buy", ProviderDetails.Buy.OrderBy(provider => provider.DisplayPriority)));
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
}
