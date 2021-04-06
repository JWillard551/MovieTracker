using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMDbLib.Objects.General;
using Xamarin.Forms;

namespace MovieTracker.App.Converters
{
    public class GenreToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var genres = value as List<Genre>;
            if (!genres?.Any() ?? true)
                return string.Empty;
            return string.Join(", ", genres.Select(g => g.Name));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
