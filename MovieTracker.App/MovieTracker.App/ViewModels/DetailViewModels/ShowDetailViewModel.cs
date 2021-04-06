﻿using System.Threading.Tasks;
using TMDbLib.Objects.TvShows;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.DetailViewModels
{
    public class ShowDetailViewModel : BaseViewModel, IDetailViewModel
    {
        public Task Initialization { get; private set; }
        public TvShow ShowInfo { get; set; }

        public Command AddToListCommand => throw new System.NotImplementedException();

        public Command AddToWatchListCommand => throw new System.NotImplementedException();

        public Command AddToFavoritesCommand => throw new System.NotImplementedException();

        public Command RateCommand => throw new System.NotImplementedException();

        public ShowDetailViewModel(int id)
        {
            Initialization = InitializeAsync(id);
        }

        public async Task InitializeAsync(int id)
        {
            ShowInfo = await TMDbService.GetTVShowAsync(id);
        }
    }
}
