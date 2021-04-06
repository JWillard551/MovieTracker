using MovieTracker.App.ViewModels.DetailViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Trending;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IDetailViewModel
    {
        public ObservableCollection<PopularItemViewModel> PopularMovies { get; set; }

        public ObservableCollection<PopularItemViewModel> PopularShows { get; set; }

        private ObservableCollection<PopularItemViewModel> _selectedSource;
        public ObservableCollection<PopularItemViewModel> SelectedSource { get => _selectedSource; set => SetProperty(ref _selectedSource, value); }

        public ObservableCollection<PopularItemViewModel> TrendingToday { get; set; }

        public ObservableCollection<PopularItemViewModel> TrendingThisWeek { get; set; }

        private ObservableCollection<PopularItemViewModel> _selectedTrendingSource;
        public ObservableCollection<PopularItemViewModel> SelectedTrendingSource { get => _selectedTrendingSource; set => SetProperty(ref _selectedTrendingSource, value); }

        public Task Initialization { get; private set; }

        public MainPageViewModel()
        {
            
            Initialization = InitializeAsync();
            
        }

        public async Task InitializeAsync()
        {
            //var sessionResult = await TMDbService.AccountGetMovieWatchlistAsync();

            var removeRayaLastDragonResult = await TMDbService.AccountChangeWatchlistStatusAsync(TMDbLib.Objects.General.MediaType.Movie, 527774, false);
            var addRayaLastDragonResult = await TMDbService.AccountChangeWatchlistStatusAsync(TMDbLib.Objects.General.MediaType.Movie, 527774, true);


            //IsBusy = true;
            //var popularMovies = await TMDbService.GetPopularMoviesAsync("en-US", 1);
            //PopularMovies = new ObservableCollection<PopularItemViewModel>(popularMovies.Results.Select(pm => new PopularItemViewModel(pm)));
            //var popularShows = await TMDbService.GetPopularShowsAsync("en-US", 1);
            //PopularShows = new ObservableCollection<PopularItemViewModel>(popularShows.Results.Select(ps => new PopularItemViewModel(ps)));
            //SelectedSource = PopularMovies;
            //var trendingToday= await TMDbService.GetTrendingMoviesAsync(TimeWindow.Day);
            //TrendingToday = new ObservableCollection<PopularItemViewModel>(trendingToday.Results.Select(ttd => new PopularItemViewModel(ttd)));
            //var trendingThisWeek = await TMDbService.GetTrendingMoviesAsync(TimeWindow.Week);
            //TrendingThisWeek = new ObservableCollection<PopularItemViewModel>(trendingThisWeek.Results.Select(ttw => new PopularItemViewModel(ttw)));
            //SelectedTrendingSource = TrendingToday;
            //IsBusy = false;
        }

        public void OnMoviesClicked()
        {
            SelectedSource = PopularMovies;
        }

        public void OnShowsClicked()
        {
            SelectedSource = PopularShows;
        }

        public void OnTrendingTodayClicked()
        {
            SelectedTrendingSource = TrendingToday;
        }

        public void OnTrendingThisWeekClicked()
        {
            SelectedTrendingSource = TrendingThisWeek;
        }
    }
}
