using MovieTracker.App.ViewModels.ModalViewModels;
using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public string CurrentQuery { get; set; } = string.Empty;
        public Command LoadMoreItemsCommand { get; set; }
        public Command SearchOptionsCommand { get; set; }

        private ObservableRangeCollection<SearchResultViewModel> itemsToDisplay;
        public ObservableRangeCollection<SearchResultViewModel> ItemsToDisplay { get => itemsToDisplay; set => SetProperty(ref itemsToDisplay, value); }

        public List<SearchResultViewModel> Items { get; set; }

        private bool SearchOptionsVisible { get; set; } = true;

        private SearchOptionsViewModel SearchOptionsViewModel { get; set; }

        public SearchViewModel()
        {
            LoadMoreItemsCommand = new Command(LoadMoreAsync);
            SearchOptionsCommand = new Command(SearchOptionsPressed);
            InitData();
        }

        public void InitData()
        {
            Items = new List<SearchResultViewModel>();
            ItemsToDisplay = new ObservableRangeCollection<SearchResultViewModel>(Items);
        }

        private string GetRatingDetail(int rating)
        {
            if (rating >= 90)
                return "Excellent!";
            else if (rating >= 75)
                return "Great!";
            else if (rating >= 50)
                return "Okay.";
            else if (rating >= 35)
                return "Meh.";
            else
                return "Bad.";
        }

        public async Task OnSearchAsync(string query)
        {
            IsBusy = true;
            UpdateCurrentSearchQuery(query);
            //This method should take the query from the SearchBarQueryHandler and pass it into the TMDB client helper class.
            //The helper class will handle the API call to fetch data. The returned results will need to be converted to one of my internal model objects (a search result object).
            Items = new List<SearchResultViewModel>();
            var results = await TMDbService.SearchAsync(CurrentQuery, new CancellationToken());
            foreach (var res in results.Results)
            {
                Items.Add(new SearchResultViewModel(res));
            }
            ItemsToDisplay = new ObservableRangeCollection<SearchResultViewModel>(Items);
            IsBusy = false;
        }

        private async void LoadMoreAsync()
        {
            if (IsBusy)
                return;

            if (TMDbService.CurrentPage > TMDbService.TotalPages)
                return;

            IsBusy = true;

            var results = await TMDbService.SearchAsync(CurrentQuery, new CancellationToken());
            ItemsToDisplay.AddRange(results.Results.Select(searchResult => new SearchResultViewModel(searchResult)));
            IsBusy = false;
        }

        private async void SearchOptionsPressed()
        {
            //SearchOptionsVisible = !SearchOptionsVisible;
            //var json = JsonConvert.SerializeObject(SearchOptionsViewModel);
            //await Shell.Current.GoToAsync($"{nameof(SearchOptionsPage)}?Content={json}");
        }

        private void UpdateCurrentSearchQuery(string query)
        {
            if (!CurrentQuery.Equals(query))
                CurrentQuery = query;
        }
    }
}
