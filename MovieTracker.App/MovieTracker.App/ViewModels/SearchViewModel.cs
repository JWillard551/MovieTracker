using MovieTracker.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MovieTracker.App.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private ObservableCollection<ItemTest> itemsToDisplay;
        public ObservableCollection<ItemTest> ItemsToDisplay { get => itemsToDisplay; set => SetProperty(ref itemsToDisplay, value); }

        public List<ItemTest> Items { get; set; }

        public SearchViewModel()
        {
            InitData();
        }

        public void InitData()
        {
            Items = new List<ItemTest>();
            for (var i = 1; i < 100; i++)
            {
                Items.Add(new ItemTest()
                {
                    TextValue = $"Text Value {i}",
                    Title="This is a really long title for a movie Title",
                    ReleaseYear=$"200{i}"
                });
            }
            ItemsToDisplay = new ObservableCollection<ItemTest>(Items);
        }
    }
}
