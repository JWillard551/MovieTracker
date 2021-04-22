using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class WatchlistIconConverter : IValueConverter
    {
        public readonly string WATCHLIST = "add_watchlist_white.png";
        public readonly string WATCHLIST_SET = "add_watchlist_set.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSet = (bool)value;
            return isSet ? WATCHLIST_SET : WATCHLIST;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
