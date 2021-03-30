using Foundation;
using MovieTracker.App.Services;
using UIKit;

namespace MovieTracker.App.iOS
{
    public class iOSToast : IMessage
    {
        private const double LONG_DELAY = 5.0;
        private const double SHORT_DELAY = 2.5;

        NSTimer alertDelay;
        UIAlertController alert;

        private void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) => { DismissMessage(); });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        public void LongAlertMessage(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        public void ShortAlertMessage(string message)
        {
            ShowAlert(message, SHORT_DELAY);
        }

        private void DismissMessage()
        {
            if (alert != null)
                alert.DismissViewController(true, null);
            if (alertDelay != null)
                alertDelay.Dispose();
        }
    }
}