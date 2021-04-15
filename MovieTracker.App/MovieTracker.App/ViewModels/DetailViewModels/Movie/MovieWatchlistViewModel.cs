using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Common;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Utils;
using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieWatchlistViewModel : BaseDetailViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public Command<MovieItemViewModel> RateCommand { get; private set; }

        private ObservableRangeCollection<MovieItemViewModel> _watchlistMovies;
        public ObservableRangeCollection<MovieItemViewModel> WatchlistMovies
        {
            get => _watchlistMovies;
            set => SetProperty(ref _watchlistMovies, value);
        }

        public List<MovieItemViewModel> WatchlistMovieList { get; set; }

        public Command<MovieItemViewModel> RemoveFromWatchlistCommand { get; private set; }

        public Command<MovieItemViewModel> GoToDetailsCommand { get; set; }

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

        private async void OnRateSelected(MovieItemViewModel movie)
        {
            Rated = await HandleDetailButtonSelectedAsync(Rated, DetailButtonType.Ratings, MediaType.Movie, movie.Movie.Id);
        }

        private async void OnRemoveFromWatchlistSelected(MovieItemViewModel movie)
        {
            var result = await HandleRemoveItem(movie.Movie.Id, MediaType.Movie, DetailButtonType.Watchlist);
            if (result)
                WatchlistMovies.Remove(movie);
        }

        private async void GoToDetails(MovieItemViewModel viewModel)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={viewModel.Movie.Id}");
            IsBusy = false;
        }
    }
}
