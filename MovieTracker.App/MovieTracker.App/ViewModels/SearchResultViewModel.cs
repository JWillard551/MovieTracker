using MovieTracker.Model.ModelObjects;
using System;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        public Command GoToDetailsCommand { get; set; }
        public Command LoadMoreItemsCommand { get; set; }

        public SearchResult SearchResult { get; set; }
        //public string PhotoUrl
        //{
        //    get
        //    {
        //        return "kingsman.jpg";
        //    }
        //}

        //public string TextValue { get; set; }

        //public string ReleaseYear { get; set; }

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
        }
    }
}
