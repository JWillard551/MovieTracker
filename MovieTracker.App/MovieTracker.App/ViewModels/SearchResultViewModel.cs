using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using System;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;
using MovieTracker.App.ViewModels.BaseViewModels;

namespace MovieTracker.App.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        public Command GoToDetailsCommand { get; set; }

        public Command<SearchItem> SearchResultTapped { get; set; }

        public SearchItem SearchItem { get; set; }

        private RadialGaugeViewModel _ratingVM;
        public RadialGaugeViewModel RadialGaugeViewModel
        {
            get => _ratingVM;
            set => SetProperty(ref _ratingVM, value);
        }

        public SearchResultViewModel() { }

        public SearchResultViewModel(SearchBase searchResult)
        {
            SearchItem = GetSearchResultItem(searchResult);
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(SearchItem.Rating * 10),
            };
            SearchResultTapped = new Command<SearchItem>(OnSearchResultTapped);
        }

        private SearchItem GetSearchResultItem(SearchBase searchResult)
        {
            if (searchResult is SearchMovie)
                return new SearchItem(searchResult as SearchMovie);

            if (searchResult is SearchTv)
                return new SearchItem(searchResult as SearchTv);

            if (searchResult is SearchPerson)
                return new SearchItem(searchResult as SearchPerson);

            return new SearchItem(searchResult);
        }

        private async void OnSearchResultTapped(SearchItem searchItem)
        {
            IsBusy = true;
            if (searchItem.MediaType == MediaType.Movie)
            {
                await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={searchItem.Id}");
            }
            else if (searchItem.MediaType == MediaType.Tv)
            {
                await Shell.Current.GoToAsync($"{nameof(ShowTabbedPage)}?ShowID={searchItem.Id}");
            }
            else if (searchItem.MediaType == MediaType.Person)
            {
                //Create PersonDetailViewModel + Push PersonDetailPage
            }
            else
            {
                //Unspecified - Handle.
            }
            IsBusy = false;
        }
    }
}
