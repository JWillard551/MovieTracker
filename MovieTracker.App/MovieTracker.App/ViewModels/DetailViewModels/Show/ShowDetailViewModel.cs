using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.TMDbModel.Utils;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.TvShows;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowDetailViewModel : AccountDetailViewModel, IViewModelInitialize
    {
        public Task Initialization { get; private set; }
        public TvShow ShowInfo { get; private set; }
        public string ShowRating { get; private set; }
        public UriImageSource UriImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ShowInfo?.PosterPath))
                    return new UriImageSource() { Uri = ModelUtils.GetImageUri(ShowInfo.PosterPath) };
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

        public ShowDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        private async Task InitializeAsync(int id)
        {
            //Handle initialization for the show info.
            ShowInfo = await TMDbService.GetTVShowAsync(id, TvShowMethods.ContentRatings);
            await InitializeAccountInfo(id, MediaType.Tv);
            ShowRating = GetRating();
            InitializeRadialGaugeViewModel(ShowInfo?.VoteAverage ?? 0);

            //Command Initialization
            PlayTrailerCommand = new Command(OnPlayTrailerSelected);
            AddToWatchListCommand = new Command(OnAddToWatchlistSelected);
            AddToFavoritesCommand = new Command(OnAddToFavoritesSelected);
            RateCommand = new Command(OnRateSelected);
        }

        private string GetRating()
        {
            var rating = ShowInfo?.ContentRatings?.Results?.FirstOrDefault(contentRating => contentRating.Iso_3166_1 == "US")?.Rating;
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
            ItemWatchlisted = await HandleDetailButtonSelectedAsync(ItemWatchlisted, DetailButtonType.Watchlist, MediaType.Tv, ShowInfo.Id);
        }

        private async void OnAddToFavoritesSelected()
        {
            ItemFavorited = await HandleDetailButtonSelectedAsync(ItemFavorited, DetailButtonType.Favorites, MediaType.Tv, ShowInfo.Id);
        }

        private async void OnRateSelected()
        {
            ItemRated = await HandleDetailButtonSelectedAsync(ItemRated, DetailButtonType.Ratings, MediaType.Tv, ShowInfo.Id);
        }
    }
}
