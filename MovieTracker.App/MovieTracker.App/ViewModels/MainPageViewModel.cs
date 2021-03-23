using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IDetailViewModel
    {
        public List<Movie> PopularMovies { get; set; }

        public List<Show> PopularShows { get; set; }

        public Task Initialization { get; private set; }

        public MainPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            PopularMovies = await TMDbServiceClientHelper.GetPopularMovies(new System.Threading.CancellationToken());
            PopularShows = await TMDbServiceClientHelper.GetPopularShows(new System.Threading.CancellationToken());
        }
    }
}
