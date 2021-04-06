using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowItemViewModel : BaseViewModel
    {
        public SearchTv Show { get; set; }

        public ShowItemViewModel(SearchTv show)
        {
            Show = show;
        }
    }
}
