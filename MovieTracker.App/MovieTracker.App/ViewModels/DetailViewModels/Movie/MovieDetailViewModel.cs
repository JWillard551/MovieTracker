using MovieTracker.App.ViewModels.DetailViewModels.Common;
using MovieTracker.TMDbModel.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public sealed class MovieDetailViewModel : BaseDetailViewModel, IDetailViewModel
    {
        public TMDbLib.Objects.Movies.Movie MovieInfo { get; set; }

        public string Rating { get; set; } = "NR";

        public UriImageSource UriImage 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(MovieInfo?.PosterPath))
                    return new UriImageSource() { Uri = ModelUtils.GetImageUri(MovieInfo.PosterPath) };
                else
                    return null;
            }
        }

        public Task Initialization { get; private set; }

        public Command PlayTrailerCommand { get; private set; }

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
            AccountState = await TMDbService.GetMovieAccountStateAsync(id);
            Favorited = AccountState?.Favorite ?? false;
            Watchlisted = AccountState?.Watchlist ?? false;
            Rated = System.Convert.ToDouble(AccountState.Rating) > 0;
            Rating = GetRating();
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(MovieInfo.VoteAverage * 10)
            };
            PlayTrailerCommand = new Command(OnPlayTrailerSelected);
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

        private async void OnPlayTrailerSelected()
        {

        }

        private async void OnAddToWatchlistSelected()
        {
            Watchlisted = await HandleDetailButtonSelectedAsync(Watchlisted, DetailButtonType.Watchlist, MediaType.Movie, MovieInfo.Id);
        }

        private async void OnAddToFavoritesSelected()
        {
            Favorited = await HandleDetailButtonSelectedAsync(Favorited, DetailButtonType.Favorites, MediaType.Movie, MovieInfo.Id);
        }

        private async void OnRateSelected()
        {
            Rated = await HandleDetailButtonSelectedAsync(Rated, DetailButtonType.Ratings, MediaType.Movie, MovieInfo.Id);
        }
    }
}
