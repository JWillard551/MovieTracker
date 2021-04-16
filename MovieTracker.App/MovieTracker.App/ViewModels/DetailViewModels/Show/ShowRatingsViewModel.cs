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
    public class ShowRatingsViewModel : AccountDetailViewModel, IViewModelInitialize
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
            var rated = await TMDbService.AccountGetRatedTvShowsAsync();
            RatedShows = new ObservableRangeCollection<ShowItemViewModel>(rated.Results.Select(tv => new RatedShowItemViewModel(tv)).ToList());
            EditRateCommand = new Command<ShowItemViewModel>(OnEditRateCommand);
            RemoveRatingCommand = new Command<ShowItemViewModel>(OnRemoveRatingCommand);
            GoToDetailsCommand = new Command<ShowItemViewModel>(GoToDetails);
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
    }
}
