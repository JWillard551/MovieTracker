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
}
