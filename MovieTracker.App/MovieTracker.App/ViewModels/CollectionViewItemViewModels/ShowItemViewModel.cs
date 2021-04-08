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

    public class RatedShowItemViewModel : ShowItemViewModel
    {
        public double Rating { get; set; }

        public RatedShowItemViewModel(AccountSearchTv ratedShow) : base(ratedShow)
        {
            Rating = ratedShow.Rating;
        }
    }
}
