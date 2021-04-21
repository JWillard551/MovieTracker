using MovieTracker.App.ViewModels.BaseViewModels;
using MovieTracker.App.ViewModels.DetailViewModels;
using MovieTracker.TMDbModel.AdditionalModelObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels
{
    public class LetsWatchPageViewModel : BaseViewModel, IViewModelInitialize
    {
        public Dictionary<int, LetsWatchItemViewModel> LetsWatchItems { get; set; }

        public ObservableCollection<LetsWatchItemViewModel> TotalItems { get; set; }
        public ObservableCollection<LetsWatchItemViewModel> FilteredItems { get; set; }

        public Task Initialization { get; private set; }

        public LetsWatchPageViewModel()
        {
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            IsBusy = true;
            var wlMovies = await LoadWatchlistMoviesAsync();
            var favMovies = await LoadFavoriteMoviesAsync();
            var ratedMovies = await LoadRatedMoviesAsync();

            var wlShows = await LoadWatchlistShowsAsync();
            var favShows = await LoadFavoriteShowsAsync();
            var ratedShows = await LoadRatedShowsAsync();

            BuildList(wlMovies, favMovies, ratedMovies, wlShows, favShows, ratedShows);
            SetFilteredList();
            IsBusy = false;
        }

        private void BuildList(List<SearchMovie> wlMovies, List<SearchMovie> favMovies, List<SearchMovie> ratedMovies, List<SearchTv> wlShows, List<SearchTv> favShows, List<SearchTv> ratedShows)
        {
            LetsWatchItems = new Dictionary<int, LetsWatchItemViewModel>();
            wlMovies.ForEach(x => AddWatchlistItemToCollection(x));
            favMovies.ForEach(x => AddFavoriteItemToCollection(x));
            ratedMovies.ForEach(x => AddRatedItemToCollection(x));

            wlShows.ForEach(x => AddWatchlistItemToCollection(x));
            favShows.ForEach(x => AddFavoriteItemToCollection(x));
            ratedShows.ForEach(x => AddRatedItemToCollection(x));
        }

        private void SetFilteredList()
        {
            FilteredItems = new ObservableCollection<LetsWatchItemViewModel>(LetsWatchItems.Values.ToList());
            TotalItems = FilteredItems;
        }

        private async Task<List<SearchMovie>> LoadWatchlistMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var wlMovies = await TMDbService.AccountGetMovieWatchlistAsync(page++);

                if (wlMovies.Page > wlMovies.TotalPages && wlMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlMovies.Results);
            }

            return results;
        }

        private async Task<List<SearchTv>> LoadWatchlistShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var wlShows = await TMDbService.AccountGetTvWatchlistAsync(page++);

                if (wlShows.Page > wlShows.TotalPages && wlShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlShows.Results);
            }

            return results;
        }

        private async Task<List<SearchTv>> LoadFavoriteShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var wlShows = await TMDbService.AccountGetFavoriteTvAsync(page++);

                if (wlShows.Page > wlShows.TotalPages && wlShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(wlShows.Results);
            }

            return results;
        }

        private async Task<List<SearchMovie>> LoadFavoriteMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var favMovies = await TMDbService.AccountGetFavoriteMoviesAsync(page++);

                if (favMovies.Page > favMovies.TotalPages && favMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(favMovies.Results);
            }

            return results;
        }

        private async Task<List<SearchTv>> LoadRatedShowsAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchTv> results = new List<SearchTv>();

            while (!loaded)
            {
                var ratedShows = await TMDbService.AccountGetRatedTvShowsAsync(page++);

                if (ratedShows.Page > ratedShows.TotalPages && ratedShows.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(ratedShows.Results);
            }

            return results;
        }

        private async Task<List<SearchMovie>> LoadRatedMoviesAsync()
        {
            int page = 1;
            bool loaded = false;
            List<SearchMovie> results = new List<SearchMovie>();

            while (!loaded)
            {
                var ratedMovies = await TMDbService.AccountGetRatedMoviesAsync(page++);

                if (ratedMovies.Page > ratedMovies.TotalPages && ratedMovies.Results.Count == 0)
                    loaded = true;
                else
                    results.AddRange(ratedMovies.Results);
            }

            return results;
        }

        private void AddWatchlistItemToCollection(SearchMovie sm)
        {
            if (LetsWatchItems.ContainsKey(sm.Id))
            {
                LetsWatchItems[sm.Id].Item.Watchlist = true;
            }
            else
            {
                LetsWatchItems.Add(sm.Id, new LetsWatchItemViewModel(new LetsWatchItem(sm, watchlist: true)));
            }
        }

        private void AddWatchlistItemToCollection(SearchTv tv)
        {
            if (LetsWatchItems.ContainsKey(tv.Id))
            {
                LetsWatchItems[tv.Id].Item.Watchlist = true;
            }
            else
            {
                LetsWatchItems.Add(tv.Id, new LetsWatchItemViewModel(new LetsWatchItem(tv, watchlist: true)));
            }
        }

        private void AddFavoriteItemToCollection(SearchMovie sm)
        {
            if (LetsWatchItems.ContainsKey(sm.Id))
            {
                LetsWatchItems[sm.Id].Item.Favorite = true;
            }
            else
            {
                LetsWatchItems.Add(sm.Id, new LetsWatchItemViewModel(new LetsWatchItem(sm, favorite: true)));
            }
        }

        private void AddFavoriteItemToCollection(SearchTv tv)
        {
            if (LetsWatchItems.ContainsKey(tv.Id))
            {
                LetsWatchItems[tv.Id].Item.Favorite = true;
            }
            else
            {
                LetsWatchItems.Add(tv.Id, new LetsWatchItemViewModel(new LetsWatchItem(tv, favorite: true)));
            }
        }

        private void AddRatedItemToCollection(SearchMovie sm)
        {
            if (LetsWatchItems.ContainsKey(sm.Id))
            {
                LetsWatchItems[sm.Id].Item.Rated = true;
            }
            else
            {
                LetsWatchItems.Add(sm.Id, new LetsWatchItemViewModel(new LetsWatchItem(sm, rated: true)));
            }
        }

        private void AddRatedItemToCollection(SearchTv tv)
        {
            if (LetsWatchItems.ContainsKey(tv.Id))
            {
                LetsWatchItems[tv.Id].Item.Rated = true;
            }
            else
            {
                LetsWatchItems.Add(tv.Id, new LetsWatchItemViewModel(new LetsWatchItem(tv, rated: true)));
            }
        }

        public void Refresh()
        {
            
        }

        private void ResetFilteredItems()
        {
            FilteredItems = TotalItems;
        }

        public int OnPickMovie()
        {
            IsBusy = true;
            ResetFilteredItems();
            FilteredItems = new ObservableCollection<LetsWatchItemViewModel>(FilteredItems.Where(item => item.Item.MediaType == TMDbLib.Objects.General.MediaType.Movie));
            IsBusy = false;
            return FilteredItems.Count;
        }

        public int OnPickShow()
        {
            IsBusy = true;
            ResetFilteredItems();
            FilteredItems = new ObservableCollection<LetsWatchItemViewModel>(FilteredItems.Where(item => item.Item.MediaType == TMDbLib.Objects.General.MediaType.Tv));
            IsBusy = false;
            return FilteredItems.Count;
        }

        public int OnPick()
        {
            IsBusy = true;
            ResetFilteredItems();
            IsBusy = false;
            return FilteredItems.Count;
        }
    }
}
