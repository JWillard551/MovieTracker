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
    public class ShowWatchlistViewModel : PagedLoadingViewModel<SearchContainer<SearchTv>>, IViewModelInitialize
    {
        private ObservableRangeCollection<ShowItemViewModel> _watchlistShows;
        public ObservableRangeCollection<ShowItemViewModel> WatchlistShows
        {
            get => _watchlistShows;
            set => SetProperty(ref _watchlistShows, value);
        }

        public Task Initialization { get; private set; }

        #region Commands

        public Command<ShowItemViewModel> RateCommand { get; private set; }

        public Command<ShowItemViewModel> RemoveFromWatchlistCommand { get; private set; }

        public Command<ShowItemViewModel> GoToDetailsCommand { get; set; }

        #endregion

        public ShowWatchlistViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var shows = await TMDbService.AccountGetTvWatchlistAsync(CurrentPage);
            WatchlistShows = new ObservableRangeCollection<ShowItemViewModel>(shows.Results.Select(show => new ShowItemViewModel(show)).ToList());
            RateCommand = new Command<ShowItemViewModel>(OnRateSelected);
            RemoveFromWatchlistCommand = new Command<ShowItemViewModel>(OnRemoveFromWatchlistSelected);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
        }

        private async void OnRateSelected(ShowItemViewModel show)
        {
            ItemRated = await HandleDetailButtonSelectedAsync(ItemRated, DetailButtonType.Ratings, MediaType.Tv, show.Show.Id);
        }

        private async void OnRemoveFromWatchlistSelected(ShowItemViewModel show)
        {
            var result = await HandleRemoveItem(show.Show.Id, MediaType.Tv, DetailButtonType.Watchlist);
            if (result)
                WatchlistShows.Remove(show);
        }

        private async void GoToDetails(ShowItemViewModel viewModel)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ShowTabbedPage)}?ShowID={viewModel.Show.Id}");
            IsBusy = false;
        }

        public override async Task<SearchContainer<SearchTv>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.AccountGetTvWatchlistAsync(++CurrentPage);
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

            WatchlistShows.AddRange(results.Results.Select(show => new ShowItemViewModel(show)).ToList());
        }
    }
}
