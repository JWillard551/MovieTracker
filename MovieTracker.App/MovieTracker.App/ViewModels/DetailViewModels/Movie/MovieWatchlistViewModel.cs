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
    public class MovieWatchlistViewModel : AccountDetailViewModel, IViewModelInitialize
    {
        private ObservableRangeCollection<MovieItemViewModel> _watchlistMovies;
        public ObservableRangeCollection<MovieItemViewModel> WatchlistMovies
        {
            get => _watchlistMovies;
            set => SetProperty(ref _watchlistMovies, value);
        }

        public Task Initialization { get; private set; }

        #region Commands

        public Command<MovieItemViewModel> RateCommand { get; private set; }

        public Command<MovieItemViewModel> RemoveFromWatchlistCommand { get; private set; }

        public Command<MovieItemViewModel> GoToDetailsCommand { get; set; }

        #endregion

        public MovieWatchlistViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var movies = await TMDbService.AccountGetMovieWatchlistAsync();
            WatchlistMovies = new ObservableRangeCollection<MovieItemViewModel>(movies.Results.Select(movie => new MovieItemViewModel(movie)).ToList());
            RateCommand = new Command<MovieItemViewModel>(OnRateSelected);
            RemoveFromWatchlistCommand = new Command<MovieItemViewModel>(OnRemoveFromWatchlistSelected);
            GoToDetailsCommand = new Command<MovieItemViewModel>(GoToDetails);
        }

        private async void OnRateSelected(MovieItemViewModel mvm)
        {
            ItemRated = await HandleDetailButtonSelectedAsync(ItemRated, DetailButtonType.Ratings, MediaType.Movie, mvm.Movie.Id);
        }

        private async void OnRemoveFromWatchlistSelected(MovieItemViewModel mvm)
        {
            var result = await HandleRemoveItem(mvm.Movie.Id, MediaType.Movie, DetailButtonType.Watchlist);
            if (result)
                WatchlistMovies.Remove(mvm);
        }

        private async void GoToDetails(MovieItemViewModel mvm)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={mvm.Movie.Id}");
            IsBusy = false;
        }
    }
}
