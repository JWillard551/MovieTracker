using MovieTracker.App.ViewModels;
using Xamarin.Forms;

namespace MovieTracker.App.Handlers
{
    public class SearchBarSearchHandler : SearchHandler
    {
        SearchViewModel _svm;

        public SearchBarSearchHandler(SearchViewModel svm)
        {
            SearchBoxVisibility = SearchBoxVisibility.Expanded;
            IsSearchEnabled = true;
            ShowsResults = false;
            Placeholder = "Search for a movie, show, or person!";
            PlaceholderColor = Color.Gray;
            CancelButtonColor = Color.Gray;
            TextColor = Color.White;

            _svm = svm;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
        }
    }
}
