using MovieTracker.TMDbModel.Utils;
using System;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace MovieTracker.App.ViewModels.BaseViewModels
{
    public class AccountDetailViewModel : BaseViewModel
    {
        #region Account Properties

        private bool _itemFavorited;
        public bool ItemFavorited
        {
            get { return _itemFavorited; }
            set { SetProperty(ref _itemFavorited, value); }
        }

        private bool _itemWatchlisted;
        public bool ItemWatchlisted
        {
            get { return _itemWatchlisted; }
            set { SetProperty(ref _itemWatchlisted, value); }
        }

        private bool _itemRated;
        public bool ItemRated
        {
            get { return _itemRated; }
            set { SetProperty(ref _itemRated, value); }
        }

        private string _itemRating = "0";
        public string ItemRating
        {
            get { return _itemRating; }
            set { SetProperty(ref _itemRating, value); }
        }

        private RadialGaugeViewModel _radialGaugeViewModel;
        public RadialGaugeViewModel RadialGaugeViewModel
        {
            get => _radialGaugeViewModel;
            set => SetProperty(ref _radialGaugeViewModel, value);
        }

        #endregion

        protected async Task InitializeAccountInfo(int id, MediaType mediaType)
        {
            AccountState accountState = null;

            if (mediaType == MediaType.Movie)
                accountState = await TMDbService.GetMovieAccountStateAsync(id);
            else if (mediaType == MediaType.Tv)
                accountState = await TMDbService.GetTvShowAccountStateAsync(id);

            ItemFavorited = accountState?.Favorite ?? false;
            ItemWatchlisted = accountState?.Watchlist ?? false;
            ItemRated = System.Convert.ToDouble(accountState?.Rating) > 0;
            ItemRating = GetMediaAccountRating(accountState?.Rating ?? 0);
        }

        protected void InitializeRadialGaugeViewModel(double voteAvg)
        {
            RadialGaugeViewModel = new RadialGaugeViewModel()
            {
                MinValue = 1,
                MaxValue = 100,
                CurrentProgress = Convert.ToDouble(voteAvg * 10)
            };
        }

        private string GetMediaAccountRating(double rating)
        {
            if (rating == 0)
                return string.Empty;
            else return rating.ToString();
        }

        #region Item Commands (Update / Remove / Add / Etc...)

        protected async Task<bool> HandleRemoveItem(int mediaID, MediaType mediaType, DetailButtonType buttonType)
        {
            var result = await UserPromptService.PromptUserYesNoAsync($"Remove this item from your {buttonType.ToString()}?");
            if (result)
            {
                switch (buttonType)
                {
                    case DetailButtonType.Watchlist:
                        return await RemoveWatchlistItem(mediaID, mediaType);
                    case DetailButtonType.Favorites:
                        return await RemoveFavoritesItem(mediaID, mediaType);
                    case DetailButtonType.Ratings:
                        return await RemoveRatingItem(mediaID, mediaType);
                    default:
                        return false;
                }
            }
            return false;
        }

        private async Task<bool> RemoveWatchlistItem(int mediaID, MediaType mediaType)
        {
            return await TMDbService.AccountChangeWatchlistStatusAsync(mediaType, mediaID, false);
        }

        private async Task<bool> RemoveFavoritesItem(int mediaID, MediaType mediaType)
        {
            return await TMDbService.AccountChangeFavoriteStatusAsync(mediaType, mediaID, false);
        }

        private async Task<bool> RemoveRatingItem(int mediaID, MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.Movie:
                    return await TMDbService.MovieRemoveRatingAsync(mediaID);
                case MediaType.Tv:
                    return await TMDbService.TvShowRemoveRatingAsync(mediaID);
                default:
                    return false;
            }
        }

        protected async Task<bool> HandleDetailButtonSelectedAsync(bool buttonCurrentSetState, DetailButtonType buttonType, MediaType mediaType, int mediaID)
        {
            if (buttonCurrentSetState)
            {
                var dialogResult = await UserPromptService.PromptUserYesNoAsync($"Remove this from your {buttonType.ToString()}?");
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
                        var newRating = await HandleRatingEdit("0", mediaID, mediaType);
                        if (string.IsNullOrWhiteSpace(newRating) || newRating.Equals("0"))
                        {
                            ItemRating = "";
                            ItemRated = false;
                            return buttonCurrentSetState;
                        }
                        else
                        {
                            ItemRating = newRating;
                            ItemRated = true;
                            return !buttonCurrentSetState;
                        }
                    }
                    break;
            }

            if (successful)
            {
                if (!buttonCurrentSetState)
                    ToastService.ShortAlertMessage($"Added to {buttonType.ToString()}!");
                else
                    ToastService.ShortAlertMessage($"Removed from {buttonType.ToString()}!");
            
                return !buttonCurrentSetState;
            }
            else
            {
                return buttonCurrentSetState;
            }
        }

        protected async Task<string> HandleRatingEdit(string initialRating, int mediaID, MediaType mediaType)
        {
            bool successful = false;
            var rateResult = await UserPromptService.PromptUserRatingAsync(mediaID, mediaType);
            var rating = System.Convert.ToInt32(rateResult);

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
                    ToastService.LongAlertMessage($"Failed to set rating on item!");
                    successful = false;
                }
            }
            else if (rating == 0)
            {
                var remove = await UserPromptService.PromptUserYesNoAsync("Remove this rating?");

                if (remove)
                {
                    if (mediaType == MediaType.Movie)
                        successful = await TMDbService.MovieRemoveRatingAsync(mediaID);
                    else if (mediaType == MediaType.Tv)
                        successful = await TMDbService.TvShowRemoveRatingAsync(mediaID);

                    if (successful)
                    {
                        ToastService.LongAlertMessage($"Removed rating!");
                    }
                }
            }
            else if (rating == -1)
            {
                return initialRating;
            }

            if (successful)
                return rating.ToString();
            else
                return initialRating;
        }

        #endregion
    }
}
