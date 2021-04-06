using System;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public sealed class MovieDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Movie MovieInfo { get; set; }
        public Task Initialization { get; private set; }

        public Command AddToListCommand { get; private set; }

        public Command AddToWatchListCommand { get; private set; }

        public Command AddToFavoritesCommand { get; private set; }

        public Command RateCommand { get; private set; }


        private RadialGaugeViewModel _ratingVM;
        public RadialGaugeViewModel RadialGaugeViewModel
        {
            get => _ratingVM;
            set => SetProperty(ref _ratingVM, value);
        }

        public MovieDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            MovieInfo = await TMDbService.GetMovieAsync(id);
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(MovieInfo.VoteAverage * 10)
            };
            AddToListCommand = new Command(OnAddToListSelected);
            AddToWatchListCommand = new Command(OnAddToWatchlistSelected);
            AddToFavoritesCommand = new Command(OnAddToFavoritesSelected);
            RateCommand = new Command(OnRateSelected);
        }

        private async void OnAddToListSelected()
        {

        }

        private async void OnAddToWatchlistSelected()
        {

        }

        private async void OnAddToFavoritesSelected()
        {

        }

        private async void OnRateSelected()
        {

        }
    }
}
