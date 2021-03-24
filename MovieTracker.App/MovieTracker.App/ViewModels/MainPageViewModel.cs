using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.Model.Client;
using MovieTracker.Model.ModelObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IDetailViewModel
    {
        public ObservableCollection<PopularItemViewModel> PopularMovies { get; set; }

        public ObservableCollection<PopularItemViewModel> PopularShows { get; set; }

        private ObservableCollection<PopularItemViewModel> _selectedSource;
        public ObservableCollection<PopularItemViewModel> SelectedSource { get => _selectedSource; set => SetProperty(ref _selectedSource, value); }

        public Command ShowsClickedCommand { get; set; }

        public Command MoviesClickedCommand { get; set; }

        public Task Initialization { get; private set; }

        public MainPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            var popularMovies = await TMDbServiceClientHelper.GetPopularMovies(new System.Threading.CancellationToken());
            PopularMovies = new ObservableCollection<PopularItemViewModel>(popularMovies.Select(pm => new PopularItemViewModel(pm)));
            var popularShows = await TMDbServiceClientHelper.GetPopularShows(new System.Threading.CancellationToken());
            PopularShows = new ObservableCollection<PopularItemViewModel>(popularShows.Select(ps => new PopularItemViewModel(ps)));
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
