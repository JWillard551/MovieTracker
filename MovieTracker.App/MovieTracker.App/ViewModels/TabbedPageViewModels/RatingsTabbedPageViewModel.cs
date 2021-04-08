using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.App.ViewModels.DetailViewModels.Movie;
using MovieTracker.App.ViewModels.DetailViewModels.Show;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels.TabbedPageViewModels
{
    public class RatingsTabbedPageViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }

        public MovieRatingsViewModel MovieViewModel { get; set; }

        public ShowRatingsViewModel ShowViewModel { get; set; }

        public RatingsTabbedPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            MovieViewModel = new MovieRatingsViewModel();
            ShowViewModel = new ShowRatingsViewModel();
            await Task.WhenAll(MovieViewModel.Initialization, ShowViewModel.Initialization);
        }
    }
}
