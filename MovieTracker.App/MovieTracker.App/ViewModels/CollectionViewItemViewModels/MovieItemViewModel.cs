using TMDbLib.Objects.Search;

namespace MovieTracker.App.ViewModels.CollectionViewItemViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public SearchMovie Movie { get; set; }

        public MovieItemViewModel(SearchMovie movie)
        {
            Movie = movie;
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
