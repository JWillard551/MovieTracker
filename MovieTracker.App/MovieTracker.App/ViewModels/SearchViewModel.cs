using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.ModalViewModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class SearchViewModel : PagedLoadingViewModel<SearchContainer<SearchBase>>
    {
        public Command SearchOptionsCommand { get; set; }

        private ObservableRangeCollection<SearchResultViewModel> itemsToDisplay;
        public ObservableRangeCollection<SearchResultViewModel> ItemsToDisplay { get => itemsToDisplay; set => SetProperty(ref itemsToDisplay, value); }

        public List<SearchResultViewModel> Items { get; set; }

        private bool SearchOptionsVisible { get; set; } = true;

        private SearchOptionsViewModel SearchOptionsViewModel { get; set; }

        public SearchViewModel()
        {
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
            SearchOptionsCommand = new Command(SearchOptionsPressed);
            InitializeSearchData();
        }

        public void InitializeSearchData()
        {
            Items = new List<SearchResultViewModel>();
            ItemsToDisplay = new ObservableRangeCollection<SearchResultViewModel>(Items);
        }

        private async void SearchOptionsPressed()
        {
        }

        public async Task OnSearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;
            IsBusy = true;
            ResetItemThreshold(5);
            UpdateCurrentSearchQueryAndPage(query);
            Items = new List<SearchResultViewModel>();
            var results = await TMDbService.SearchAsync(CurrentQuery, CurrentPage, new CancellationToken());

            foreach (var res in results.Results)
                Items.Add(new SearchResultViewModel(res));

            ItemsToDisplay = new ObservableRangeCollection<SearchResultViewModel>(Items);
            IsBusy = false;
        }

        public override async Task<SearchContainer<SearchBase>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.SearchAsync(CurrentQuery, ++CurrentPage, new CancellationToken());
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                return new SearchContainer<SearchBase>() { Results = new List<SearchBase>() };
            }
        }

        public override void EvaluateResultsAndUpdateObservableCollection(SearchContainer<SearchBase> results)
        {
            if (results.Page > results.TotalPages && results.Results.Count == 0)
            {
                ItemLoadingComplete();
                return;
            }

            ItemsToDisplay.AddRange(results.Results.Select(searchResult => new SearchResultViewModel(searchResult)));
        }
    }
}
