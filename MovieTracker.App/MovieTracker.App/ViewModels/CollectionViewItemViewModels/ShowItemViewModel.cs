using MovieTracker.App.ViewModels.DetailViewModels.Common;
using System;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowItemViewModel : BaseDetailViewModel
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
        public RatedShowItemViewModel(AccountSearchTv ratedShow) : base(ratedShow)
        {
            Rating = Math.Round(ratedShow.Rating).ToString();
        }
    }
}
