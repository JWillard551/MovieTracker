using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Utils;
using MvvmHelpers;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Show
{
    public class ShowFavoritesViewModel : AccountDetailViewModel, IViewModelInitialize
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
            var favorites = await TMDbService.AccountGetFavoriteTvAsync();
            FavoriteShows = new ObservableRangeCollection<ShowItemViewModel>(favorites.Results.Select(show => new ShowItemViewModel(show)).ToList());
            AddToWatchlistCommand = new Command<ShowItemViewModel>(OnAddToWatchlistCommand);
            RemoveFavoriteCommand = new Command<ShowItemViewModel>(OnRemoveFavoriteCommand);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
        }

        private async void OnAddToWatchlistCommand(ShowItemViewModel svm)
        {
            //svm.ItemRating = await HandleRatingEdit(svm.ItemRating, svm.Show.Id, MediaType.Tv);
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
    }
}
