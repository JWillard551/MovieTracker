using MovieTracker.Model.Client;
using MovieTracker.Model.ModelEnums;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class WatchOnViewModel : BaseViewModel, IDetailViewModel
    {
        //TODO: This is hella confusing. Need to relook at this and better organize the class structure.
        public ProviderModelDetails ProviderDetails { get; set; }
        public List<ProviderLists> ProviderLists { get; set; }
        public Task Initialization { get; private set; }

        public WatchOnViewModel(int id, MediaType mediaType)
        {
            Initialization = InitializeAsync(id, mediaType);
        }

        public async Task InitializeAsync(int id, MediaType mediaType)
        {
            ProviderDetails = await TMDbServiceClientHelper.GetMovieProvidersById(id, new System.Threading.CancellationToken());
            ProviderLists = new List<ProviderLists>();
            try
            {
                ProviderLists.Add(new ProviderLists("Stream", ProviderDetails.Flatrate.OrderBy(provider => provider.DisplayPriority)));
                ProviderLists.Add(new ProviderLists("Rent", ProviderDetails.Rent.OrderBy(provider => provider.DisplayPriority)));
                ProviderLists.Add(new ProviderLists("Buy", ProviderDetails.Buy.OrderBy(provider => provider.DisplayPriority)));
            }
            catch (Exception ex)
            {
            }
        }
    }
}
