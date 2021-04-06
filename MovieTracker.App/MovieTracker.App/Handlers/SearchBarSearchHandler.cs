using MovieTracker.App.ViewModels;
using Xamarin.Forms;

namespace MovieTracker.App.Handlers
{
    public class SearchBarSearchHandler : SearchHandler
    {
        SearchViewModel _svm;

        public SearchBarSearchHandler(SearchViewModel svm)
        {
            //TODO: Placeholder text size might require a custom renderer. Or we could just change the placeholder text to be shorter...
            SearchBoxVisibility = SearchBoxVisibility.Expanded;
            IsSearchEnabled = true;
            ShowsResults = false;
            Placeholder = "Find a movie, show, or person";
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

        protected override void OnClearPlaceholderClicked()
        {
            base.OnClearPlaceholderClicked();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
        }
    }
}
