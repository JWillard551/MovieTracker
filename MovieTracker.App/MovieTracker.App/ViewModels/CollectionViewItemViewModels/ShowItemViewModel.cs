using System;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowItemViewModel : BaseViewModel
    {
        public SearchTv Show { get; set; }

        public RadialGaugeViewModel RadialGaugeViewModel { get; set; }

        public ShowItemViewModel(SearchTv show)
        {
            Show = show;
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(Show.VoteAverage * 10)
            };
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
