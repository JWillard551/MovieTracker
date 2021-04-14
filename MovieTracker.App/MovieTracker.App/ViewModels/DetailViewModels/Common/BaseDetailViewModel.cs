using MovieTracker.TMDbModel.Utils;
using System;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace MovieTracker.App.ViewModels.DetailViewModels.Common
{
    public class BaseDetailViewModel : BaseViewModel
    {
        private bool _favorited;
        public bool Favorited
        {
            get { return _favorited; }
            set { SetProperty(ref _favorited, value); }
        }

        private bool _watchlisted;
        public bool Watchlisted
        {
            get { return _watchlisted; }
            set { SetProperty(ref _watchlisted, value); }
        }

        private bool _rated;
        public bool Rated
        {
            get { return _rated; }
            set { SetProperty(ref _rated, value); }
        }

        protected AccountState AccountState { get; set; }

        protected async Task<bool> HandleDetailButtonSelectedAsync(bool buttonCurrentSetState, DetailButtonType buttonType, MediaType mediaType, int mediaID)
        {
            if (buttonCurrentSetState)
            {
                var dialogResult = await UserPromptService.PromptUserYesNoAsync(buttonType.ToString(), $"Remove this from your {buttonType.ToString()}?");
                if (!dialogResult)
                    return buttonCurrentSetState;
            }

            bool successful = false;
            switch (buttonType)
            {
                case DetailButtonType.Watchlist:
                    successful = await TMDbService.AccountChangeWatchlistStatusAsync(mediaType, mediaID, !buttonCurrentSetState);
                    break;
                case DetailButtonType.Favorites:
                    successful = await TMDbService.AccountChangeFavoriteStatusAsync(mediaType, mediaID, !buttonCurrentSetState);
                    break;
                case DetailButtonType.Ratings:
                    if (buttonCurrentSetState)
                    {
                        if (mediaType == MediaType.Movie)
                            successful = await TMDbService.MovieRemoveRatingAsync(mediaID);
                        else if (mediaType == MediaType.Tv)
                            successful = await TMDbService.TvShowRemoveRatingAsync(mediaID);
                    }
                    else
                    {
                        var rateResult = await UserPromptService.PromptUserRatingAsync("Rating", "Rate this media (1-10)", keyboard: Xamarin.Forms.Keyboard.Numeric, initialValue: "0");
                        var rating = System.Convert.ToDouble(rateResult);

                        //TODO: Temporary hack to scrub input and force it between a 1-10 value.
                        if (rating > 10)
                            rating = 10;
                        else if (rating < 1)
                            rating = 1;

                        if (rating > 0)
                        {
                            try
                            {
                                if (mediaType == MediaType.Movie)
                                    successful = await TMDbService.MovieSetRatingAsync(mediaID, rating);
                                else if (mediaType == MediaType.Tv)
                                    successful = await TMDbService.TvShowSetRatingAsync(mediaID, rating);
                            }
                            catch (Exception ex)
                            {
                                successful = false;
                                //If odd numbers are entered this throws and error. For now, just catch it and forget it.
                            }
                            
                        }
                        else
                        {
                            return buttonCurrentSetState;
                        }
                    }
                    break;
            }

            if (successful)
            {
                if (!buttonCurrentSetState)
                    ToastService.LongAlertMessage($"Added to {buttonType.ToString()}!");
                else
                    ToastService.LongAlertMessage($"Removed from {buttonType.ToString()}!");
            
                return !buttonCurrentSetState;
            }
            else
            {
                return buttonCurrentSetState;
            }
        }
    }
}
