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
    public class ShowRatingsViewModel : PagedLoadingViewModel<SearchContainer<AccountSearchTv>>, IViewModelInitialize
    {
        private ObservableRangeCollection<ShowItemViewModel> _ratedShows;
        public ObservableRangeCollection<ShowItemViewModel> RatedShows
        {
            get => _ratedShows;
            set => SetProperty(ref _ratedShows, value);
        }
        public Task Initialization { get; private set; }

        #region Commands

        public Command<ShowItemViewModel> EditRateCommand { get; private set; }

        public Command<ShowItemViewModel> RemoveRatingCommand { get; private set; }

        public Command<ShowItemViewModel> GoToDetailsCommand { get; private set; }

        #endregion

        public ShowRatingsViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var rated = await TMDbService.AccountGetRatedTvShowsAsync(CurrentPage);
            RatedShows = new ObservableRangeCollection<ShowItemViewModel>(rated.Results.Select(tv => new RatedShowItemViewModel(tv)).ToList());
            EditRateCommand = new Command<ShowItemViewModel>(OnEditRateCommand);
            RemoveRatingCommand = new Command<ShowItemViewModel>(OnRemoveRatingCommand);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
        }

        private async void OnEditRateCommand(ShowItemViewModel svm)
        {
            svm.ItemRating = await HandleRatingEdit(svm.ItemRating, svm.Show.Id, MediaType.Tv);
        }

        private async void OnRemoveRatingCommand(ShowItemViewModel svm)
        {
            var result = await HandleRemoveItem(svm.Show.Id, MediaType.Tv, DetailButtonType.Ratings);
            if (result)
                RatedShows.Remove(svm);
        }

        private async void GoToDetails(ShowItemViewModel svm)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ShowTabbedPage)}?ShowID={svm.Show.Id}");
            IsBusy = false;
        }

        public override async Task<SearchContainer<AccountSearchTv>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.AccountGetRatedTvShowsAsync(++CurrentPage);
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                return new SearchContainer<AccountSearchTv>() { Results = new List<AccountSearchTv>() };
            }
        }

        public override void EvaluateResultsAndUpdateObservableCollection(SearchContainer<AccountSearchTv> results)
        {
            if (results.Page > results.TotalPages && results.Results.Count == 0)
            {
                ItemLoadingComplete();
                return;
            }

            RatedShows.AddRange(results.Results.Select(show => new RatedShowItemViewModel(show)).ToList());
        }
    }
}
