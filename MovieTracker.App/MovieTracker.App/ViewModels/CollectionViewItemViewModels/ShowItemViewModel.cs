using MovieTracker.App.ViewModels.BaseViewModels;
using System;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowItemViewModel : AccountDetailViewModel
    {
        public SearchTv Show { get; set; }

        public ShowItemViewModel(SearchTv show)
        {
            Show = show;
            InitializeRadialGaugeViewModel(show.VoteAverage);
        }
    }

    public class RatedShowItemViewModel : ShowItemViewModel
    {
        public RatedShowItemViewModel(AccountSearchTv ratedShow) : base(ratedShow)
        {
            ItemRating = Math.Round(ratedShow.Rating).ToString();
        }
    }
}
