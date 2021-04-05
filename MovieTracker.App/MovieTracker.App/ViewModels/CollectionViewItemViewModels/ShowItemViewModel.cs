using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class ShowItemViewModel : BaseViewModel
    {
        public Show Show { get; set; }

        public ShowItemViewModel(Show show)
        {
            Show = show;
        }
    }
}
