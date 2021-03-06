using MovieTracker.App.Views;
using MovieTracker.App.Views.DetailPages;
using MovieTracker.Model.ModelObjects;
using System;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        public Command GoToDetailsCommand { get; set; }

        public Command<SearchResult> SearchResultTapped { get; set; }

        public SearchResult SearchResult { get; set; }

        private RadialGaugeViewModel _ratingVM;
        public RadialGaugeViewModel RadialGaugeViewModel
        {
            get => _ratingVM;
            set => SetProperty(ref _ratingVM, value);
        }

        public SearchResultViewModel() { }

        public SearchResultViewModel(SearchResult searchResult)
        {
            SearchResult = searchResult;
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(searchResult.Rating),
            };
            SearchResultTapped = new Command<SearchResult>(OnSearchResultTapped);
        }

        private async void OnSearchResultTapped(SearchResult searchResult)
        {
            IsBusy = true;
            if (searchResult.MediaType == Model.ModelEnums.MediaType.Movie)
            {
                await Shell.Current.GoToAsync($"{nameof(MovieDetailPage)}?MovieID={searchResult.Id}");
            }
            else if (searchResult.MediaType == Model.ModelEnums.MediaType.Show)
            {
                //Create ShowDetailViewModel + Push ShowDetailPage
            }
            else if (searchResult.MediaType == Model.ModelEnums.MediaType.Person)
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
