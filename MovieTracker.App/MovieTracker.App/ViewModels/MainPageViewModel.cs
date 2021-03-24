using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IDetailViewModel
    {
        public ObservableCollection<PopularItem> PopularMovies { get; set; }

        public ObservableCollection<PopularItem> PopularShows { get; set; }

        private ObservableCollection<PopularItem> _selectedSource;
        public ObservableCollection<PopularItem> SelectedSource { get => _selectedSource; set => SetProperty(ref _selectedSource, value); }

        public Command ShowsClickedCommand { get; set; }

        public Command MoviesClickedCommand { get; set; }

        public Task Initialization { get; private set; }

        public MainPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            PopularMovies = await TMDbServiceClientHelper.GetPopularMovies(new System.Threading.CancellationToken());
            PopularShows = await TMDbServiceClientHelper.GetPopularShows(new System.Threading.CancellationToken());
            SelectedSource = PopularMovies;
            //MoviesClickedCommand = new Command(OnMoviesClicked);
            //ShowsClickedCommand = new Command(OnShowsClicked);
        }

        public void OnMoviesClicked()
        {
            SelectedSource = PopularMovies;
        }

        public void OnShowsClicked()
        {
            SelectedSource = PopularShows;
        }
    }
}
