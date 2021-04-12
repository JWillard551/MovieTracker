using MovieTracker.TMDbModel.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.TvShows;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }
        public TvShow ShowInfo { get; set; }

        public string Rating { get; set; } = "NR";

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

        public ShowDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            //Handle initialization for the movie info.
            ShowInfo = await TMDbService.GetTVShowAsync(id, TvShowMethods.ContentRatings);

            Rating = GetRating();
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(ShowInfo.VoteAverage * 10)
            };
            PlayTrailerCommand = new Command(OnPlayTrailerSelected);
            AddToWatchListCommand = new Command(OnAddToWatchlistSelected);
            AddToFavoritesCommand = new Command(OnAddToFavoritesSelected);
            RateCommand = new Command(OnRateSelected);
        }

        private string GetRating()
        {
            var rating = ShowInfo.ContentRatings.Results.FirstOrDefault(contentRating => contentRating.Iso_3166_1 == "US")?.Rating;
            if (string.IsNullOrWhiteSpace(rating))
                return "??";
            return rating;
        }


        private async void OnPlayTrailerSelected()
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
