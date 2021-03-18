using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.ModelEnums;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MovieTabbedPageViewModel : BaseViewModel, IDetailViewModel
    {
        public MovieDetailViewModel Tab1 { get; set; }

        public CastAndCrewViewModel Tab2 { get; set; }

        public WatchOnViewModel Tab3 { get; set; }

        public Task Initialization { get; private set; }

        public Command AddToListCommand => throw new NotImplementedException();

        public Command AddToWatchListCommand => throw new NotImplementedException();

        public Command AddToFavoritesCommand => throw new NotImplementedException();

        public Command RateCommand => throw new NotImplementedException();

        public MovieTabbedPageViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            IsBusy = true;
            //Handle initialization for the movie info.
            Tab1 = new MovieDetailViewModel(id);
            Tab2 = new CastAndCrewViewModel(id, MediaType.Movie);
            Tab3 = new WatchOnViewModel(id, MediaType.Movie);
            await Task.WhenAll(Tab1.Initialization, Tab2.Initialization, Tab3.Initialization);
            IsBusy = false;
        }
    }
}
