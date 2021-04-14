using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class DoubleToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = System.Convert.ToDouble(value);
            if (result == 0)
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
