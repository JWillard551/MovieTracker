using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Utils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieWatchlistViewModel : PagedLoadingViewModel<SearchContainer<SearchMovie>>, IViewModelInitialize
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
            var movies = await TMDbService.AccountGetMovieWatchlistAsync(CurrentPage);
            ResetItemThreshold(5);
            WatchlistMovies = new ObservableRangeCollection<MovieItemViewModel>(movies.Results.Select(movie => new MovieItemViewModel(movie)).ToList());
            RateCommand = new Command<MovieItemViewModel>(OnRateSelected);
            RemoveFromWatchlistCommand = new Command<MovieItemViewModel>(OnRemoveFromWatchlistSelected);
            GoToDetailsCommand = new Command<MovieItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
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

        public override async Task<SearchContainer<SearchMovie>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.AccountGetMovieWatchlistAsync(++CurrentPage);
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                return new SearchContainer<SearchMovie>() { Results = new List<SearchMovie>() };
            }
        }

        public override void EvaluateResultsAndUpdateObservableCollection(SearchContainer<SearchMovie> results)
        {
            if (results.Page > results.TotalPages && results.Results.Count == 0)
            {
                ItemLoadingComplete();
                return;
            }

            WatchlistMovies.AddRange(results.Results.Select(movie => new MovieItemViewModel(movie)).ToList());
        }
    }
}
