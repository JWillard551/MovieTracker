using MovieTracker.App.ViewModels.DetailViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MovieTabbedPageViewModel : BaseViewModel, IDetailViewModel
    {
        public MovieDetailViewModel Tab1 { get; set; }

        public MovieDetailViewModel Tab2 { get; set; }


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
            //Handle initialization for the movie info.
            Tab1 = new MovieDetailViewModel(id);
            Tab2 = new MovieDetailViewModel(id);
            await Task.WhenAll(Tab1.Initialization, Tab2.Initialization);
        }
    }
}
