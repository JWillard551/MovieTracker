using MovieTracker.App.ViewModels.BaseViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.ViewModels.PopUps
{
    public class RatingPopUpViewModel : BaseViewModel
    {
        private int MediaID { get; set; }

        private MediaType MediaType { get; set; }
        public Command CancelSelectedCommand;

        public RatingPopUpViewModel(int mediaID, MediaType mediaType)
        {
            MediaID = mediaID;
            MediaType = mediaType;
            CancelSelectedCommand = new Command(OnCancelSelected);
        }

        public async Task<int> OnRatingSelected(int selectedRating)
        {
            try
            {
                if (MediaType == MediaType.Movie)
                {
                    await TMDbService.MovieSetRatingAsync(MediaID, selectedRating);
                }
                else if (MediaType == MediaType.Tv)
                {
                    await TMDbService.TvShowSetRatingAsync(MediaID, selectedRating);
                }
            }
            catch (Exception ex)
            {

            }
            return selectedRating;
        }

        private void OnCancelSelected()
        {
            PopupNavigation.Instance.PopAllAsync();
        }
    }
}
