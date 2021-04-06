using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public sealed class MovieDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Movie MovieInfo { get; set; }

        public string Rating { get; set; } = "NR";
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
            MovieInfo = await TMDbService.GetMovieAsync(id, MovieMethods.Releases);
            Rating = GetRating();
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

        private string GetRating()
        {
            var rating = MovieInfo.Releases.Countries.FirstOrDefault(country => country.Iso_3166_1 == "US")?.Certification;
            if (string.IsNullOrWhiteSpace(rating))
                return "??";
            return rating;
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
