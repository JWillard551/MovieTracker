using MovieTracker.App.ViewModels.PopUps;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace MovieTracker.App.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopUp : PopupPage
    {
        private ConfirmationPopUpViewModel _viewModel;
        private TaskCompletionSource<bool> _taskCompletionSource = null;

        public ConfirmationPopUp(string message, string confirmOption = "Confirm", string cancelOption = "Cancel")
        {
            _viewModel = new ConfirmationPopUpViewModel(message, confirmOption, cancelOption);
            BindingContext = _viewModel;
            InitializeComponent();
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.SetResult(true);
            PopupNavigation.Instance.PopAsync();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.SetResult(false);
            PopupNavigation.Instance.PopAsync();
        }

        public async Task<bool> Show()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            await PopupNavigation.Instance.PushAsync(this);
            return await _taskCompletionSource.Task;
        }
    }
}