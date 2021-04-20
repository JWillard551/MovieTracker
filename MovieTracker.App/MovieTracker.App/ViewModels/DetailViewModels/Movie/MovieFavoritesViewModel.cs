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
    public class MovieFavoritesViewModel : PagedLoadingViewModel<SearchContainer<SearchMovie>>, IViewModelInitialize
    {
        private ObservableRangeCollection<MovieItemViewModel> _favoriteMovies;
        public ObservableRangeCollection<MovieItemViewModel> FavoriteMovies
        {
            get => _favoriteMovies;
            set => SetProperty(ref _favoriteMovies, value);
        }

        public Task Initialization { get; private set; }

        #region Commands

        public Command<MovieItemViewModel> AddToWatchlistCommand { get; private set; }

        public Command<MovieItemViewModel> RemoveFavoriteCommand { get; private set; }

        public Command<MovieItemViewModel> GoToDetailsCommand { get; private set; }

        #endregion

        public MovieFavoritesViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var favorites = await TMDbService.AccountGetFavoriteMoviesAsync(CurrentPage);
            FavoriteMovies = new ObservableRangeCollection<MovieItemViewModel>(favorites.Results.Select(movie => new MovieItemViewModel(movie)).ToList());
            AddToWatchlistCommand = new Command<MovieItemViewModel>(OnAddToWatchlistCommand);
            RemoveFavoriteCommand = new Command<MovieItemViewModel>(OnRemoveFavoriteCommand);
            GoToDetailsCommand = new Command<MovieItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
        }

        private async void OnAddToWatchlistCommand(MovieItemViewModel mvm)
        {
            //svm.ItemRating = await HandleRatingEdit(svm.ItemRating, svm.Show.Id, MediaType.Tv);
        }

        private async void OnRemoveFavoriteCommand(MovieItemViewModel mvm)
        {
            var result = await HandleRemoveItem(mvm.Movie.Id, MediaType.Movie, DetailButtonType.Favorites);
            if (result)
                FavoriteMovies.Remove(mvm);
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
                return await TMDbService.AccountGetFavoriteMoviesAsync(++CurrentPage);
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

            FavoriteMovies.AddRange(results.Results.Select(movie => new MovieItemViewModel(movie)).ToList());
        }
    }
}
