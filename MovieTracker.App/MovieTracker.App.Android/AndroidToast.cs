using Android.App;
using Android.Widget;
using MovieTracker.App.Services;

namespace MovieTracker.App.Droid
{
    public class AndroidToast : IMessage
    {
        public void LongAlertMessage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlertMessage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}