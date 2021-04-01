using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowWatchlistItemViewModel : BaseViewModel
    {
        public Show Show { get; set; }

        public ShowWatchlistItemViewModel(Show show)
        {
            Show = show;
        }
    }
}
