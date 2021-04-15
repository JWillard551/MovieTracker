using MovieTracker.App.ViewModels.DetailViewModels.Common;
using MovieTracker.TMDbModel.Utils;
using System;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public SearchMovie Movie { get; set; }

        public RadialGaugeViewModel RadialGaugeViewModel { get; set; }

        public MovieItemViewModel(SearchMovie movie)
        {
            Movie = movie;
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(Movie.VoteAverage * 10)
            };
        }
    }

    public class RatedMovieItemViewModel : MovieItemViewModel
    {
        public double Rating { get; set; }

        public RatedMovieItemViewModel(SearchMovieWithRating ratedMovie) : base(ratedMovie)
        {
            Rating = ratedMovie.Rating;
        }
    }
}
