using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class PopularItemViewModel : BaseViewModel
    {
        public PopularItem PopularItem { get; set; }

        public Command<PopularItem> ItemTapped { get; set; }

        public PopularItemViewModel(SearchMovie item)
        {
            PopularItem = new PopularItem(item);
            ItemTapped = new Command<PopularItem>(OnItemTapped);
        }

        public PopularItemViewModel(SearchTv item)
        {
            PopularItem = new PopularItem(item);
            ItemTapped = new Command<PopularItem>(OnItemTapped);
        }

        public async void OnItemTapped(PopularItem item)
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
