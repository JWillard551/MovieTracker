using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class LetsWatchItemViewModel : BaseViewModel
    {
        public LetsWatchItem Item { get; set; }

        public Command<LetsWatchItem> ItemTapped { get; set; }

        public LetsWatchItemViewModel(LetsWatchItem item)
        {
            Item = item;
            ItemTapped = new Command<LetsWatchItem>(OnItemTapped);
        }

        public async void OnItemTapped(LetsWatchItem item)
        {
            IsBusy = true;
            if (item.MediaType == MediaType.Movie)
                await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={item.Id}");
            else if (item.MediaType == MediaType.Tv)
                await Shell.Current.GoToAsync($"{nameof(ShowTabbedPage)}?ShowID={item.Id}");
            IsBusy = false;
        }
    }
}
