using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class ImagePlaceholderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "imgplaceholder.png";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
