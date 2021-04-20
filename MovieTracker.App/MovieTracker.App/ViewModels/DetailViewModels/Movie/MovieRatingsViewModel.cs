using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.CollectionViewItemViewModels;
using MovieTracker.App.Views.TabbedPages;
using MovieTracker.TMDbModel.Utils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels.Movie
{
    public class MovieRatingsViewModel : PagedLoadingViewModel<SearchContainer<SearchMovieWithRating>>, IViewModelInitialize
    {
        private ObservableRangeCollection<MovieItemViewModel> _ratedMovies;
        public ObservableRangeCollection<MovieItemViewModel> RatedMovies
        {
            get => _ratedMovies;
            set => SetProperty(ref _ratedMovies, value);
        }
        public Task Initialization { get; private set; }

        #region Commands

        public Command<RatedMovieItemViewModel> EditRateCommand { get; private set; }

        public Command<MovieItemViewModel> RemoveRatingCommand { get; private set; }

        public Command<MovieItemViewModel> GoToDetailsCommand { get; private set; }

        #endregion

        public MovieRatingsViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var rated = await TMDbService.AccountGetRatedMoviesAsync(CurrentPage);
            RatedMovies = new ObservableRangeCollection<MovieItemViewModel>(rated.Results.Select(movie => new RatedMovieItemViewModel(movie)).ToList());
            EditRateCommand = new Command<RatedMovieItemViewModel>(OnEditRateCommand);
            RemoveRatingCommand = new Command<MovieItemViewModel>(OnRemoveRatingCommand);
            GoToDetailsCommand = new Command<MovieItemViewModel>(GoToDetails);
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
        }

        private async void OnEditRateCommand(RatedMovieItemViewModel mvm)
        {
            mvm.ItemRating = await HandleRatingEdit(mvm.ItemRating, mvm.Movie.Id, MediaType.Movie);
        }

        private async void OnRemoveRatingCommand(MovieItemViewModel mvm)
        {
            var result = await HandleRemoveItem(mvm.Movie.Id, MediaType.Movie, DetailButtonType.Ratings);
            if (result)
                RatedMovies.Remove(mvm);
        }

        private async void GoToDetails(MovieItemViewModel mvm)
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(MovieTabbedPage)}?MovieID={mvm.Movie.Id}");
            IsBusy = false;
        }

        public override async Task<SearchContainer<SearchMovieWithRating>> LoadMoreItemsAsync()
        {
            try
            {
                return await TMDbService.AccountGetRatedMoviesAsync(++CurrentPage);
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                return new SearchContainer<SearchMovieWithRating>() { Results = new List<SearchMovieWithRating>() };
            }
        }

        public override void EvaluateResultsAndUpdateObservableCollection(SearchContainer<SearchMovieWithRating> results)
        {
            if (results.Page > results.TotalPages && results.Results.Count == 0)
            {
                ItemLoadingComplete();
                return;
            }

            RatedMovies.AddRange(results.Results.Select(movie => new RatedMovieItemViewModel(movie)).ToList());
        }
    }
}
