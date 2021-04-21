using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class FavoriteIconConverter : IValueConverter
    {
        public readonly string FAVORITE = "outline_favorite_white_24dp.png";
        public readonly string FAVORITE_SET = "outline_favorite_white_set_24dp.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSet = (bool)value;
            return isSet ? FAVORITE_SET : FAVORITE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
