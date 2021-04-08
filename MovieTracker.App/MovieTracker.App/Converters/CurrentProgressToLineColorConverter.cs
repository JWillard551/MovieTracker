using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class CurrentProgressToLineColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rating = (double)value;
            return GetLineColorByRating(rating);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color GetLineColorByRating(double rating)
        {
            if (rating >= 70)
                return Color.FromHex("41FF2B");
            else if (rating >= 40)
                return Color.FromHex("9EA832");
            else
                return Color.FromHex("A83632");
        }
    }
}
