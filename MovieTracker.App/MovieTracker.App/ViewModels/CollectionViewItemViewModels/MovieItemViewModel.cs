using MovieTracker.App.ViewModels.BaseViewModels;
using System;
using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class MovieItemViewModel : AccountDetailViewModel
    {
        public SearchMovie Movie { get; set; }

        public MovieItemViewModel(SearchMovie movie)
        {
            Movie = movie;
            InitializeRadialGaugeViewModel(movie.VoteAverage);
        }
    }

    public class RatedMovieItemViewModel : MovieItemViewModel
    {
        public RatedMovieItemViewModel(SearchMovieWithRating ratedMovie) : base(ratedMovie)
        {
            ItemRating = Math.Round(ratedMovie.Rating).ToString();
        }
    }
}
