using MovieTracker.App.Models;
using MovieTracker.Model.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20;

        private ObservableCollection<SearchResultViewModel> itemsToDisplay;
        public ObservableCollection<SearchResultViewModel> ItemsToDisplay { get => itemsToDisplay; set => SetProperty(ref itemsToDisplay, value); }

        public List<SearchResultViewModel> Items { get; set; }

        public SearchViewModel()
        {
            InitData();
        }

        public void InitData()
        {
            Items = new List<SearchResultViewModel>();

            //for (var i = 1; i < 100; i++)
            //{
            //    Items.Add(new SearchResultViewModel()
            //    {
            //        TextValue = $"Text Value {i}",
            //        Title="This is a really long title for a movie Title",
            //        ReleaseYear=$"200{i}",
            //        RadialGaugeViewModel = new RadialGaugeViewModel()
            //        {
            //            MinValue = 1,
            //            MaxValue = 100,
            //            CurrentProgress = i,
            //            //Detail = GetRatingDetail(i),
            //        }
            //    });
            //}

            ItemsToDisplay = new ObservableCollection<SearchResultViewModel>(Items);
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
            //This method should take the query from the SearchBarQueryHandler and pass it into the TMDB client helper class.
            //The helper class will handle the API call to fetch data. The returned results will need to be converted to one of my internal model objects (a search result object).
            //
            Items = new List<SearchResultViewModel>();
            var results = await TMDbServiceClientHelper.SearchAsync(query, CurrentPage, new CancellationToken());
            foreach (var res in results)
            {
                Items.Add(new SearchResultViewModel(res));
            }
            ItemsToDisplay = new ObservableCollection<SearchResultViewModel>(Items);
        }
    }
}
