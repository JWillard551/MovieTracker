using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.TMDbModel.Utils;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public sealed class MovieDetailViewModel : AccountDetailViewModel, IViewModelInitialize
    {
        public Task Initialization { get; private set; }
        public TMDbLib.Objects.Movies.Movie MovieInfo { get; private set; }
        public string MovieRating { get; private set; }
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

        #region Commands

        public Command PlayTrailerCommand { get; private set; }

        public Command AddToWatchListCommand { get; private set; }

        public Command AddToFavoritesCommand { get; private set; }

        public Command RateCommand { get; private set; }

        #endregion

        public MovieDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            MovieInfo = await TMDbService.GetMovieAsync(id, MovieMethods.Releases | MovieMethods.AccountStates);
            await InitializeAccountInfo(id, MediaType.Movie);
            MovieRating = GetRating();
            InitializeRadialGaugeViewModel(MovieInfo?.VoteAverage ?? 0);

            //Command Initialization
            PlayTrailerCommand = new Command(OnPlayTrailerSelected);
            AddToWatchListCommand = new Command(OnAddToWatchlistSelected);
            AddToFavoritesCommand = new Command(OnAddToFavoritesSelected);
            RateCommand = new Command(OnRateSelected);
        }

        private string GetRating()
        {
            var rating = MovieInfo?.Releases?.Countries?.FirstOrDefault(country => country.Iso_3166_1 == "US")?.Certification;
            if (string.IsNullOrWhiteSpace(rating))
                return "NR";
            return rating;
        }

        private async void OnPlayTrailerSelected()
        {
            await Task.Delay(1000);
            return;
        }

        private async void OnAddToWatchlistSelected()
        {
            ItemWatchlisted = await HandleDetailButtonSelectedAsync(ItemWatchlisted, DetailButtonType.Watchlist, MediaType.Movie, MovieInfo.Id);
        }

        private async void OnAddToFavoritesSelected()
        {
            ItemFavorited = await HandleDetailButtonSelectedAsync(ItemFavorited, DetailButtonType.Favorites, MediaType.Movie, MovieInfo.Id);
        }

        private async void OnRateSelected()
        {
            ItemRated = await HandleDetailButtonSelectedAsync(ItemRated, DetailButtonType.Ratings, MediaType.Movie, MovieInfo.Id);
        }
    }
}
