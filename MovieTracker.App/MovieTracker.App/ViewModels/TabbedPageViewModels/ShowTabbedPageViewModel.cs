using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Common;
using MovieTracker.App.ViewModels.DetailViewModels.Show;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class ShowTabbedPageViewModel : BaseViewModel, IDetailViewModel
    {
        public ShowDetailViewModel Tab1 { get; set; }

        public CastAndCrewViewModel Tab2 { get; set; }

        public WatchOnViewModel Tab3 { get; set; }

        public Task Initialization { get; private set; }

        public Command AddToListCommand => throw new NotImplementedException();

        public Command AddToWatchListCommand => throw new NotImplementedException();

        public Command AddToFavoritesCommand => throw new NotImplementedException();

        public Command RateCommand => throw new NotImplementedException();

        public ShowTabbedPageViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            Tab1 = new ShowDetailViewModel(id);
            Tab2 = new CastAndCrewViewModel(id);
            Tab3 = new WatchOnViewModel(id);
            await Task.WhenAll(Tab1.Initialization, Tab2.Initialization, Tab3.Initialization);
        }
    }
}
