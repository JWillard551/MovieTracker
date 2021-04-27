using MovieTracker.App.ViewModels.PopUps;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPopUp : PopupPage
    {
        RatingPopUpViewModel _viewModel;

        TaskCompletionSource<int> _taskCompletionSource = null;

        public RatingPopUp(int id, MediaType mediaType)
        {
            InitializeComponent();
            _viewModel = new RatingPopUpViewModel(id, mediaType);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                if (button.Text == "Cancel")
                {
                    _taskCompletionSource?.SetResult(-1);
                    await PopupNavigation.Instance.PopAllAsync();
                    return;
                }

                if (button.Text == "Remove")
                {
                    _taskCompletionSource?.SetResult(0);
                    await PopupNavigation.Instance.PopAllAsync();
                    return;
                }

                var rating = System.Convert.ToInt32(button.Text);

                if (rating == 0)
                    return;

                var setResult = await _viewModel.OnRatingSelected(rating);
                _taskCompletionSource?.SetResult(setResult);
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        public async Task<int> Show()
        {
            _taskCompletionSource = new TaskCompletionSource<int>();
            await PopupNavigation.Instance.PushAsync(this);
            return await _taskCompletionSource.Task;
        }
    }
}