using MovieTracker.TMDbModel.ModelObjects;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels
{
    public class PopularItemViewModel : BaseViewModel
    {
        public PopularItem PopularItem { get; set; }

        public PopularItemViewModel(SearchMovie item)
        {
            PopularItem = new PopularItem(item);
        }

        public PopularItemViewModel(SearchTv item)
        {
            PopularItem = new PopularItem(item);
        }
    }
}
