using MovieTracker.TMDbModel.AdditionalModelObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace MovieTracker.App.ViewModels.DetailViewModels.Common
{
    public class WatchOnViewModel : BaseViewModel, IDetailViewModel
    {
        public SingleResultContainer<Dictionary<string, WatchProviders>> Providers { get; set; }
        private WatchProviders RegionWatchProvider { get; set; }
        public List<ProviderLists> ProviderLists { get; set; }
        public Task Initialization { get; private set; }

        public WatchOnViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            Providers = await TMDbService.GetMovieWatchProvidersAsync(id, new System.Threading.CancellationToken());

            ProviderLists = new List<ProviderLists>();
            if (Providers?.Results?.ContainsKey("US") ?? false)
            {
                RegionWatchProvider = Providers.Results["US"];
                if (RegionWatchProvider?.FlatRate?.Any() ?? false)
                    ProviderLists.Add(new ProviderLists("Stream", RegionWatchProvider.FlatRate.OrderBy(x => x.DisplayPriority).Select(item => new ProviderItem(item)).ToList()));
                if (RegionWatchProvider?.Ads?.Any() ?? false)
                    ProviderLists.Add(new ProviderLists("Ads", RegionWatchProvider.Ads.OrderBy(x => x.DisplayPriority).Select(item => new ProviderItem(item)).ToList()));
                if (RegionWatchProvider?.Rent?.Any() ?? false)
                    ProviderLists.Add(new ProviderLists("Rent", RegionWatchProvider.Rent.OrderBy(x => x.DisplayPriority).Select(item => new ProviderItem(item)).ToList()));
                if (RegionWatchProvider?.Buy?.Any() ?? false)
                    ProviderLists.Add(new ProviderLists("Buy", RegionWatchProvider.Buy.OrderBy(x => x.DisplayPriority).Select(item => new ProviderItem(item)).ToList()));
                
            }
        }
    }
}
