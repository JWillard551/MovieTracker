using MovieTracker.App.ViewModels;
using MovieTracker.Model.Client;
using System.Threading;
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
