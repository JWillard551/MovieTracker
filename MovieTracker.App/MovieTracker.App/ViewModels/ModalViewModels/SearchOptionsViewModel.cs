using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.ModalViewModels
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class SearchOptionsViewModel : BaseViewModel
    {
        string _content = "";
        public string Content
        {
            get => _content;
            set
            {
                _content = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                PerformOperation(_content);
            }
        }


        //What to filter movies by? What can I do easy enough?
        //Sort By Date (Ascending and Descending)
        //Sort By Name (Ascending and Descending)

        //Include People Checkbox
        public bool IncludePeopleInSearch { get; set; } = true;

        //Include TV Shows Checkbox
        public bool IncludeTVInSearch { get; set; } = true;

        //Include Movies Checkbox
        public bool IncludeMoviesInSearch { get; set; } = true;

        public SearchOptionsViewModel()
        {

        }

        private void PerformOperation(string json)
        {
            var content = JsonConvert.DeserializeObject<SearchOptionsViewModel>(json);
            //Map properties here.
            IncludeMoviesInSearch = content.IncludeMoviesInSearch;
            IncludePeopleInSearch = content.IncludePeopleInSearch;
            IncludeTVInSearch = content.IncludeTVInSearch;
        }
    }
}
