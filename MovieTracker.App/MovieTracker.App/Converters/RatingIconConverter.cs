using System.Globalization;
using System;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class RatingIconConverter : IValueConverter
    {
        public readonly string RATING = "add_rating_white.png";
        public readonly string RATING_SET = "add_rating_set.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSet = (bool)value;
            return isSet ? RATING_SET : RATING;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
