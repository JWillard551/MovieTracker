using MovieTracker.App.ViewModels.BaseViewModels;

namespace MovieTracker.App.ViewModels.PopUps
{
    public class ConfirmationPopUpViewModel : BaseViewModel
    {
        string _confirmationMessage = string.Empty;
        public string ConfirmationMessage
        {
            get { return _confirmationMessage; }
            set { SetProperty(ref _confirmationMessage, value); }
        }

        string _cancelOptionText = string.Empty;
        public string CancelOptionText
        {
            get { return _cancelOptionText; }
            set { SetProperty(ref _cancelOptionText, value); }
        }

        string _confirmationOptionText = string.Empty;
        public string ConfirmationOptionText
        {
            get { return _confirmationOptionText; }
            set { SetProperty(ref _confirmationOptionText, value); }
        }

        public ConfirmationPopUpViewModel(string message, string confirmOption = "Confirm", string cancelOption = "Cancel")
        {
            ConfirmationMessage = message;
            ConfirmationOptionText = confirmOption;
            CancelOptionText = cancelOption;
        }
    }
}
