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
            Placeholder = "Find a movie or show";
            PlaceholderColor = Color.LightGray;
            CancelButtonColor = Color.LightGray;
            TextColor = Color.Black;

            _svm = svm;
        }

        protected override async void OnQueryConfirmed()
        {
            base.OnQueryConfirmed();
            await _svm.OnSearchAsync(Query);
        }
    }
}
