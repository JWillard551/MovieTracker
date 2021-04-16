using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Utils;
using MvvmHelpers;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieRatingsViewModel : AccountDetailViewModel, IViewModelInitialize
    {
        private ObservableRangeCollection<MovieItemViewModel> _ratedMovies;
        public ObservableRangeCollection<MovieItemViewModel> RatedMovies
        {
            get => _ratedMovies;
            set => SetProperty(ref _ratedMovies, value);
        }
        public Task Initialization { get; private set; }

        #region Commands

        public Command<RatedMovieItemViewModel> EditRateCommand { get; private set; }

        public Command<MovieItemViewModel> RemoveRatingCommand { get; private set; }

        public Command<MovieItemViewModel> GoToDetailsCommand { get; private set; }

        #endregion

        public MovieRatingsViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var rated = await TMDbService.AccountGetRatedMoviesAsync();
            RatedMovies = new ObservableRangeCollection<MovieItemViewModel>(rated.Results.Select(movie => new RatedMovieItemViewModel(movie)).ToList());
            EditRateCommand = new Command<RatedMovieItemViewModel>(OnEditRateCommand);
            RemoveRatingCommand = new Command<MovieItemViewModel>(OnRemoveRatingCommand);
            GoToDetailsCommand = new Command<MovieItemViewModel>(GoToDetails);
        }

        private async void OnEditRateCommand(RatedMovieItemViewModel mvm)
        {
            mvm.ItemRating = await HandleRatingEdit(mvm.ItemRating, mvm.Movie.Id, MediaType.Movie);
        }

        private async void OnRemoveRatingCommand(MovieItemViewModel mvm)
        {
            var result = await HandleRemoveItem(mvm.Movie.Id, MediaType.Movie, DetailButtonType.Ratings);
            if (result)
                RatedMovies.Remove(mvm);
        }

        private async void GoToDetails(MovieItemViewModel mvm)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={mvm.Movie.Id}");
            IsBusy = false;
        }
    }
}
