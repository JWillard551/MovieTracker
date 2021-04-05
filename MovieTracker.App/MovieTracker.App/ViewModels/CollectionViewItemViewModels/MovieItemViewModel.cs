using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public Movie Movie { get; set; }

        public MovieItemViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}
