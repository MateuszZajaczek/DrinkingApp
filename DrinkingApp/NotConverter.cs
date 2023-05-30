using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DrinkingApp.Converters // Zamie� na rzeczywist� przestrze� nazw
{
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return value;
        }
    }
}
