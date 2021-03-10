using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Threading;
using System.Threading.Tasks;
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
            MovieInfo = await TMDbServiceClientHelper.GetMovieDetailsById(id, new CancellationToken());
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(decimal.Round(MovieInfo.VoteAverage * 10, 0)),
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
