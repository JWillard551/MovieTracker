using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class NumberOfSeasonsToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numOfSeasons = (int)value;
            if (numOfSeasons == 0)
                return value;
            else if (numOfSeasons == 1)
                return $"{numOfSeasons} Season";
            else
                return $"{numOfSeasons} Seasons";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
