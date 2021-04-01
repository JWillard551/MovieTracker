using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class MovieWatchlistItemViewModel : BaseViewModel
    {
        public Movie Movie { get; set; }

        public MovieWatchlistItemViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}
