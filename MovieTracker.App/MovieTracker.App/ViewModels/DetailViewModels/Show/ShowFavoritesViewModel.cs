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

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowFavoritesViewModel : PagedLoadingViewModel<SearchContainer<SearchTv>>, IViewModelInitialize
    {
        private ObservableRangeCollection<ShowItemViewModel> _favoriteShows;
        public ObservableRangeCollection<ShowItemViewModel> FavoriteShows
        {
            get => _favoriteShows;
            set => SetProperty(ref _favoriteShows, value);
        }
        public Task Initialization { get; private set; }

        #region Commands

        public Command<ShowItemViewModel> AddToWatchlistCommand { get; private set; }

        public Command<ShowItemViewModel> RemoveFavoriteCommand { get; private set; }

        public Command<ShowItemViewModel> GoToDetailsCommand { get; private set; }

        #endregion
        public ShowFavoritesViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var favorites = await TMDbService.AccountGetFavoriteTvAsync(CurrentPage);
            FavoriteShows = new ObservableRangeCollection<ShowItemViewModel>(favorites.Results.Select(show => new ShowItemViewModel(show)).ToList());
            AddToWatchlistCommand = new Command<ShowItemViewModel>(OnAddToWatchlistCommand);
            RemoveFavoriteCommand = new Command<ShowItemViewModel>(OnRemoveFavoriteCommand);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
        }

        private async void OnAddToWatchlistCommand(ShowItemViewModel svm)
        {
            svm.ItemWatchlisted = await HandleDetailButtonSelectedAsync(svm.ItemWatchlisted, DetailButtonType.Watchlist, MediaType.Tv, svm.Show.Id);
        }

        private async void OnRemoveFavoriteCommand(ShowItemViewModel svm)
        {
            var result = await HandleRemoveItem(svm.Show.Id, MediaType.Tv, DetailButtonType.Favorites);
            if (result)
                FavoriteShows.Remove(svm);
        }

        private async void GoToDetails(ShowItemViewModel svm)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ShowTabbedPage)}?ShowID={svm.Show.Id}");
            IsBusy = false;
        }

        public override async Task<SearchContainer<SearchTv>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.AccountGetFavoriteTvAsync(++CurrentPage);
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                return new SearchContainer<SearchTv>() { Results = new List<SearchTv>() };
            }
        }

        public override void EvaluateResultsAndUpdateObservableCollection(SearchContainer<SearchTv> results)
        {
            if (results.Page > results.TotalPages && results.Results.Count == 0)
            {
                ItemLoadingComplete();
                return;
            }

            FavoriteShows.AddRange(results.Results.Select(show => new ShowItemViewModel(show)).ToList());
        }
    }
}
