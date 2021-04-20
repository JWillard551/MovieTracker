using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.BaseViewModels
{
    public abstract class PagedLoadingViewModel<T> : AccountDetailViewModel
    {
        public string CurrentQuery { get; set; } = string.Empty;

        public int CurrentPage { get; set; } = 1;

        private int _itemThreshold;
        public int ItemThreshold
        {
            get { return _itemThreshold; }
            private set { SetProperty(ref _itemThreshold, value); }
        }

        public Command LoadMoreItemsCommand { get; set; }

        public abstract Task<T> LoadMoreItemsAsync();

        public abstract void EvaluateResultsAndUpdateObservableCollection(T results);

        protected async void OnLoadMoreItems()
        {
            if (!ShouldLoadMore())
                return;

            IsBusy = true;
            var results = await LoadMoreItemsAsync();
            EvaluateResultsAndUpdateObservableCollection(results);
            IsBusy = false;
        }

        protected void UpdateCurrentSearchQueryAndPage(string query)
        {
            if (!CurrentQuery.Equals(query))
            {
                CurrentPage = 1;
                CurrentQuery = query;
            }
        }

        protected virtual bool ShouldLoadMore()
        {
            if (IsBusy)
                return false;

            return true;
        }

        protected void ResetItemThreshold(int baseThreshold)
        {
            ItemThreshold = baseThreshold;
        }

        protected void ItemLoadingComplete()
        {
            ItemThreshold = -1;
        }
    }
}
