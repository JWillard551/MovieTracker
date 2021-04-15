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

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowWatchlistViewModel : BaseDetailViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        private ObservableRangeCollection<ShowItemViewModel> _watchlistShows;
        public ObservableRangeCollection<ShowItemViewModel> WatchlistShows
        {
            get => _watchlistShows;
            set => SetProperty(ref _watchlistShows, value);
        }

        public List<ShowItemViewModel> WatchlistShowList { get; set; }

        public Command<ShowItemViewModel> RateCommand { get; private set; }

        public Command<ShowItemViewModel> RemoveFromWatchlistCommand { get; private set; }

        public Command<ShowItemViewModel> GoToDetailsCommand { get; set; }

        public ShowWatchlistViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var shows = await TMDbService.AccountGetTvWatchlistAsync();
            WatchlistShows = new ObservableRangeCollection<ShowItemViewModel>(shows.Results.Select(show => new ShowItemViewModel(show)).ToList());
            RateCommand = new Command<ShowItemViewModel>(OnRateSelected);
            RemoveFromWatchlistCommand = new Command<ShowItemViewModel>(OnRemoveFromWatchlistSelected);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
        }

        private async void OnRateSelected(ShowItemViewModel show)
        {
            Rated = await HandleDetailButtonSelectedAsync(Rated, DetailButtonType.Ratings, MediaType.Tv, show.Show.Id);
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
    }
}
