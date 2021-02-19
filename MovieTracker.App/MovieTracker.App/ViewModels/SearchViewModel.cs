using MovieTracker.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MovieTracker.App.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
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
            for (var i = 1; i < 100; i++)
            {
                Items.Add(new SearchResultViewModel()
                {
                    TextValue = $"Text Value {i}",
                    Title="This is a really long title for a movie Title",
                    ReleaseYear=$"200{i}",
                    RadialGaugeViewModel = new RadialGaugeViewModel()
                    {
                        MinValue = 1,
                        MaxValue = 100,
                        CurrentProgress = i,
                        Detail = GetRatingDetail(i),
                    }
                });
            }
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
    }
}
